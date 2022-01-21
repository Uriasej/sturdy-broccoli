using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity.Services.Core.Configuration;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Device;
using Unity.Services.Core.Device.Internal;
using Unity.Services.Core.Environments;
using Unity.Services.Core.Environments.Internal;
using Unity.Services.Core.Internal;
using Unity.Services.Core.Scheduler.Internal;
using Unity.Services.Core.Telemetry.Internal;
using Unity.Services.Core.Threading.Internal;
using UnityEngine;
using NotNull = JetBrains.Annotations.NotNullAttribute;

namespace Unity.Services.Core.Registration
{
    class CorePackageInitializer : IInitializablePackage
    {
        internal const string CorePackageName = "com.unity.services.core";

        ActionScheduler m_ActionScheduler;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            CoreRegistry.Instance.RegisterPackage(new CorePackageInitializer())
                .ProvidesComponent<IInstallationId>()
                .ProvidesComponent<ICloudProjectId>()
                .ProvidesComponent<IActionScheduler>()
                .ProvidesComponent<IEnvironments>()
                .ProvidesComponent<IProjectConfiguration>()
                .ProvidesComponent<IMetricsFactory>()
                .ProvidesComponent<IDiagnosticsFactory>()
                .ProvidesComponent<IUnityThreadUtils>();
        }

        /// <summary>
        /// This is the Initialize callback that will be triggered by the Core package.
        /// This method will be invoked when the game developer calls UnityServices.InitializeAsync().
        /// </summary>
        /// <param name="registry">
        /// The registry containing components from different packages.
        /// </param>
        /// <returns>
        /// Return a Task representing your initialization.
        /// </returns>
        public async Task Initialize(CoreRegistry registry)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // There are potential race conditions with other services we're trying to avoid by calling
            // RegisterInstallationId as the _very first_ thing we do.
            RegisterInstallationId(registry);

            if (m_ActionScheduler is null)
            {
                m_ActionScheduler = new ActionScheduler();
            }

            RegisterActionScheduler(registry, m_ActionScheduler);
            try
            {
                var projectConfiguration = await RegisterProjectConfigurationAsync(
                    registry, UnityServices.Instance.Options);
                var environments = RegisterEnvironments(registry, projectConfiguration);
                var cloudProjectId = RegisterCloudProjectId(registry);

                var metricsFactory = RegisterMetrics(
                    registry, m_ActionScheduler, projectConfiguration, cloudProjectId, environments);
                var coreMetrics = metricsFactory.Create(CorePackageName);
                CoreMetrics.Instance.Metrics = coreMetrics;

                RegisterDiagnostics(registry, m_ActionScheduler, projectConfiguration, cloudProjectId, environments);
                RegisterThreadingUtils(registry);
            }
            catch (Exception)
            {
                // We keep a reference to the scheduler and monitor other components registration
                // to be able to revert the changes done to external systems in case of failure.
                m_ActionScheduler.QuitPlayerLoopSystem();
                throw;
            }

            stopwatch.Stop();
            CoreMetrics.Instance.SendCorePackageInitTimeMetric(stopwatch.Elapsed.TotalSeconds);
        }

        internal static void RegisterInstallationId(CoreRegistry registry)
        {
            var installationId = new InstallationId();
            installationId.CreateIdentifier();
            registry.RegisterServiceComponent<IInstallationId>(installationId);
        }

        internal static void RegisterActionScheduler(CoreRegistry registry, ActionScheduler scheduler)
        {
            scheduler.JoinPlayerLoopSystem();
            registry.RegisterServiceComponent<IActionScheduler>(scheduler);
        }

        internal static ICloudProjectId RegisterCloudProjectId(CoreRegistry registry)
        {
            var cloudProjectId = new CloudProjectId();
            registry.RegisterServiceComponent<ICloudProjectId>(cloudProjectId);
            return cloudProjectId;
        }

        internal static IEnvironments RegisterEnvironments(CoreRegistry registry, IProjectConfiguration projectConfiguration)
        {
            var environments = new Environments.Internal.Environments();
            environments.Current = projectConfiguration.GetString(EnvironmentsOptionsExtensions.EnvironmentNameKey, "production");
            registry.RegisterServiceComponent<IEnvironments>(environments);
            return environments;
        }

        internal static async Task<IProjectConfiguration> RegisterProjectConfigurationAsync(
            [NotNull] CoreRegistry registry,
            [NotNull] InitializationOptions options)
        {
            var projectConfig = await GenerateProjectConfigurationAsync(options);
            registry.RegisterServiceComponent<IProjectConfiguration>(projectConfig);
            return projectConfig;
        }

        internal static async Task<ProjectConfiguration> GenerateProjectConfigurationAsync(
            [NotNull] InitializationOptions options)
        {
            var serializedConfig = await GetSerializedConfigOrEmptyAsync();
            var configValues = new Dictionary<string, ConfigurationEntry>(serializedConfig.Keys.Length);
            configValues.FillWith(serializedConfig);
            configValues.FillWith(options);
            return new ProjectConfiguration(configValues);
        }

        internal static async Task<SerializableProjectConfiguration> GetSerializedConfigOrEmptyAsync()
        {
            try
            {
                var config = await ConfigurationUtils.ConfigurationLoader.GetConfigAsync();
                return config;
            }
            catch (Exception e)
            {
                CoreLogger.LogError(
                    "En error occured while trying to get the project configuration for services." +
                    $"\n{e.Message}" +
                    $"\n{e.StackTrace}");
                return SerializableProjectConfiguration.Empty;
            }
        }

        internal static IMetricsFactory RegisterMetrics(
            CoreRegistry registry, IActionScheduler scheduler, IProjectConfiguration projectConfiguration,
            ICloudProjectId cloudProjectId, IEnvironments environments)
        {
            var metricsFactory = TelemetryUtils.CreateMetricsFactory(
                scheduler, projectConfiguration, cloudProjectId, environments);
            registry.RegisterServiceComponent<IMetricsFactory>(metricsFactory);
            return metricsFactory;
        }

        internal static void RegisterDiagnostics(
            CoreRegistry registry, IActionScheduler scheduler, IProjectConfiguration projectConfiguration,
            ICloudProjectId cloudProjectId, IEnvironments environments)
        {
            var diagnosticsFactory = TelemetryUtils.CreateDiagnosticsFactory(
                scheduler, projectConfiguration, cloudProjectId, environments);
            registry.RegisterServiceComponent<IDiagnosticsFactory>(diagnosticsFactory);
        }

        internal static void RegisterThreadingUtils(CoreRegistry registry)
        {
            var threadingUtils = new UnityThreadUtilsInternal();
            registry.RegisterServiceComponent<IUnityThreadUtils>(threadingUtils);
        }
    }
}

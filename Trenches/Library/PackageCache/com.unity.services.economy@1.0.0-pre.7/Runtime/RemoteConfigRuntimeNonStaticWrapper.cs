using System;
using Newtonsoft.Json.Linq;
using Unity.RemoteConfig;

namespace Unity.Services.Economy
{
    internal interface IRemoteConfigRuntimeWrapper
    {
        IRuntimeConfigWrapper GetConfig(string key);
        void SetPlayerIdentityToken(string token);
        void FetchConfigs(string configType);
    }

    internal interface IRuntimeConfigWrapper
    {
        event Action<ConfigResponse> FetchCompleted;
        JObject config { get; }
        string GetJson(string key);
    }

    internal class RuntimeConfigSealedClassWrapper : IRuntimeConfigWrapper
    {
        RuntimeConfig m_RuntimeConfig;

        internal RuntimeConfigSealedClassWrapper(RuntimeConfig config)
        {
            m_RuntimeConfig = config;
        }
        
        public event Action<ConfigResponse> FetchCompleted
        {
            add { m_RuntimeConfig.FetchCompleted += value; }
            remove { m_RuntimeConfig.FetchCompleted -= value; }
        }

        public JObject config => m_RuntimeConfig.config;
        
        public string GetJson(string key)
        {
            return m_RuntimeConfig.GetJson(key);
        }
    }
    
    internal class RemoteConfigRuntimeNonStaticWrapper: IRemoteConfigRuntimeWrapper
    {
        struct UserAttributes {}
        struct AppAttributes {}
        
        public IRuntimeConfigWrapper GetConfig(string key)
        {
            return new RuntimeConfigSealedClassWrapper(ConfigManager.GetConfig(key));
        }

        public void SetPlayerIdentityToken(string token)
        {
            ConfigManager.SetPlayerIdentityToken(token);
        }

        public void FetchConfigs(string configType)
        {
            ConfigManager.FetchConfigs(configType, new UserAttributes(), new AppAttributes());
        }
    }
}

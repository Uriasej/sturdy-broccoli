using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Unity.Services.Lobbies.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// Here is the first point and call for accessing the Lobby Package's features!
    /// Use the .Instance method to get a singleton of the ILobbyServiceSDK, and from there you can make various requests to the Lobby service API.
    /// </summary>
    public static class Lobbies
    {
        private static ILobbyServiceSDK service;

        private static readonly Configuration configuration;

        /// <summary>
        /// Sets the configuration base path.
        /// </summary>
        /// <param name="basePath">The base path to be set for the configuration.</param>
        public static void SetBasePath(string basePath)
        {
            configuration.BasePath = basePath;
        }

        static Lobbies()
        {
            configuration = new Configuration("https://lobby.cloud.unity3d.com/v1", 10, 4, null);
        }

        /// <summary>
        /// Provides the Lobby Service SDK interface for making service API requests.
        /// </summary>
        public static ILobbyServiceSDK Instance
        {
            get
            {
                if (service == null)
                {
                    var lobbyService = LobbyService.Instance;
                    if (lobbyService == null) {
                        throw new InvalidOperationException($"Unable to get {nameof(ILobbyServiceSDK)} because Lobby API is not initialized. Make sure you call UnityServices.InitializeAsync().");
                    }
                    service = new WrappedLobbyService(LobbyService.Instance.LobbyApi, configuration);
                }
                return service;
            }
        }
    }
}

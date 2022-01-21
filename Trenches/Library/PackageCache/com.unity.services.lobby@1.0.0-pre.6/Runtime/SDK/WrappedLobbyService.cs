using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies.Apis.Lobby;
using Unity.Services.Lobbies.Http;
using Unity.Services.Lobbies.Lobby;

namespace Unity.Services.Lobbies
{
    /// <summary>
    /// The Lobby Service enables clients to create/host, join, delete lobbies using the bespoke underlying relay protocol.
    /// </summary>
    internal class WrappedLobbyService : ILobbyServiceSDK, ILobbyServiceSDKConfiguration
    {
        internal ILobbyApiClient LobbyApiClient;
        internal Configuration Configuration;

        //Minimum value of a lobby error (used to elevate standard errors if unhandled)
        internal const int LOBBY_ERROR_MIN_RANGE = 16000;

        internal WrappedLobbyService(ILobbyApiClient lobbyApiClient, Configuration configuration)
        {
            LobbyApiClient = lobbyApiClient;
            Configuration = configuration;
        }

        /// <inheritdoc/>
        public async Task<Models.Lobby> CreateLobbyAsync(string lobbyName, int maxPlayers, CreateLobbyOptions options = default)
        {
            var createRequest = new CreateRequest(
                name: lobbyName,
                maxPlayers: maxPlayers,
                isPrivate: options?.IsPrivate,
                player:  options?.Player,
                data: options?.Data
            );

            var response = await TryCatchRequest(LobbyApiClient.CreateLobbyAsync, new CreateLobbyRequest(createRequest));
            return response.Result;
        }

        /// <inheritdoc/>
        public async Task DeleteLobbyAsync(string lobbyId)
        {
            await TryCatchRequest(LobbyApiClient.DeleteLobbyAsync, new DeleteLobbyRequest(lobbyId));
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetJoinedLobbiesAsync() 
        {
            var request = new GetJoinedLobbiesRequest();
            var response = await TryCatchRequest(LobbyApiClient.GetJoinedLobbiesAsync, request);
            return response.Result;
        }

        /// <inheritdoc/>
        public async Task<Models.Lobby> GetLobbyAsync(string lobbyId)
        {
            var response = await TryCatchRequest(LobbyApiClient.GetLobbyAsync, new GetLobbyRequest(lobbyId));
            return response.Result;
        }

        /// <inheritdoc/>
        public async Task SendHeartbeatPingAsync(string lobbyId)
        {
            await TryCatchRequest(LobbyApiClient.HeartbeatAsync, new HeartbeatRequest(lobbyId));
        }

        /// <inheritdoc/>
        public async Task<Models.Lobby> JoinLobbyByCodeAsync(string lobbyCode, JoinLobbyByCodeOptions options = default)
        {
            var response = await TryCatchRequest(LobbyApiClient.JoinLobbyByCodeAsync, 
                new JoinLobbyByCodeRequest(
                    new JoinByCodeRequest(lobbyCode, options?.Player))
                );
            return response.Result;
        }

        /// <inheritdoc/>
        public async Task<Models.Lobby> JoinLobbyByIdAsync(string lobbyId, JoinLobbyByIdOptions options = default)
        {
            var response = await TryCatchRequest(LobbyApiClient.JoinLobbyByIdAsync, 
                new JoinLobbyByIdRequest(lobbyId, options?.Player));
            return response.Result;
        }

        /// <inheritdoc/>
        public async Task<QueryResponse> QueryLobbiesAsync(QueryLobbiesOptions options = default)
        {
            var queryRequest = options == null ? null : new QueryRequest(options.Count, options.Skip, options.SampleResults, options.Filters, options.Order, options.ContinuationToken);
            var queryLobbiesRequest = new QueryLobbiesRequest(queryRequest);
            var response = await TryCatchRequest(LobbyApiClient.QueryLobbiesAsync, queryLobbiesRequest);
            return response.Result;
        }

        /// <inheritdoc/>
        public async Task<Models.Lobby> QuickJoinLobbyAsync(QuickJoinLobbyOptions options = default)
        {
            var quickJoinRequest = options == null ? null : new QuickJoinRequest(options.Filter, options.Player);
            var quickJoinLobbyRequest = new QuickJoinLobbyRequest(quickJoinRequest);
            var response = await TryCatchRequest(LobbyApiClient.QuickJoinLobbyAsync, quickJoinLobbyRequest);
            return response.Result;
        }

        /// <inheritdoc/>
        public async Task RemovePlayerAsync(string lobbyId, string playerId)
        {
            var removePlayerRequest = new RemovePlayerRequest(lobbyId, playerId);
            await TryCatchRequest(LobbyApiClient.RemovePlayerAsync, removePlayerRequest);
        }

        /// <inheritdoc/>
        public async Task<Models.Lobby> UpdateLobbyAsync(string lobbyId, UpdateLobbyOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Update Lobby Options object must not be null.");
            }
            var updateRequest = options == null ? null : new UpdateRequest(options.Name, options.MaxPlayers, options.IsPrivate, options.IsLocked, options.Data, options.HostId);
            var updateLobbyRequest = new UpdateLobbyRequest(lobbyId, updateRequest);
            var response = await TryCatchRequest(LobbyApiClient.UpdateLobbyAsync, updateLobbyRequest);
            return response.Result;
        }

        /// <inheritdoc/>
        public async Task<Models.Lobby> UpdatePlayerAsync(string lobbyId, string playerId, UpdatePlayerOptions options)
        {
            if (options == null) {
                throw new ArgumentNullException(nameof(options), "Update Player Options object must not be null.");
            }
            var playerUpdateRequest = options == null ? null : new PlayerUpdateRequest(options.ConnectionInfo, options.Data, options.AllocationId);
            var updatePlayerRequest = new UpdatePlayerRequest(lobbyId, playerId, playerUpdateRequest);
            var response = await TryCatchRequest(LobbyApiClient.UpdatePlayerAsync, updatePlayerRequest);
            return response.Result;
        }

        public void SetBasePath(string basePath)
        {
            Configuration.BasePath = basePath;
        }

        #region Helper Functions

        // Helper function to reduce code duplication of try-catch
        private async Task<Response> TryCatchRequest<TRequest>(Func<TRequest, Configuration, Task<Response>> func, TRequest request) 
        {
            Response response = null;
            try
            {
                response = await func(request, Configuration);
            }
            catch (HttpException<ErrorStatus> he)
            {
                ResolveErrorWrapping((LobbyExceptionReason)he.ActualError.Code, he);
            }
            catch (HttpException he)
            {
                int httpErrorStatusCode = (int)he.Response.StatusCode;
                LobbyExceptionReason reason = LobbyExceptionReason.Unknown;
                if (he.Response.IsNetworkError)
                {
                    reason = LobbyExceptionReason.NetworkError;
                }
                else if (he.Response.IsHttpError)
                {
                    //Elevate unhandled http codes to lobby enum range
                    if (httpErrorStatusCode < 1000)
                    {
                        httpErrorStatusCode += LOBBY_ERROR_MIN_RANGE;
                        if (Enum.IsDefined(typeof(LobbyExceptionReason), httpErrorStatusCode))
                        {
                            reason = (LobbyExceptionReason)httpErrorStatusCode;
                        }
                    }
                }

                ResolveErrorWrapping(reason, he);
            }
            catch (Exception e)
            {
                //Pass error code that will throw default label, provide exception object for stack trace.
                ResolveErrorWrapping(LobbyExceptionReason.Unknown, e);
            }
            return response;
        }

        // Helper function to reduce code duplication of try-catch (generic version)
        private async Task<Response<TReturn>> TryCatchRequest<TRequest, TReturn>(Func<TRequest, Configuration, Task<Response<TReturn>>> func, TRequest request)
        {
            Response<TReturn> response = null;
            try
            {
                response = await func(request, Configuration);
            }
            catch (HttpException<ErrorStatus> he) 
            {
                ResolveErrorWrapping((LobbyExceptionReason) he.ActualError.Code, he);
            }
            catch (HttpException he)
            {
                int httpErrorStatusCode = (int) he.Response.StatusCode;
                LobbyExceptionReason reason = LobbyExceptionReason.Unknown;
                if (he.Response.IsNetworkError)
                {
                    reason = LobbyExceptionReason.NetworkError;
                }
                else if (he.Response.IsHttpError)
                {
                    //Elevate unhandled http codes to lobby enum range
                    if (httpErrorStatusCode < 1000)
                    {
                        httpErrorStatusCode += LOBBY_ERROR_MIN_RANGE;
                        if (Enum.IsDefined(typeof(LobbyExceptionReason), httpErrorStatusCode)) 
                        {
                            reason = (LobbyExceptionReason) httpErrorStatusCode;
                        }
                    }
                }

                ResolveErrorWrapping(reason, he);
            }
            catch (Exception e)
            {
                //Pass error code that will throw default label, provide exception object for stack trace.
                ResolveErrorWrapping(LobbyExceptionReason.Unknown, e);
            }
            return response;
        }

        // Helper function to resolve the new wrapped error/exception based on input parameter
        private void ResolveErrorWrapping(LobbyExceptionReason reason, Exception exception = null) 
        {
            if (reason == LobbyExceptionReason.Unknown)
            {
                throw new LobbyServiceException(reason, "Something went wrong.", exception);
            }
            else 
            {
                //Check if the exception is of type HttpException<ErrorStatus> - extract api user-facing message
                HttpException<ErrorStatus> apiException = exception as HttpException<ErrorStatus>;
                if (apiException != null)
                {
                    throw new LobbyServiceException(reason, apiException.ActualError.Detail, apiException);
                }
                else 
                {
                    //Other general exception message handling
                    throw new LobbyServiceException(reason, exception.Message, exception);
                }
            }
        }
        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using QFSW.QC;

public class TestLobby : MonoBehaviour
{
    private float heartBeatTimer;
    private Lobby hostLobby;
    private float heartBeatTimerMax;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    private void Update()
    {

    }
    private async void HandleLobbyHeartBeat()
    {
        if (hostLobby != null)
            heartBeatTimer -= Time.deltaTime;
        if (heartBeatTimer < 0f)
        {
            heartBeatTimerMax = 15f;
            heartBeatTimer = heartBeatTimerMax;
            await LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
        }

    }
    [Command]
    private async void CreateLobby()
    {
        try
        {
            string lobbyName = "my lobby";
            int maxPlayers = 4;

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers);
            hostLobby = lobby;
            Debug.Log("Created " + lobby.Name + " " + lobby.MaxPlayers);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    [Command]
    private async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>{
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots,"0",QueryFilter.OpOptions.GT)
        },
                Order = new List<QueryOrder>{
            new QueryOrder(false,QueryOrder.FieldOptions.Created)
        }
            };
            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("ListLobbies " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers);
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e);
        }
    }
    private void JoinLobby()
    {
        // Lobbies.Instance.JoinLobbyByIdAsync();
    }

}

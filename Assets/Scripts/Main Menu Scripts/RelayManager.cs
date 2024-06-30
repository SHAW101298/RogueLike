using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
    #region
    public static RelayManager Instance;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion


    [SerializeField] LobbyManager lobbyManager;


    // Update is called once per frame
    public async Task<string> CreateRelay()
    {
        string joinCode = "";
        try
        {
            int maxPlayers = lobbyManager.currentLobby.MaxPlayers - 1;
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayers);
             joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new RelayServerData(allocation,"dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();

        }
        catch(RelayServiceException e)
        {
            Debug.Log(e);
            return "";
        }
        return joinCode;
    }
    public async void JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();

        }
        catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}

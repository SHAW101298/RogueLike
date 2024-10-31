using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTypeController : MonoBehaviour
{
    [SerializeField] UnityTransport relayTransport;
    [SerializeField] UnityTransport unityTransport;


    public void SetAsRelayTransport()
    {
        Debug.Log("Setting As Relay");
        NetworkManager.Singleton.NetworkConfig.NetworkTransport = relayTransport;
    }
    public void SetAsUnityTransport()
    {
        Debug.Log("Setting As Unity");
        NetworkManager.Singleton.NetworkConfig.NetworkTransport = unityTransport;
    }
}

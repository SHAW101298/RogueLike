using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class UI_MainMenu : MonoBehaviour
{
    public NetworkManager networkManager;
    public UnityTransport transport;
    public GameObject MenuCanvas;
    [Space(10)]
    public GameObject ipWindow;
    public Text ipField;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BTN_StartHost()
    {
        networkManager.StartHost();
    }
    public void BTN_JoinGame()
    {
        networkManager.StartClient();
    }

    public void BTN_Join()
    {
    }
}

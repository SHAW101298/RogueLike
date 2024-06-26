using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    [SerializeField] UI_Window menuWindow;
    [SerializeField] UI_Window playWindow;
    [SerializeField] UI_Window optionsWindow;
    [SerializeField] UI_Window exitWindow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BTN_Play()
    {
        playWindow.OpenWindow();
    }
    public void BTN_Options()
    {

    }

    public void BTN_Exit()
    {
    }
}

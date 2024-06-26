using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Window : MonoBehaviour
{
    [SerializeField] UI_Window parent;

    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
        if(parent == null)
        {
            return;
        }
            parent.OpenWindow();
    }
}

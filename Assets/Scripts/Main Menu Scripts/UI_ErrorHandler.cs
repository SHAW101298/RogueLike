using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_ErrorHandler : MonoBehaviour
{
    #region
    public static UI_ErrorHandler instance;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public GameObject errorprefab;
    public GameObject parent;

    public void ShowErrorMessage(string text)
    {
        GameObject tempGO = Instantiate(errorprefab);
        UI_ErrorMessage tempMessage = tempGO.GetComponent<UI_ErrorMessage>();
        tempGO.transform.SetParent(parent.transform);
        tempGO.transform.localScale = Vector3.one;
        tempGO.transform.localPosition = Vector3.zero;
        tempMessage.ShowErrorMessage(text);
    }

}

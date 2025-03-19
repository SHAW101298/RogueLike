using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DamageDealtVisualizer : MonoBehaviour
{
    #region
    public static UI_DamageDealtVisualizer Instance;
    private void Awake()
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
    public GameObject prefab;
    public GameObject worldSpaceCanvas;

    public Color[] colors;

    PlayerData localPlayer;
    public void ShowDamageNumber(Vector3 pos, float damage, ENUM_DamageType type)
    {
        // Rotate canvas with player
        worldSpaceCanvas.transform.localEulerAngles = localPlayer.transform.localEulerAngles;

        // Create and Position object properly
        GameObject temp = Instantiate(prefab);
        temp.transform.SetParent(worldSpaceCanvas.transform);
        temp.transform.position = pos;
        float rand = Random.Range(-1f, 1f);
        Vector3 randPos = new Vector3(1, 0.1f, 0);
        randPos.x = rand;
        //Debug.Log("randpos = " + randPos);
        temp.transform.localPosition += randPos;
        temp.transform.localEulerAngles = Vector3.zero;

        // Set text Data
        UI_DamageAnimation animation = temp.GetComponent<UI_DamageAnimation>();
        animation.textField.text = damage.ToString();
        animation.textField.color = colors[(int)type];
    }
    public void SetPlayer(PlayerData player)
    {
        localPlayer = player;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Character : MonoBehaviour
{
    public int index;

    public void BTN_ChooseCharacter()
    {
        CharactersList.Instance.ChooseCharacter(index);
    }
}

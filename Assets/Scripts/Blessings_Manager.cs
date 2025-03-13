using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessings_Manager : MonoBehaviour
{
    #region
    public static Blessings_Manager Instance;

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

    public List<Blessing_Base> blessings;
    public GameObject blessingPickUpPrefab;

    public Blessing_Base GetRandomBlessing()
    {
        int rand = Random.Range(0, blessings.Count);
        return blessings[rand];
    }
    public Blessing_Base GetBlessing(int id)
    {
        return blessings[id];
    }
    public void CreateRandomBlessingOnGround(Vector3 pos)
    {
        GameObject temp = Instantiate(blessingPickUpPrefab);
        temp.transform.position = pos;
        AltarOfBlessings newAltar = temp.GetComponent<AltarOfBlessings>();
        newAltar.GenerateBlessing();
    }
}

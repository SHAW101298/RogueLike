using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    public ProjectileBehaviour projectileBehaviour;
    public BulletLifeTime bulletLifeTime;
    public Gun owningGun;
    public UnitData owningUnit;
    [SerializeField] public BulletInfo bulletInfo;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFloatingScript : MonoBehaviour
{
    [SerializeField] GameObject objectToWobble;
    [SerializeField] Vector3 baseRotation = new Vector3(0,0,-20f);
    [SerializeField] Vector3 rotateSpeed = new Vector3(0, 0.1f, 0);
    [SerializeField] float boopingSpeed = 0.05f;
    [SerializeField] float boopingHeight = 0.07f;
    [SerializeField] Vector3 boopingDir = Vector3.up;
    [SerializeField] Vector3 basePos;



    private void Start()
    {
        basePos = objectToWobble.transform.position;
        //transform.localEulerAngles = baseRotation;
    }
    private void Update()
    {
        WobbleAnim();
    }
    void WobbleAnim()
    {
        objectToWobble.transform.Translate(boopingSpeed * Time.deltaTime * boopingDir);
        objectToWobble.transform.Rotate(rotateSpeed);
        if (objectToWobble.transform.position.y >= basePos.y + boopingHeight || objectToWobble.transform.position.y <= basePos.y - boopingHeight)
        {
            boopingDir *= -1;
        }

    }
}

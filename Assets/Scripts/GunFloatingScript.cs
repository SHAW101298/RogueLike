using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFloatingScript : MonoBehaviour
{
    [SerializeField] Vector3 rotateSpeed = new Vector3(0, 0.1f, 0);
    [SerializeField] float boopingSpeed = 0.05f;
    [SerializeField] float boopingHeight = 0.07f;
    [SerializeField] Vector3 boopingDir = Vector3.up;
    [SerializeField] Vector3 basePos;



    private void Start()
    {
        basePos = transform.position;
    }
    private void Update()
    {
        WobbleAnim();
    }
    void WobbleAnim()
    {
        transform.Translate(boopingSpeed * Time.deltaTime * boopingDir);
        transform.Rotate(rotateSpeed);
        if (transform.position.y >= basePos.y + boopingHeight || transform.position.y <= basePos.y - boopingHeight)
        {
            boopingDir *= -1;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : NetworkBehaviour
{
    public GameObject body;
    public GameObject camera;

    public float sensitivity = 3f;
    public Vector2 input;
    Vector3 rot;
    public float x;
    public float y;

    public void ActionLook(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        //Debug.Log(context.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner == false)
        {
            return;
        }
        RotateBody();
        //Rotate();
    }
    private void LateUpdate()
    {
        if(IsOwner == false)
        {
            return;
        }
        RotateCam();
    }

    void Rotate()
    {
        rot = body.transform.localRotation.eulerAngles;
        Vector3 movement = new Vector3(0, input.x, 0);
        rot += movement * sensitivity * Time.deltaTime;
        body.transform.localEulerAngles = rot;

        rot = camera.transform.localRotation.eulerAngles;
        movement = new Vector3(-input.y, 0, 0);
        rot += movement * sensitivity * Time.deltaTime;
        camera.transform.localEulerAngles = rot;
        //characterModel.transform.localEulerAngles = rot;
    }
    void Rotate2()
    {
        rot = body.transform.localRotation.eulerAngles;
        Vector3 movement = new Vector3(-input.y, input.x, 0);
        rot += movement * (sensitivity / 10);
        body.transform.localEulerAngles = rot;
    }
    void RotateBody()
    {
        x = input.x * sensitivity * Time.deltaTime;
        y = input.y * sensitivity * Time.deltaTime;
        Vector3 rotateValue = new Vector3(0, -x, 0);
        body.transform.eulerAngles = body.transform.eulerAngles - rotateValue;
    }
    void RotateCam()
    {
        Vector3 rotateValue = new Vector3(y, -x, 0);
        camera.transform.eulerAngles = camera.transform.eulerAngles - rotateValue;
    }
    void Rotate3()
    {
        x = input.x * sensitivity * Time.deltaTime;
        y = input.y * sensitivity * Time.deltaTime;
        Vector3 rotateValue = new Vector3(0,x, 0);
        body.transform.eulerAngles = body.transform.eulerAngles - rotateValue;

        
    }
}

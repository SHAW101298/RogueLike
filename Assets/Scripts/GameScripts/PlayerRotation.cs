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

    public bool blockedRotation { get; private set; }

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
        if (blockedRotation == true)
            return;

        RotateBody();
        //Rotate();
    }

    /*
    void Rotate()
    {
        rot = body.transform.localRotation.eulerAngles;
        Vector3 movement = new Vector3(0, input.x, 0);
        rot += movement * (sensitivity/10);
        body.transform.localEulerAngles = rot;

        rot = camera.transform.localRotation.eulerAngles;
        movement = new Vector3(-input.y, 0, 0);
        rot += movement * (sensitivity/10);
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
    */
    void RotateBody()
    {
        x = input.x * sensitivity/10;
        y = input.y * sensitivity/10;
        Vector3 rotateValue = new Vector3(0, -x, 0);
        body.transform.eulerAngles = body.transform.eulerAngles - rotateValue;
    }

    public void BlockRotation()
    {
        blockedRotation = true;
    }
    public void AllowRotation()
    {
        blockedRotation = false;
    }
}

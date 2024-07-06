using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : NetworkBehaviour
{
    public GameObject body;
    public GameObject camera;
    public GameObject characterModel;

    public float sensitivity = 0.5f;
    Vector2 input;
    Vector3 rot;

    public void ActionLook(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        //Debug.Log(context.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner == false)
            return;
        Rotate();
    }

    void Rotate()
    {
        rot = body.transform.localRotation.eulerAngles;
        Vector3 movement = new Vector3(0, input.x, 0);
        rot += movement * (sensitivity / 10);
        body.transform.localEulerAngles = rot;

        rot = camera.transform.localRotation.eulerAngles;
        movement = new Vector3(-input.y, 0, 0);
        rot += movement * (sensitivity / 10);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation2 : MonoBehaviour
{
    public GameObject body;
    public GameObject camera;

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
        Rotate();
    }

    void Rotate()
    {
        rot = body.transform.localRotation.eulerAngles;
        Vector3 movement = new Vector3(0, input.x,0);
        rot += movement * (sensitivity / 10);
        body.transform.localEulerAngles = rot;

        rot = camera.transform.localRotation.eulerAngles;
        movement = new Vector3(-input.y, 0, 0);
        rot += movement * (sensitivity / 10);
        camera.transform.localEulerAngles = rot;
    }

    public void EscapeButton(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Debug.LogWarning("MOVE ME SOMEWHERE ELSE");
            Debug.Break();
        }
        
    }
}

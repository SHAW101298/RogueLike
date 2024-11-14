using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHookUp : MonoBehaviour
{
    #region
    public static CameraHookUp Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Debug.Log("CAMERA HOOKUP");
            Instance = this;
        }
    }
#endregion
    public GameObject cam;
    public GameObject player;
    [SerializeField] PlayerRotation input;
    public Vector3 positionOnPlayer;
    [SerializeField] float sensitivity = 3;
    float x, y;
    [Space(15)]
    public Transform forwardPos;
   

    private void Update()
    {
        x = input.input.x;
        y = input.input.y;
        UpdatePosition();
    }
    private void LateUpdate()
    {
        RotateCam();
    }

    public void Attach(GameObject playerObject)
    {
        player = playerObject;
        input = player.gameObject.GetComponent<PlayerRotation>();
    }

    void RotateCam()
    {
        //x *= Time.deltaTime * sensitivity;
        x *= sensitivity/10;
        y *= sensitivity/10;
        //y *= Time.deltaTime * sensitivity;
        Vector3 rotateValue = new Vector3(y, -x, 0);
        cam.transform.eulerAngles = cam.transform.eulerAngles - rotateValue;
    }
    void UpdatePosition()
    {
        cam.transform.position = player.transform.position + positionOnPlayer;
    }
}

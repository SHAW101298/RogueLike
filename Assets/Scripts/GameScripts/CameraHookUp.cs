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
    public GameObject cameraTarget;
    public GameObject handsObject;
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
        //UpdatePosition();
    }
    private void LateUpdate()
    {
        UpdatePosition();
        RotateCam();
    }

    public void Attach(GameObject playerObject)
    {
        player = playerObject;
        PlayerData playerData = playerObject.GetComponent<PlayerData>();
        input = playerData.rotation;
        cameraTarget = playerData.characterData.cameraTarget;
        handsObject = playerData.characterData.handsObject;
    }

    void RotateCam()
    {
        //x *= Time.deltaTime * sensitivity;
        x *= sensitivity/10;
        y *= sensitivity/10;
        //input.body.transform.eulerAngles.y;
        //y *= Time.deltaTime * sensitivity;

        Vector3 rotation = Vector3.zero;
        rotation.x = cam.transform.eulerAngles.x;
        rotation.y = input.body.transform.eulerAngles.y;
        rotation.x -= y;
        //Vector3 rotateValue = new Vector3(y, -x, 0);
        cam.transform.eulerAngles = rotation;
        handsObject.transform.localEulerAngles = new Vector3(rotation.x,0,0);
    }
    void UpdatePosition()
    {
        cam.transform.position = cameraTarget.transform.position;
    }
}

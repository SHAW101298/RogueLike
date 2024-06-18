using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunManagement : MonoBehaviour
{
    public List<Gun> possesedGuns;
    public Gun selectedGun;
    public Transform nozzlePosition;


    public bool AttemptGunChange(Gun newGun)
    {
        if (selectedGun == possesedGuns[0])
        {
            return false;
        }

        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

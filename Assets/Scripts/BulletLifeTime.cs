using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeTime : MonoBehaviour
{
    public float lifespan;
    float timer;
    bool activateMesh;
    [SerializeField] MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifespan)
        {
            Destroy(gameObject);
        }

        if (activateMesh == true)
        {
            meshRenderer.enabled = true;
        }

        if(timer >= 0.05f)
        {
            activateMesh = true;
        }
    }
}

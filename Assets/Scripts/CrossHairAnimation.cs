using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairAnimation : MonoBehaviour
{
    public Transform crosshair;
    public Vector3 scaleModifier;
    public Vector3 maxScale;
    [Header("WindTime")]
    [SerializeField] float upTime;
    [SerializeField] float downTime;
    
    bool animateUp;
    bool animateDown;
    float timerUp;
    float timerDown;

    Vector3 savedScale;
    Vector3 desiredScale;

    [Header("Debug")]
    public bool trigger;

    // Update is called once per frame
    void Update()
    {
        if(trigger == true)
        {
            animateUp = true;
            savedScale = crosshair.localScale;
            desiredScale = savedScale + scaleModifier;

            if(desiredScale.x > maxScale.x)
            {
                desiredScale = maxScale;
            }
            trigger = false;
        }

        AnimateUp();
        AnimateDown();
    }

    public void TriggerShoot()
    {
        animateUp = true;
        savedScale = crosshair.localScale;
        desiredScale = savedScale + scaleModifier;

        if (desiredScale.x > maxScale.x)
        {
            desiredScale = maxScale;
        }
        trigger = false;
    }
    void AnimateUp()
    {
        if (animateUp == true)
        {
            animateDown = false;

            timerUp += Time.deltaTime;
            
            float percentage = timerUp / upTime;
            crosshair.localScale = Vector3.Lerp(savedScale, desiredScale, percentage);

            if (timerUp > upTime)
            {
                timerDown = 0;
                timerUp = 0;
                animateUp = false;
                animateDown = true;

                savedScale = crosshair.localScale;
                desiredScale = Vector3.one / 2;
            }
        }
    }
    void AnimateDown()
    {
        if(animateDown == true)
        {
            animateUp = false;
            timerDown += Time.deltaTime;

            float percentage = timerDown / downTime;
            crosshair.localScale = Vector3.Lerp(savedScale, desiredScale, percentage);

            if (timerDown > downTime)
            {
                timerDown = 0;
                timerUp = 0;
                animateDown = false;
            }
        }
    }


}

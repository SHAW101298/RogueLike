using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DamageAnimation : MonoBehaviour
{
    public TMP_Text textField;
    [SerializeField] float minTextSize;
    [SerializeField] float timeVisible;
    [SerializeField] float sizeDecreaseSpeed;
    bool animate;

    float timer;


    public void AnimationStart()
    {
        animate = true;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeVisible)
        {
            Destroy(gameObject);
        }

        textField.fontSize -= Time.deltaTime * sizeDecreaseSpeed;
        if (textField.fontSize <= minTextSize)
        {
            textField.fontSize = minTextSize;
            return;
        }
    }
}

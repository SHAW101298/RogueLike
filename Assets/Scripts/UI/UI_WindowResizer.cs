using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WindowResizer : MonoBehaviour
{
    [SerializeField] List<UI_WindowSizeCalculator> objects;
    [SerializeField] float distanceBetweenObjects;
    public RectTransform rectTransform;

    public void Calculate()
    {
        CollectChildren();

        float height = 0;
        for(int i = 0; i < objects.Count; i++)
        {
            objects[i].rectTransform.anchoredPosition = new Vector2()
            {
                x = 0,
                y = -height
            };

            objects[i].CalculateAndSetHeight();
            height += objects[i].rectTransform.sizeDelta.y;
            height += distanceBetweenObjects;
        }
        height -= distanceBetweenObjects;
        Vector2 newSize = new Vector2(rectTransform.sizeDelta.x, height);
        rectTransform.sizeDelta = newSize;
    }
    public void CollectChildren()
    {
        objects.Clear();
        Debug.Log("Child number = " + transform.childCount);
        for(int i = 0; i < transform.childCount; i++)
        {
            objects.Add(transform.GetChild(i).gameObject.GetComponent<UI_WindowSizeCalculator>());
        }
    }
}

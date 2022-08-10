using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVerticalFitter : MonoBehaviour
{
    public float offset_y;
    RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        float delta_y = offset_y;
        RectTransform[] rects = GetComponentsInChildren<RectTransform>();
        for(int i=1; i<rects.Length; i++)
        {
            delta_y += rects[i].rect.height;
        }
        Debug.Log(delta_y);
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, delta_y);
    }
}

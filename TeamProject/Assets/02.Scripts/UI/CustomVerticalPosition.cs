using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVerticalPosition : MonoBehaviour
{
    [SerializeField] RectTransform rect, prevRect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        RectTransform _parent = gameObject.GetComponentsInParent<RectTransform>()[1];
        RectTransform[] _rects = _parent.GetComponentsInChildren<RectTransform>();
        for(int i=1; i<_rects.Length; i++)
        {
            if(rect == _rects[i])
            {
                if(i>1)
                    prevRect = _rects[i - 1];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(prevRect!=null)
            rect.position = new Vector3(rect.position.x, prevRect.position.y - prevRect.rect.height, rect.position.z);
    }
}

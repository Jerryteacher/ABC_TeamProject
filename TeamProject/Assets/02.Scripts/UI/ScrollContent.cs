using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContent : MonoBehaviour
{
    [SerializeField] RectTransform viewportTr;
    [SerializeField] RectTransform contentTr;

    Vector2 midDir;
    Vector2 topDir;
    // Start is called before the first frame update
    void Start()
    {
        contentTr = GetComponent<RectTransform>();
        viewportTr = GetComponentsInParent<RectTransform>()[1];
        //Debug.Log("content: " + contentTr.rect.height);
        //Debug.Log("viewportTr: " + viewportTr.rect.height);

        midDir = new Vector2(0.5f, 0.5f);
        topDir = new Vector2(0.5f, 1f);
    }

    // 테스트용
    // 최적화를 위해, 컨텐츠가 변경될 때마다 UpdateScroll()함수를 호출하는 방식으로 사용
    private void Update()
    {
        UpdateScroll();
    }

    //스크롤 컨텐츠 크기에 따른 앵커, 피봇 변화
    public void UpdateScroll()
    {
        if (contentTr.rect.height >= viewportTr.rect.height)
        {
            contentTr.anchorMin = topDir;
            contentTr.anchorMax = topDir;
            contentTr.pivot = topDir;
        }
        else
        {
            contentTr.anchorMin = midDir;
            contentTr.anchorMax = midDir;
            contentTr.pivot = midDir;
        }
    }
}

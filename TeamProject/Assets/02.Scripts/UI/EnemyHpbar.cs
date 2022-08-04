using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpbar : MonoBehaviour
{
    public Vector3 Offset;
    [SerializeField] GameObject HpbarPrefab;
    RectTransform Hpbar;
    [SerializeField] RectTransform rectParent;
    //public Camera HpCam;

    bool isShow;
    public bool IsShow
    {
        get
        {
            return isShow;
        }
        set
        {
            if(value != isShow)
            {
                if (value == true)
                    ShowHpbar(true);
                else ShowHpbar(false);
            }
            isShow = value;
        }
    }
    
    void Start()
    {
        rectParent = GameObject.Find("MainUICanvas").transform.GetChild(0).GetComponent<RectTransform>();

        HpbarPrefab = Resources.Load<GameObject>("Prefab/UI/EnemyHpbar");
    }

    private void LateUpdate()
    {
        if(IsShow)
        {
            var ScreenPos = Camera.main.WorldToScreenPoint(transform.position + Offset);
            if (ScreenPos.z < 0.0f)
            {
                ScreenPos *= -1;
            }
            //var localPos = Vector2.zero;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, ScreenPos, HpCam, out localPos);
            //Hpbar.rectTransform.localPosition = localPos;
            Hpbar.position = ScreenPos;
            float maxHp = GetComponent<EnemyHealth>().MaxHp;
            float curHp = GetComponent<EnemyHealth>().Hp;
            Hpbar.GetComponent<Image>().fillAmount = Mathf.Clamp((curHp / maxHp), 0, 1);
        }
    }

    void ShowHpbar(bool IsShow)
    {
        if (IsShow)

            Hpbar = Instantiate(HpbarPrefab, rectParent).GetComponent<RectTransform>();
        else
        {
            //Debug.Log("Called Destroy");
            Destroy(Hpbar.gameObject);
        }
        
        //Hpbar.color = new Color(0, 255, 0, IsShow ? 255 : 0);
    }
}

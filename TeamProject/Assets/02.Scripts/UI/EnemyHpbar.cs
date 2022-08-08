using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpbar : MonoBehaviour
{
    public Vector3 Offset;
    Vector3 txtDmgOffset;
    [SerializeField] GameObject HpbarPrefab;
    [SerializeField] GameObject DamageTextPrefab;
    RectTransform Hpbar;
    [SerializeField] RectTransform rectParent;
    //public Camera HpCam;

    //Damaged Text
    List<Text> txtDmg = new List<Text>();
    List<Vector3> txtDmgTr = new List<Vector3>();
    List<float> txtDmgTrDeltaTime = new List<float>();
    float txtDmgTrDelta = 35f;

    //Hpbar
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
        DamageTextPrefab = Resources.Load<GameObject>("Prefab/UI/DamageText");
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
        if (txtDmg.Count > 0)
        {
            for (int i = 0; i < txtDmg.Count; i++)
            {
                var ScreenPos = Camera.main.WorldToScreenPoint(txtDmgTr[i] + txtDmgOffset);
                ScreenPos.y += txtDmgTrDelta * txtDmgTrDeltaTime[i];
                txtDmg[i].color = new Color(255, 0, 0, 1 - txtDmgTrDeltaTime[i]);
                txtDmgTrDeltaTime[i] += Time.deltaTime;
                if (ScreenPos.z < 0.0f)
                {
                    ScreenPos *= -1;
                }
                txtDmg[i].transform.position = ScreenPos;
            }
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
    public void ShowDamage(float dmg)
    {
        Text _txt = Instantiate(DamageTextPrefab, rectParent).GetComponent<Text>();
        _txt.text = dmg.ToString("-0");
        txtDmg.Add(_txt);
        txtDmgTr.Add(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        txtDmgTrDeltaTime.Add(0);
        StartCoroutine(delayDamage(txtDmg[txtDmg.Count - 1]));
    }

    IEnumerator delayDamage(Text txt)
    {
        //txt.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1);
        //txt.color = new Color(0, 0, 0, 0);
        int idx = txtDmg.IndexOf(txt);
        txtDmg.RemoveAt(idx);
        txtDmgTr.RemoveAt(idx);
        txtDmgTrDeltaTime.RemoveAt(idx);
        Destroy(txt.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //싱글턴
    static UIManager instance;

    //HP, SP, EXP
    [SerializeField] Image Hpbar, Spbar, Expbar;
    [SerializeField] Text txtHp, txtSp, txtExp;

    [Space]
    // 상호작용
    [SerializeField] GameObject interItem;
    [SerializeField] Text txtInterName, txtInterKey, txtInterType;

    ChatUICtrl chatUI;

    public bool IsChatUIActive { get; protected set; }

    public static UIManager getInstance
    {
        get
        {
            if (instance == null)
                instance = (UIManager)FindObjectOfType(typeof(UIManager));
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Hp, Sp, Exp 초기화
        {
            Transform _tr = GameObject.Find("Health Point BG").transform;
            Hpbar = _tr.GetChild(0).GetComponent<Image>();
            txtHp = _tr.GetChild(1).GetComponent<Text>();
            _tr = GameObject.Find("Stamina Point BG").transform;
            Spbar = _tr.GetChild(0).GetComponent<Image>();
            txtSp = _tr.GetChild(1).GetComponent<Text>();
            _tr = GameObject.Find("Experience Point BG").transform;
            Expbar = _tr.GetChild(0).GetComponent<Image>();
            txtExp = _tr.GetChild(1).GetComponent<Text>();
        }
        // 상호작용 초기화
        {
            interItem = GameObject.Find("InterItem");
            txtInterName = interItem.transform.GetChild(0).GetChild(0).GetComponent<Text>();
            txtInterKey = interItem.transform.GetChild(1).GetComponent<Text>();
            txtInterType = interItem.transform.GetChild(2).GetComponent<Text>();

            interItem.SetActive(false);
        }
        //Chat Dialog 초기화
        {
            chatUI = GameObject.Find("Chat Dialog").GetComponent<ChatUICtrl>();
            ShowChatDialog(false);
        }
        //UpdateHealth(200, 100);
        //UpdateStamina(100, 80);
        //UpdateExperience(12345, 123);

        //SetInterKey("Left Shift");
        //SetInteractionItem("던전 진입", InteractionType.MOVE);
    }


    // 체력 UI 업데이트
    // maxHp에 0이 안들어가도록 조심할 것
    public void UpdateHealth(int maxHp, int curHp)
    {
        float HpPer = (float)curHp / maxHp;
        Hpbar.fillAmount = HpPer;
        txtHp.text = curHp + "/" + maxHp;
    }
    // 스테미나 UI 업데이트
    // maxSp에 0이 안들어가도록 조심할 것
    public void UpdateStamina(int maxSp, int curSp)
    {
        float SpPer = (float)curSp / maxSp;
        Spbar.fillAmount = SpPer;
        txtSp.text = curSp + "/" + maxSp;
    }
    // 경험치 UI 업데이트
    // maxExp에 0이 안들어가도록 조심할 것
    public void UpdateExperience(int maxExp, int curExp)
    {
        float ExpPer = (float)curExp / maxExp;
        Expbar.fillAmount = ExpPer;
        txtExp.text = curExp + "/" + maxExp;
    }


    // 상호작용 UI 표시
    public void ShowInteraction(bool isShow)
    {
        interItem.SetActive(isShow);
    }

    // 상호작용 키 UI 값 설정
    public void SetInterKey(string key)
    {
        txtInterKey.text = key;
    }

    // 상호작용 대상 UI 값 설정
    public void SetInteractionItem(string name, InteractionType type)
    {
        //if (interItem.activeSelf == false)
        //    interItem.SetActive(true);
        txtInterName.text = name;
        switch (type)
        {
            case InteractionType.INTERACTION:
                txtInterType.text = "상호작용"; break;
            case InteractionType.LOOT:
                txtInterType.text = "줍 기"; break;
            case InteractionType.CHAT:
                txtInterType.text = "대화하기"; break;
            case InteractionType.MOVE:
                txtInterType.text = "이동하기"; break;
        }
    }

    public void SetChatDialog(InteractionChat chat, int id, bool isNpc)
    {
        ShowChatDialog(true);
        chatUI.Talk(chat, id, isNpc);
    }

    public void ReqestNextChat()
    {
        chatUI.ReqestNext();
    }

    public void ShowChatDialog(bool IsShow)
    {
        IsChatUIActive = IsShow;
        ShowInterItem(!IsShow);
        chatUI.GetComponent<CanvasGroup>().alpha = IsShow ? 1 : 0;
        chatUI.GetComponent<CanvasGroup>().blocksRaycasts = IsShow;
    }
    void ShowInterItem(bool IsShow)
    {
        interItem.GetComponent<CanvasGroup>().alpha = IsShow ? 1 : 0;
    }

}

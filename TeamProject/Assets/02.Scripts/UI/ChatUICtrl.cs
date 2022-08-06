using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChatUICtrl : MonoBehaviour, IPointerClickHandler
{
    public string NPCName;
    List<string> detail = new List<string>();
    int idx = 0;
    bool triggerSkip = false;
    bool MsgEnd = false;

    Image imgNPC;
    Text txtNpcName;
    Text txtDetail;
    //Button btnNext;
    //Button btnApply;
    //Button btnCancel;

    //public bool NeedRequest;

    InteractionChat chat;

    public int chatID;
    // Start is called before the first frame update
    void Start()
    {
        imgNPC = transform.GetChild(0).GetComponent<Image>();
        txtNpcName = transform.GetChild(1).GetComponent<Text>();
        txtDetail = transform.GetChild(2).GetComponent<Text>();
        //btnNext = transform.GetChild(3).GetComponent<Button>();
        //btnApply = transform.GetChild(3).GetComponent<Button>();
        //btnCancel = transform.GetChild(4).GetComponent<Button>();
    }

    public void Talk(InteractionChat chat, int id, bool isNpc)
    {
        this.chat = chat;
        int questTalkIndex = QuestManager.getInstance.GetQuestTalkIndex(id);
        //Debug.Log(id);
        //Debug.Log(questTalkIndex);
        idx = 0;
        detail.Clear();
        for (int i = 0; i < 100; i++)
        {
            string _str = TalkManager.getInstance.GetTalk(id + questTalkIndex, i);
            Debug.Log(_str);
            if (_str == null)
                break;
            detail.Add(_str);

        }

        QuestManager.getInstance.CheckQuest(id);

        //Debug.Log("CheckQuest Finish");
        if (isNpc)
        {
            imgNPC.sprite = TalkManager.getInstance.GetPortrait(id, int.Parse(detail[0].Split(':')[1]));
            imgNPC.color = new Color(255, 255, 255, 1);
            for (int i = 0; i < detail.Count; i++)
                detail[i] = detail[i].Split(':')[0];
        }
        else
        {
            imgNPC.color = new Color(255, 255, 255, 0);
        }




        //btnApply.gameObject.SetActive(false);
        //btnCancel.gameObject.SetActive(false);

        //txtNpcName.text = GetChatNPC(chatID);
        //detail = GetChatMsg(chatID);
        //NeedRequest = GetChatIsRequest(chatID);

        //txtNpcName.text = "Test";
        //detail.Add("test msg");
        //NeedRequest = true;

        if (detail.Count > 0)
            OnClickNext();
    }

    public void OnClickNext()
    {
        triggerSkip = false;
        //btnNext.gameObject.SetActive(false);
        StartCoroutine(TypingText(detail[idx++]));
    }

    IEnumerator TypingText(string msg)
    {
        for (int i = 1; i <= msg.Length; i++)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            if (triggerSkip)
            {
                i = msg.Length;
                //triggerSkip = false;
            }
            txtDetail.text = msg.Substring(0, i);
        }
        //if (idx < detail.Count)
        //     btnNext.gameObject.SetActive(true);
        //else
        //{
        //    btnNext.gameObject.SetActive(false);
        //    if (NeedRequest)
        //    {
        //        btnApply.gameObject.SetActive(true);
        //        btnCancel.GetComponentInChildren<Text>().text = "거절";
        //    }
        //    btnCancel.GetComponentInChildren<Text>().text = "닫기";
        //    btnCancel.gameObject.SetActive(true);
        // }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        triggerSkip = true;
    }

    public void ReqestNext()
    {
        //Debug.Log("ReqestNext: " + idx);
        if (!triggerSkip)
            triggerSkip = true;
        else if (idx < detail.Count)
        {
            OnClickNext();
        }
        else OnClickCancel();
    }

    public void OnClickApply()
    {
        //Debug.Log("OnClickApply");
        //UIManager.getInstance.ShowChatDialog(true)
    }

    public void OnClickCancel()
    {
        //Debug.Log("OnClickCancel");
        UIManager.getInstance.ShowChatDialog(false);
        chat.IsIntering = false;
    }
}

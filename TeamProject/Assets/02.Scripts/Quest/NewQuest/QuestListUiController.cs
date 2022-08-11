using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListUiController : MonoBehaviour
{
    public GameObject Panel_Quest;
    public Transform parent;

    private Text Text_NpcName;
    private Text Text_Description;
    private Text Text_Progress;

    List<GameObject> questUIList = new List<GameObject>();

    private void Awake()
    {
        Panel_Quest = Resources.Load<GameObject>("Prefab/UI/Quest Item");
    }

    private void Start()
    {
        GameManager.instance.playerManager.AddQuestEvent += AddQuestUI;
        GameManager.instance.playerManager.RemoveQuestEvent += RemoveQuestUI;
    }

    public Transform Parent
    {
        get
        {
            if (parent == null)
                parent = UIManager.getInstance.QuestListContent;
            return parent;
        }
    }


    public void AddQuestUI(Quest quest)
    {
        GameObject ui = CreateQuestUI(quest);

        questUIList.Add(ui);
    }

    public void RemoveQuestUI(Quest quest)
    {
        // Debug.Log("현재 UI 개수 : " + questUIList.Count + ", 지운다?");
        for (int i=0; i<questUIList.Count; i++)
        {
            // Debug.Log("퀘스트 번호 : " + quest.number + ", 현재 번호 : " + i);

            if (questUIList[i].name.Equals(quest.number.ToString()))
            {
                // Debug.Log("지운다");

                Destroy(questUIList[i]); // 오브젝트 삭제 하고,
                questUIList.RemoveAt(i); // 리스트에서도 삭제
            }
        }
    }

    public void UpdateQuestUI(Quest quest)
    {
        for (int i = 0; i < questUIList.Count; i++)
        {
            if (questUIList[i].name.Equals(quest.number.ToString()))
            {
                SetQuestUI(questUIList[i], quest);
            }
        }
    }
    

    // 만들고 반환함
    private GameObject CreateQuestUI(Quest quest)
    {
        GameObject ui = Instantiate(Panel_Quest, Parent);
        ui.name = quest.number.ToString();

        ui = SetQuestUI(ui, quest);

        return ui;
    }


    // 퀘스트 내용을 UI에 세팅
    private GameObject SetQuestUI(GameObject ui, Quest quest)
    {
        // 순서 지키자
        Text_NpcName = ui.transform.GetChild(0).GetComponent<Text>();
        Text_Description = ui.transform.GetChild(1).GetComponent<Text>();
        Text_Progress = ui.transform.GetChild(2).GetComponent<Text>();

        Text_NpcName.text = quest.npcName;
        Text_Description.text = quest.shortDescription;
        Text_Progress.text = quest.progress;

        return ui;
    }
}

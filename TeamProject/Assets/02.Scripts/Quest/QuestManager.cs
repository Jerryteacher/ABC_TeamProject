using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    static QuestManager instance;

    public int questId;
    public int questActionIndex; //퀘스트 대화 순서를 정할 변수
    public GameObject[] questObject; //퀘스트 오브젝트를 저장할 변수
    Dictionary<int, QuestData> questList;

    public static QuestManager getInstance
    {
        get
        {
            if (instance == null)
                instance = (QuestManager)FindObjectOfType(typeof(QuestManager));
            return instance;
        }
    }
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }


    void GenerateData()
    {
        questList.Add(10, new QuestData("첫 마을 방문.","대화를 한다 ", new int[] { 1000, 3000 })); //Add(quest Id, data)
        questList.Add(20, new QuestData("잃어버린 망치찾기.", "스미스가 잃어버린 망치를 찾아준다. ", new int[] { 5000, 3000 })); //Add(quest Id, data)
        questList.Add(30, new QuestData("퀘스트 클리어.","", new int[] {0 }));
        //questList.Add(40, new QuestData(".", "", new int[] { 0 }));

    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++; //대화가 끝이 났을때 questActionIndex값이 올라가서 더이상 quest ID가 10이 아니다.

        ControlObject(); //퀘스트 오브젝트가 있을때 실행.

        if (questActionIndex == questList[questId].npcId.Length) //이미 저장한 npc들과 대화가 끝났다면 
            NextQuest();                                        //다음퀘스트 확인 

        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest() //다음퀘스트를 위한 함수 생성
    {
        questId += 10;
        questActionIndex = 0; //퀘스트가 다시 시작했기 때문에 0으로 초기화.
    }
    void ControlObject() //퀘스트 오브젝트를 관리할 함수 생성
    {
        switch (questId)
        {   //퀘스트 번호, 퀘스트 대화순서에 따라 오브젝트를 조절
            case 10:
                if (questActionIndex == 2) //두번대화가 끝났을때 오브젝트를 on시킨다.
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 1) //동전을 먹었을때 오브젝트를 off시킨다.
                    questObject[0].SetActive(false);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData 
{
    public string questName; //퀘스트 이름
    public string contentName;
    public int[] npcId; //npc아이디를 저장하는 int 배열


    public QuestData(string name, string content, int[] npc) //생성자
    {
        questName = name;
        contentName = content;
        npcId = npc;
    }
}
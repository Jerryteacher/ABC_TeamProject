using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TalkManager : MonoBehaviour
{
    static TalkManager instance;
    Dictionary<int, string[]> talkData;//사전(몇번째 , 대화)
    Dictionary<int, Sprite> portraitData; //초상화 Data 
    public Sprite[] portraitArr; //초상화 이미지를 넣어줄 sprite배열

    public static TalkManager getInstance
    {
        get
        {
            if (instance == null)
                instance = (TalkManager)FindObjectOfType(typeof(TalkManager));
            return instance;
        }
    }
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData() //데이터를 만들어주는 함수
    {
        talkData.Add(1000, new string[] { "안녕?:0", "이 곳이 처음이야?:0", "나는 리나라고 해.:0" }); //데이터 옆에 : + 숫자 를 사용하면 기호+ index값의 초상화 스프라이트가 가져와짐.
                                                                                      //string을 배열로 넣은 이유 = 문장을 여러개 넣기 위해서.
        talkData.Add(2000, new string[] { "나는 레지스탕스의 수장이자 반군의 수장을 맡고 있는 사령관 엘리다.:0", "잘 부탁하지.:0" });
        talkData.Add(3000, new string[] { "어서오게나:0", "나는 대장장이 스미스라고 하네.:0", "맡길 물건이 있으면 나에게 부탁하시게나:0" });
        talkData.Add(100, new string[] {"경계 중 이상무!" });
        talkData.Add(200, new string[] { "경계는 중요한 임무다!" });
        talkData.Add(300, new string[] { "근무중에는 사적인 대화를 할 수 없소." });

        //Quest Talk
        //talkData.Add(10 + 1000, new string[] {"어서 와.:0",
        //                                    "처음 보는데 일을 하나 맡아볼래?:0",
        //                                    "대장간에 있는 대장장이 스미스한태가서 자세한 설명을 들어봐.:0"});
        //talkData.Add(11 + 3000, new string[] {"어서 오게.:0",
        //                                    "리나에게 자네의 이야기는 들었네.:0",
        //                                    "사령부에 무기 공급을 해야하는데 망치를 고양이가 물어갔네.:0",
        //                                    "잃어버린 망치를 찾아주게나.:0"});
        //talkData.Add(11 + 1000, new string[] { "아직 대장간에 안갔어?.:0" });


        //talkData.Add(20 + 1000, new string[] { "고양이가 망치를 훔쳐갔다고?:0",
        //                                       "바쁜상황에 망치관리 똑바로 못하고 말이야!:0",
        //                                       "나중에 한마디 해야겠어. :0"});
        //talkData.Add(20 + 3000, new string[] { "찾으면 꼭 가져다 주게나 부탁이네.:0" });
        //talkData.Add(20 + 5000, new string[] { "잃어버린 망치을 찾았다." });
        //talkData.Add(21 + 3000, new string[] {"귀중한 물건이었는데 찾아줘서 정말 고맙네!:0",
        //                                      "아! 지휘부에서 자네를 찾는다고 하더군 산채 중앙에 있는 건물로 가보게.:0"});

        //talkData.Add(40 + 2000, new string[] { "어서와. 나는 레지스탕스의 수장이자 반군의 지휘를 맡고 있는 사령관 엘리야.:0",
        //                                       "너에게 맡길 임무가 있어:0",
        //                                       "" });


        //초상화 스프라이트(4개일경우)
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(2000 + 0, portraitArr[1]);
        portraitData.Add(3000 + 0, portraitArr[2]);

    }
    public string GetTalk(int id, int talkIndex) //대화 데이터를 가져올 함수 (오브젝트 id,대화 데이터)필요
    {
        //예외처리
        if (!talkData.ContainsKey(id)) //Containskey = Dintionary안에 key가 존재하는지 검사
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //퀘스트 맨 처음 대사가 없을때.
                //기본 대사를 가져온다.
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                //해당퀘스트 진행 순서 대사가 없을 때
                //퀘스트 맨 처음 대사를 가져온다.
                return GetTalk(id - id % 10, talkIndex);
            }
        }
        if (talkIndex == talkData[id].Length)
            return null; //더이상 남은 문장이 없다. 대화가 끝낫다.
        else
            return talkData[id][talkIndex]; //뒤에 대화가 남아서 계속 이어가야함.
    }

    public Sprite GetPortrait(int id, int portraitIndex)//스프라이트를 관리/반환할 함수 생성
    {
        return portraitData[id + portraitIndex];
    }
}



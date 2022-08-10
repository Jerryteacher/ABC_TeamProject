using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;
    InputHandler inputHandler;
    Animator anim;
    CameraHandler cameraHandler;
    PlayerLocomotion playerLocomotion;

    public bool isInteracting;


    //Slider expSlider;
    //public Text levelText;

    public PlayerStats playerStats;
    public GameObject QuestComponentSave;

    //bool 관리
    [Header("Player Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;

    [HideInInspector] public List<Quest> questList = new List<Quest>();
    public Action<Quest> AddQuestEvent;
    public Action<Quest> RemoveQuestEvent;
    public Action<Quest> CompletequestEvent;
    public Action<EnemyHealth> MonsterKillEvent;
    public Action<PlayerStats> LevelUpevent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        cameraHandler = FindObjectOfType<CameraHandler>();

        playerStats = new PlayerStats();

        playerStats.nickname = "이안";
        playerStats.currExp = 0;
        //expSlider.maxValue = playerStats.exp;
        //expSlider.value = 0;

        playerStats.Level = 1;
    }

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    void Update()
    {
        float delta = Time.deltaTime;
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");

        inputHandler.TickInput(delta);
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleRollingAndSprinting(delta);
        playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }
    }

    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        inputHandler.sprintFlag = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;

        if (isInAir)
        {
            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
        }
    }
    #region 경험치 및 레벨업
    public void GetExp(EnemyHealth enemyHealth)
    {
        int virtualExp = playerStats.currExp + enemyHealth.exp;

        while (virtualExp >= playerStats.exp)
        {
            virtualExp -= playerStats.exp;
            LevelUp();
        }
        playerStats.currExp = virtualExp;
        UIManager.getInstance.UpdateExperience(playerStats.exp, playerStats.currExp);
    }
    public void GetExp(Quest quest)
    {
        int virtualExp = playerStats.currExp + quest.reward;

        while (virtualExp >= playerStats.exp)
        {
            virtualExp -= playerStats.exp;
            LevelUp();
        }
        playerStats.currExp = virtualExp;
        UIManager.getInstance.UpdateExperience(playerStats.exp, playerStats.currExp);
    }
    public void LevelUp()
    {
        playerStats.Level += 1;

        //levelText.text = "Lv " + playerStats.Level;
        //expSlider.maxValue = playerStats.exp;

        if (LevelUpevent != null)
        {
            LevelUpevent(this.playerStats);
        }
    }
    #endregion

    #region 퀘스트 관련
    public void AddQuest(Quest quest)
    {
        questList.Add(quest);

        Debug.Log("퀘스트를 수락하셨습니다.");
        SetQuestToOneNPC(quest);
        // 이벤트 발생 (UI 적용)
        AddQuestEvent(quest);
    }
    public void RemoveQuest(Quest quest)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].number == quest.number)
            {
                Quest tmp = questList[i];
                tmp.enabled = false;

                // 이벤트 발생 (UI 적용)
                RemoveQuestEvent(quest);
            }
        }
    }
    public void SetQuestToOneNPC(Quest quest)
    {
        GameObject[] NPCArr = GameObject.FindGameObjectsWithTag("NPC");

        if (NPCArr == null)
            return;

        for (int i = 0; i < NPCArr.Length; i++)
        {
            if (quest.npcName == NPCArr[i].GetComponent<NPCCtrl>().npcName)
            {
                NPCArr[i].GetComponent<NPCCtrl>().SetQuest_WhenPlayerAddQuest(quest);
            }
        }
    }

    public void SetQuestToAllNPC()
    {
        GameObject[] NPCArr = GameObject.FindGameObjectsWithTag("NPC");

        if (NPCArr == null)
            return;

        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCArr.Length; j++)
            {
                // Debug.Log("퀘스트 비교 중, quest의 NpcName:" + questList[i].npcName + ", NPC의 이름:" + NPCArr[j].GetComponent<NPCCtrl>().npcName);

                if (questList[i].npcName == NPCArr[j].GetComponent<NPCCtrl>().npcName)
                {
                    NPCArr[j].GetComponent<NPCCtrl>().SetQuest_WhenChangeScene(questList[i]);
                }
            }
        }
    }
    #endregion
}


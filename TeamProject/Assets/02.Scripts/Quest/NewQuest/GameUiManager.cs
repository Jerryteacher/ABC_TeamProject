using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiManager : MonoBehaviour
{
    public static GameUiManager instance= null;
    [HideInInspector]
    public GameObject CanvasScreen;

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
    }


    [HideInInspector] public QuestListUiController questListUiController;


    public GameObject Tooltip;


    void Start()
    {
        CanvasScreen = GameObject.FindWithTag("CANVAS_SCREEN");


        questListUiController = CanvasScreen.GetComponent<QuestListUiController>();

    }

    // UI 전체 조절
    public void ClearUI()
    {
        CanvasScreen.GetComponent<CanvasGroup>().alpha = 0;
    }
    public void ShowUI()
    {
        CanvasScreen.GetComponent<CanvasGroup>().alpha = 1;
    }
}

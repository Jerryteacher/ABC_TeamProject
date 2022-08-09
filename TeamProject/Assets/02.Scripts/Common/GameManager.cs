using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private DataManager dataManager;
    public GameDataObject gameData;

    void Awake()
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
        

        //UI
        SceneManager.LoadScene("MainUI", LoadSceneMode.Additive);
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        //UI
        DontDestroyOnLoad(UIManager.getInstance.gameObject);
        DontDestroyOnLoad(FindObjectOfType<EventSystem>());
        DontDestroyOnLoad(FindObjectOfType<MinimapCam>());
    }
    public void incKillCount()
    {
        ++gameData.KillCount;
    }
}

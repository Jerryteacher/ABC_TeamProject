using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform[] Points1;
    [SerializeField]
    Transform[] Points2;
    public GameObject[] EnemyPrefab1; //생성할 적 프리팹
    public GameObject[] EnemyPrefab2;
    public GameObject BossPrefab;
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    public int strongholdmaxCount = 10;  //최대 생성 적
    public int ruinmaxCount = 10;
    private const string enemyTag = "Enemy";
    public bool IsGameOver = false;
    public GameObject[] Enemies;
    List<GameObject> EnemyPool1 = new List<GameObject>();
    List<GameObject> EnemyPool2 = new List<GameObject>();
    public float StrongHoldCreateTime = 3.0f;
    public float RuinCreateTime = 3.0f;
    public float BossCreateTime = 30.0f;
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
        Points1 = SpawnPoint1.GetComponentsInChildren<Transform>();
        Points2 = SpawnPoint2.GetComponentsInChildren<Transform>();
        for (int i = 0; i < strongholdmaxCount; i++)
        {
            int enemyidx = Random.Range(0, 2);
            GameObject Enemy = Instantiate(EnemyPrefab1[enemyidx]);
            Enemy.name = "Stronghold Enemy" + (i + 1).ToString();
            Enemy.SetActive(false);
            EnemyPool1.Add(Enemy);
            
        }
        if (Points1.Length > 0)
            StartCoroutine(CreateStrongHold());

        for (int i = 0; i < ruinmaxCount; i++)
        {
            int enemyidx = Random.Range(0, 2);
            GameObject Enemy = Instantiate(EnemyPrefab2[enemyidx]);
            Enemy.name = "Ruin Enemy" + (i + 1).ToString();
            Enemy.SetActive(false);
            EnemyPool2.Add(Enemy);
        }       
        if (Points2.Length > 0)
            StartCoroutine(CreateRuin());

        GameObject Boss = Instantiate(BossPrefab);
        Boss.name = "Boss";
        Boss.SetActive(false);
        EnemyPool1.Add(Boss);
        StartCoroutine(CreateBoss());

        //UI
        SceneManager.LoadScene("MainUI", LoadSceneMode.Additive);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //UI
        DontDestroyOnLoad(UIManager.getInstance.gameObject);
        DontDestroyOnLoad(FindObjectOfType<EventSystem>());
    }
    IEnumerator CreateStrongHold()
    {
        while (!IsGameOver)
        {
            yield return new WaitForSeconds(StrongHoldCreateTime);
            if (IsGameOver) yield break;
            foreach (GameObject enemy in EnemyPool1)
            {
                if (enemy.activeSelf == false)
                {
                    int idx = Random.Range(1, Points1.Length);
                    enemy.transform.position = Points1[idx].position;
                    enemy.SetActive(true);                  
                    break;
                }
            }
        }
    }
    IEnumerator CreateRuin()
    {
        while (!IsGameOver)
        {
            yield return new WaitForSeconds(RuinCreateTime);
            if (IsGameOver) yield break;
            foreach (GameObject enemy2 in EnemyPool2)
            {
                if (enemy2.activeSelf == false)
                {
                    int idx = Random.Range(1, Points2.Length);
                    enemy2.transform.position = Points2[idx].position;
                    enemy2.SetActive(true);
                    break;
                }
            }
        }
    }
    IEnumerator CreateBoss()
    {
        while (!IsGameOver)
        {
            yield return new WaitForSeconds(BossCreateTime);
            if (IsGameOver) yield break;
            foreach (GameObject Boss in EnemyPool1)
            {
                if (Boss.activeSelf == false)
                {
                    Boss.transform.position = Points1[0].position;
                    Boss.SetActive(true);
                    break;
                }
            }
        }
    }
     
    public void incKillCount()
    {
        ++gameData.KillCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject SpawnPoint1;
    public GameObject SpawnPoint2;
    [SerializeField]
    Transform[] Points1;
    [SerializeField]
    Transform[] Points2;
    public GameObject[] EnemyPrefab1; //생성할 적 프리팹
    public GameObject[] EnemyPrefab2;
    public GameObject BossPrefab;
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
    void Start()
    {
        SpawnPoint1 = Resources.Load<GameObject>("SpawnPoint/StrongHoldSpawnPoint");
        SpawnPoint2 = Resources.Load<GameObject>("SpawnPoint/RuinSpawnPoint");
        Points1 = SpawnPoint1.GetComponentsInChildren<Transform>();
        Points2 = SpawnPoint2.GetComponentsInChildren<Transform>();
        EnemyPrefab1 = Resources.LoadAll<GameObject>("Enemy/Bandit");
        EnemyPrefab2 = Resources.LoadAll<GameObject>("Enemy/Bandit");
        BossPrefab = Resources.Load<GameObject>("Enemy/Boss");
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
                    int idx = Random.Range(1, Points1.Length);
                    Boss.transform.position = Points1[idx].position;
                    Boss.SetActive(true);
                    break;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    Transform tr;
    BoxCollider boxCollider;  // 적이 생성될 범위
    public GameObject[] EnemyPrefab; //생성할 적 프리팹
    public int maxCount = 10;  //최대 생성 적
    private const string enemyTag = "Enemy";
    public bool IsGameOver = false;
    public GameObject[] Enemies;
    List<GameObject> EnemyPool = new List<GameObject>();
    private void Awake()
    {
        tr = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider>();
        CreatePoolling();
    }
    void Start()
    {
        //StartCoroutine(RandomSpawn());
    }
    void Update()
    {

    }
    private void CreatePoolling()
    {
        for(int i = 0; i<maxCount; i++)
        {
            int enemyidx = Random.Range(0, 2);
            GameObject Enemy = Instantiate(EnemyPrefab[enemyidx], RandomPosition(),Quaternion.identity) ;
            Enemy.name = "Enemy" + (i+1).ToString();
            Enemy.SetActive(true);
            EnemyPool.Add(Enemy);
        }
    }
    //IEnumerator RandomSpawn()
    //{
    //    while (true)
    //    {
    //        int enemyCount = (int)GameObject.FindGameObjectsWithTag(enemyTag).Length;
    //        if (enemyCount < maxCount)
    //        {
    //            yield return new WaitForSeconds(1f);
    //            int enemyidx = Random.Range(0, 2);
    //            GameObject Enemy = Instantiate(EnemyPrefab[enemyidx], RandomPosition(), Quaternion.identity);
    //            enemyCount++;
    //        }
    //        else
    //        {
    //            yield return null;
    //        }
    //    }
    //}
    Vector3 RandomPosition()  //박스 콜라이더 범위 내 랜덤값 
    {
        Vector3 originPosition = tr.transform.position;
        float range_x = boxCollider.bounds.size.x; //박스 콜라이더의 x축 길이
        float range_z = boxCollider.bounds.size.z; //박스 콜라이더의 z축 길이
        range_x = Random.Range((range_x / 2) * -1, range_x / 2); // 중심에서 길이의 절반만큼 랜덤
        range_z = Random.Range((range_z / 2) * -1, range_z / 2);
        Vector3 RandomPosition = new Vector3(range_x, 0f, range_z); //
        Vector3 spawnPosition = originPosition + RandomPosition;
        return spawnPosition;
    }
}

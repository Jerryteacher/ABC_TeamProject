using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsGameOver = false;
    public GameObject EnemyPrefab;
    public List<GameObject> EnemyPool = new List<GameObject>();
    public int maxCount = 0;
    public GameManager g_manager;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

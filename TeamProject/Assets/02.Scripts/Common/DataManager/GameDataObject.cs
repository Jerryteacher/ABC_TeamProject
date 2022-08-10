using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Create GameData", order = 1)]
public class GameDataObject : ScriptableObject
{
    public int EnemyLevel = 1;
    public int KillCount = 0;
    public float EnemyHp = 100f;
    public int E_Damage = 15;
}

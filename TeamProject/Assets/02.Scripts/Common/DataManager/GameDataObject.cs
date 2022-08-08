﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDataSO", menuName = "Create GameData", order = 1)]
public class GameDataObject : ScriptableObject
{
    public int KillCount = 0;
    public float EnemyHp = 100f;
    public float Damage = 25f;
}
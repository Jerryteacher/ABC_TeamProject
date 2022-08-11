using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerStats : MonoBehaviour
{
    public float healthLevel = 10;

    public float constitution;  // CON 생명력
    public float strength;      // STR 힘
    public float dexterity;     // DEX 손재주, 민첩
    public float curHealth;     // 현재 HP 체력
    public float maxHealth;     // 최대 HP 체력
    public float attack;        // ATK 공격력
    public float defence;       // DEF 방어력

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        curHealth = maxHealth;
    }
    private float SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return curHealth;
    }
    public void TakeDamage(int damage)
    {
        curHealth = curHealth - damage;
    }

}

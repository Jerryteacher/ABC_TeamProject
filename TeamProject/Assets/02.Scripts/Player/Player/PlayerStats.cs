using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("플레이어 정보")]
    public string nickname;
    public int Level = 1;

    [SerializeField] float maxHp;
    [SerializeField] float curHp;
    public int exp
    {
        get
        {
            int result = 30 + (Level * 20);
            return result;
        }
    }
    public int currExp = 0;


    void Start()
    {
        maxHp = 100;
        SetHp(maxHp);
    }



    void Update()
    {

    }

    public void OnDamaged(float dmg)
    {
        float _hp = curHp;
        //계산
        _hp -= dmg;
        SetHp(_hp);
    }

    void SetHp(float hp)
    {
        curHp = Mathf.Clamp(hp, 0, maxHp);
        if (UIManager.getInstance != null)
            UIManager.getInstance.UpdateHealth((int)maxHp, (int)curHp);
    }
}



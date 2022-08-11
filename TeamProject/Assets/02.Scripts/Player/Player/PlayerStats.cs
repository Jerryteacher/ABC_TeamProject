using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerStats
{
    [Header("플레이어 정보")]
    public string nickname;
    public int Level = 1;

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

    }



    void Update()
    {

    }


}

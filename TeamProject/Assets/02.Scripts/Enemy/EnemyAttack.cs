﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator;
    private Transform tr;
    private Transform playerTr;

    private readonly int hashAttack = Animator.StringToHash("Attack");

    private float nextAttack = 0f;
    private float AttackRate = 0.1f;

    private readonly float damping = 10f;
    EnemyAI enemyAI;
    public bool isAttack = false;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        if (enemyAI.isDie) return;
        if(isAttack)
        {
            if(Time.time>= nextAttack)
            {
                animator.SetTrigger(hashAttack);
                nextAttack = Time.time + AttackRate + Random.Range(0.1f,0.35f);
            }
            Quaternion rot = Quaternion.LookRotation(playerTr.position - tr.position);
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
        }
    }
}

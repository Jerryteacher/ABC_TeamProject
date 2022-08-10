using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator;
    private Transform tr;
    private Transform playerTr;

    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashAttackIdx = Animator.StringToHash("AttackIdx");
    private float nextAttack = 0f;
    private float AttackRate = 0.1f;
    [SerializeField]
    private EnemyDamageCollider enemyDamageCollider;
    private readonly float damping = 10f;
    EnemyAI enemyAI;
    public bool isAttack = false;
    void Start()
    {
        enemyDamageCollider = GetComponentInChildren<EnemyDamageCollider>();
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        if (enemyAI.isDie) return;
        animator.SetInteger(hashAttackIdx, Random.Range(0, 4));
        if (isAttack)
        {        
            if (Time.time>= nextAttack)
            {
                StartCoroutine(ColliderSet());
                animator.SetTrigger(hashAttack);
                nextAttack = Time.time + AttackRate + Random.Range(3f,4f);
            }
            Quaternion rot = Quaternion.LookRotation(playerTr.position - tr.position);
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
        }
    }
    IEnumerator ColliderSet()
    {
        Debug.Log("켜짐");
        enemyDamageCollider.EnableDamageCollider();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length*0.8f);
        enemyDamageCollider.DisableDamageCollider();
        Debug.Log("꺼짐");
        yield return new WaitForSeconds(0.01f);
    }
}

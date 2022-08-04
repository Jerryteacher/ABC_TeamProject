using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL, TRACE, ATTACK, DIE
    }
    public State state = State.PATROL;
    public enum ENEMY_Type
    {
        //enemy1 = 산적 기지에서 스폰하는 일반 적
        //enemy2 = 폐허에서 스폰하는 일반 적
        enemy1 = 1, enemy2
    }
    public ENEMY_Type Type;

    private Transform PointTr1;
    private Transform PointTr2;

    private Transform PlayerTr;
    private Transform Tr;
    public float traceDist = 10f;
    public float attackDist = 3f;
    public bool isDie = false;
    private Animator animator;
    private WaitForSeconds ws;

    private MoveAgent moveagent;
    private EnemyFOV enemyFov;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;

    private readonly int hashIdx = Animator.StringToHash("Idx");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");
    private readonly int hashDie= Animator.StringToHash("Die");
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null)
            PlayerTr = player.GetComponent<Transform>();

        Tr = GetComponent<Transform>();
        PointTr1 = GameObject.Find("StrongHoldSpawnPoint").GetComponent<Transform>();
        PointTr2 = GameObject.Find("RuinSpawnPoint").GetComponent<Transform>();
        moveagent = GetComponent<MoveAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyFov = GetComponent<EnemyFOV>();
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        ws = new WaitForSeconds(0.3f);
    }
    private void OnEnable()
    {
        animator.SetInteger(hashIdx, Random.Range(0, 3));
        animator.SetInteger(hashDieIdx, Random.Range(0, 3));
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }
    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if (state == State.DIE) yield break;
            float Pointdist1 = Vector3.Distance(Tr.position, PointTr1.position);
            float Pointdist2 = Vector3.Distance(Tr.position, PointTr2.position);
            if (Pointdist1<Pointdist2)
            {
                Type= ENEMY_Type.enemy1;
            }
            else { Type = ENEMY_Type.enemy2; }
            float dist = Vector3.Distance(PlayerTr.position, Tr.position);
            if (dist < attackDist)
            {
                state = State.ATTACK;
            }
            else if (dist < traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            yield return ws;
        }
    }
    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;
            switch (state)
            {
                case State.PATROL:
                    enemyAttack.isAttack = false;
                    moveagent.patrolling = true;
                    animator.SetBool(hashMove, true);
                    break;
                case State.TRACE:
                    enemyAttack.isAttack = false;
                    moveagent.traceTarget = PlayerTr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    moveagent.Stop();
                    //animator.SetBool(hashMove, false);
                    if (enemyAttack.isAttack == false)
                        enemyAttack.isAttack = true;                    
                    break;
                case State.DIE:
                    Die();
                    break;
            }
        }
    }
    private void Die()
    {
        this.gameObject.tag = "Untagged";
        isDie = true;
        moveagent.Stop();
        animator.SetTrigger(hashDie);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GameManager.instance.incKillCount();
        StopAllCoroutines();
        StartCoroutine(PushObjectPool());
    }
    void Update()
    {
        animator.SetFloat(hashSpeed, moveagent.speed);
    }
    public void OnPlayerDie()
    {
        moveagent.Stop();
        StopAllCoroutines();
    }
    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(5f);
        isDie = false;
        enemyHealth.Hp = enemyHealth.initHp;        
        gameObject.tag = "Enemy";
        state = State.PATROL;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(false);
        GetComponent<EnemyHpbar>().IsShow = false;
    }
}
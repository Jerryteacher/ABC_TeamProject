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

    private Transform PlayerTr;
    private Transform Tr;
    private Animator Ani;
    public float traceDist = 10f;
    public float attackDist = 3f;
    public bool isDie = false;
    private Animator animator;
    private WaitForSeconds ws;

    private MoveAgent moveagent;
    private EnemyFOV enemyFov;
    private EnemyAttack enemyAttack;
    private EnemyHealth enemyHealth;
    private Spawn spawn;
    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");
    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null)
            PlayerTr = player.GetComponent<Transform>();

        Tr = GetComponent<Transform>();
        Ani = GetComponent<Animator>();

        moveagent = GetComponent<MoveAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyFov = GetComponent<EnemyFOV>();
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        spawn = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Spawn>();
        ws = new WaitForSeconds(0.3f);
    }
    private void OnEnable()
    {
        animator.SetInteger(hashDieIdx, Random.Range(0, 3));
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }
 
    IEnumerator CheckState()
    {
        yield return new WaitForSeconds(1.0f);
        while (!isDie)
        {
            if (state == State.DIE) yield break;
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
            //0.3초 동안 대기하는 동안 프레임 제어권을 양보
            yield return ws;
        }
    }
    IEnumerator Action()
    {
        while (isDie==false)
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
                    animator.SetBool(hashMove, false);
                    if (enemyAttack.isAttack == false)
                        enemyAttack.isAttack = true;                    
                    break;
                case State.DIE:
                    Die();
                    break;
            }
        }
    }
    public void Die()
    {
        this.gameObject.tag = "Untagged";
        isDie = true;
        moveagent.Stop();
        
        animator.SetTrigger(hashDie);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
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
        yield return new WaitForSeconds(3.0f);
        isDie = false;
        enemyHealth.Hp = enemyHealth.MaxHp;
        gameObject.tag = "Enemy";
        state = State.PATROL;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(true);
    }
}
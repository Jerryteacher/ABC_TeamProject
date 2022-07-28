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

    private readonly int hashMove = Animator.StringToHash("IsMove");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
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
        animator = GetComponent<Animator>();
        ws = new WaitForSeconds(0.3f);
    }
    private void Start() /*OnEnable()으로 변경*/
    {   
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }
    private void OnDisable()
    {
        
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
    private void Die()
    {
        this.gameObject.tag = "Untagged";
        isDie = true;
        moveagent.Stop();

        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        StopAllCoroutines();
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
}
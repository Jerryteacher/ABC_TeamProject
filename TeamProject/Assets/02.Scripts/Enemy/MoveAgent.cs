using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int nexIdx = 0;

    private readonly float patrolSpeed = 1.5f; 
    private readonly float traceSpeed = 4f;

    float damping = 1.0f;
    private NavMeshAgent agent;
    private Transform enemyTr;
    private bool _patrolling;
    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }
    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 7.0f;
            TraceTarget(_traceTarget);
        }
    }
    EnemyAI enemyAI;
    public float speed
    {
        get { return agent.velocity.magnitude; }
    }
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>(); 
        agent.autoBraking = false;
        agent.updateRotation = false;
        agent.speed = patrolSpeed;
        if (enemyAI.Type == EnemyAI.ENEMY_Type.enemy1)
        {
           var group = GameObject.Find("StrongholdWayPoint");
            if (group != null)
            {
                group.GetComponentsInChildren<Transform>(wayPoints);
                wayPoints.RemoveAt(0);
                nexIdx = Random.Range(0, wayPoints.Count);
            }
        }
        else
        {
            var group = GameObject.Find("RuinWayPoint");
            if (group != null)
            {
                group.GetComponentsInChildren<Transform>(wayPoints);
                wayPoints.RemoveAt(0);
                nexIdx = Random.Range(0, wayPoints.Count);
            }
        }
        MoveWayPoint();
    }
    void MoveWayPoint() 
    {   
        if (agent.isPathStale) return;
        agent.destination = wayPoints[nexIdx].position;
        agent.isStopped = false;
    }
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos;
        agent.isStopped = false;
    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }
    void Update()
    {
        if (GetComponent<EnemyAI>().isDie == true) return;
        if (agent.isStopped == false)
        {    
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime);
        }
        if (!_patrolling) return;
        if (agent.velocity.sqrMagnitude>=0.2f*0.2f&&agent.remainingDistance<=1f)
        {
            nexIdx = Random.Range(0, wayPoints.Count);
            MoveWayPoint();
        }
    }
}

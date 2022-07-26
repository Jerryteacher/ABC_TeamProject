using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    //적 캐릭터의 추적 사정 거리의 범위
    public float viewRange = 15.0f;
    [Range(0, 360)]
    //적 캐릭터 시야각
    public float viewAngle = 120f;

    private Transform enemyTr;
    private Transform playerTr;
    private int PlayerLayer;
    private int obstacleLayer;
    private int layerMask;
    void Start()
    {
        enemyTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        PlayerLayer = LayerMask.NameToLayer("PLAYER");
        obstacleLayer = LayerMask.NameToLayer("OBSTACLE");
        layerMask = 1 << PlayerLayer | 1 << obstacleLayer;

    }
    //주어진 각도에 의해 원주 위의 점의 좌표값을 계산하는 함수
    public Vector3 CirclePoint(float angle)
    {
        //로컬 좌표계 기준으로 설정하기 위해 적 캐릭터의
        //Y 회전값을 더함
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sign(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    public bool isTracePlayer()
    {

        bool isTrace = false;

        Collider[] cols = Physics.OverlapSphere(enemyTr.position,
                                                viewRange,
                                                1 << PlayerLayer);

        if (cols.Length == 1)
        {
            Vector3 dir = (playerTr.position - enemyTr.position).normalized;

            if (Vector3.Angle(enemyTr.forward, dir) < viewAngle * 0.5f)
            {
                isTrace = true;
            }
        }
        return isTrace;
    }
    public bool isViewPlayer()
    {
        bool isView = false;
        RaycastHit hit;

        Vector3 dir = (playerTr.position - enemyTr.position).normalized;
        if (Physics.Raycast(enemyTr.position, dir, out hit, viewRange, layerMask))
        {
            isView = (hit.collider.CompareTag("Player"));
        }
        return isView;
    }

}

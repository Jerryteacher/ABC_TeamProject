﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchInteraction : MonoBehaviour
{
    GameObject interObj;
    [SerializeField] float radius = 3f;
    [SerializeField] float EnemyHpChkradius = 3f;
    bool IsSearching = false;
    public bool SearchingPause { get; set; }
    
    bool hasInteraction = false;


    private void OnEnable()
    {
        interObj = null;
        IsSearching = true;
        StartCoroutine(CheckInteractionObject());
    }

    private void OnDisable()
    {
        IsSearching = false;
        if (UIManager.getInstance != null)
            UIManager.getInstance.ShowInteraction(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interObj != null)
            {
                //searchingPause = true;
                interObj.GetComponent<IInteractionObject>().ActionInter();
            }
            //else if(UIManager.getInstance.IsChatUIActive)
            //{
            //    UIManager.getInstance.ShowChatDialog(false);
            //}


        }

        /*
         * 
         * if(InputManager.GetInputInteractionKey)
         * {
         * if(interObj!=null)
         * {
         * interObj.GetComponent<IInteractionObject>().ActionInter();
         * }
         * }
         */
    }
    //public void ResetPause() => searchingPause = false;
    IEnumerator CheckInteractionObject()
    {
        //초기화 딜레이
        yield return new WaitForSeconds(0.1f);
        while (IsSearching)
        {
            if (!SearchingPause) 
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, radius);
                IInteractionObject _obj = null;
                GameObject minDistObj = null;
                float minDist = radius;
                foreach (Collider col in cols)
                {
                    // 예외처리
                    if (col.gameObject == gameObject) continue; //자신 제외

                    // 상호작용 인터페이스 검사
                    _obj = col.GetComponent<IInteractionObject>();
                    if (_obj != null)
                    {
                        // 가장 가까운 대상 탐색
                        float _dist = Vector3.Distance(transform.position, col.transform.position);
                        if (_dist <= minDist)
                        {
                            minDist = _dist;
                            minDistObj = col.gameObject;
                        }
                    }
                }
                if (minDistObj != null)
                {
                    // UI 활성화
                    if (!hasInteraction)
                        UIManager.getInstance.ShowInteraction(true);
                    // 이전 대상과 비교 후, 업데이트
                    if (minDistObj != interObj)
                    {
                        UIManager.getInstance.SetInteractionItem(minDistObj.name, minDistObj.GetComponent<IInteractionObject>().InterType);
                        interObj = minDistObj;
                    }
                }
                else
                {
                    //UI 비활성화
                    hasInteraction = false;
                    UIManager.getInstance.ShowInteraction(false);
                    interObj = null;
                }
            }
            //감지 범위 내 적 탐색 후 Hp표시
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, EnemyHpChkradius + 2);
                foreach (Collider col in cols)
                {
                    // 예외처리
                    if (col.gameObject == gameObject) continue; //자신 제외
                    EnemyHpbar _obj;
                    // 상호작용 인터페이스 검사
                    _obj = col.GetComponent<EnemyHpbar>();
                    if (_obj != null)
                    {
                        Vector3 dist = col.transform.position - transform.position;
                        if (Vector3.SqrMagnitude(dist) < EnemyHpChkradius * EnemyHpChkradius)
                        {
                            _obj.IsShow = true;
                        }
                        else _obj.IsShow = false;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.name);
    //    IInteractionObject _obj = other.GetComponent<IInteractionObject>();
    //    if (_obj != null)
    //        _obj.ShowInter(true);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    //Debug.Log(other.name);
    //    IInteractionObject _obj = other.GetComponent<IInteractionObject>();
    //    if (_obj != null)
    //        _obj.ShowInter(false);
    //}
}

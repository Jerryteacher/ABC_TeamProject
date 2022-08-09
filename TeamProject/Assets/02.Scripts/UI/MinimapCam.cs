using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    Transform playerTr;


    readonly string targetTag = "PLAYER";

    Transform targetTr;
    public Transform TargetTr
    {
        get
        {
            if(targetTr == null)
                targetTr = GameObject.FindWithTag(targetTag).GetComponent<Transform>();
            return targetTr;
        }
    }
    void Start()
    {
        //playerTr = GameObject.FindWithTag(targetTag).GetComponent<Transform>();
    }



    // Update is called once per frame
    void LateUpdate()
    {
        if (TargetTr == null) return;
        Vector3 playerPos = TargetTr.position;
        transform.position = new Vector3(playerPos.x, 40f, playerPos.z);
    }
}

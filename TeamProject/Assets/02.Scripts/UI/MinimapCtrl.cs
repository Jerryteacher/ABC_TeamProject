using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCtrl : MonoBehaviour
{
    Transform camTr;
    Transform targetTr;
    [SerializeField] RectTransform miniMap;
    [SerializeField] RectTransform playerIcon;

    public bool isRotIcon = true;

    readonly string targetTag = "PLAYER";
    public Transform TargetTr
    {
        get
        {
            if (targetTr == null)
                targetTr = GameObject.FindWithTag(targetTag).GetComponent<Transform>();
            return targetTr;
        }
    }

    public Transform CamTr
    {
        get
        {
            if (camTr == null)
                camTr = FindObjectOfType<CameraHandler>().GetComponent<Transform>();
            return camTr;
        }
    }    

    private void Start()
    {
        miniMap = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        playerIcon = transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(isRotIcon)
        {
            miniMap.eulerAngles = new Vector3(0, 0, CamTr.rotation.eulerAngles.y);
            playerIcon.eulerAngles = new Vector3(0, 0, CamTr.rotation.eulerAngles.y - TargetTr.rotation.eulerAngles.y);
        }
        else
        {
            miniMap.eulerAngles = Vector3.zero;
            playerIcon.eulerAngles = new Vector3(0, 0, -TargetTr.rotation.eulerAngles.y);
        }
        
        //float _rot = TargetTr.rotation.eulerAngles.y;
        //if (isRotIcon)
        //{
        //    playerIcon.eulerAngles = new Vector3(0, 0, -_rot);
        //    miniMap.eulerAngles = Vector3.zero;
        //}
        //else
        //{
        //    playerIcon.eulerAngles = Vector3.zero;
        //    float _tmp = CamTr.rotation.eulerAngles.y;
        //    miniMap.eulerAngles = new Vector3(0, 0, _tmp);
        //}
    }
}

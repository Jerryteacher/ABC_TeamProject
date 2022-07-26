using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//Editor 클래스는 유니티 IDE를 구성하는 API 집합이며
//런타임 시에 다른 스크립트에서 참조 할 수 없다.
//즉, 디자인 시점에서만 사용할 수 있는 클래스이다.
//반드시 Editor라는 폴더 안에서 사용해야 한다.
[CustomEditor(typeof(EnemyFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {   
        EnemyFOV fov = (EnemyFOV)target;
        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);

        Handles.color = Color.white;

        Handles.DrawWireDisc(fov.transform.position, Vector3.up, fov.viewRange);   
        Handles.color = new Color(1, 1, 1, 0.2f);

        Handles.DrawSolidArc(fov.transform.position, Vector3.up,  
                            fromAnglePos,  fov.viewAngle, fov.viewRange); 

        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f),
            fov.viewAngle.ToString());
    }
}

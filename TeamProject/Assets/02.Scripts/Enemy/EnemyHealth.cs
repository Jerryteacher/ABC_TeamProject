using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private const string WeaponTag = "WEAPON";
    [SerializeField]
    public float Hp = 0f;
    public float MaxHp = 100f;
    public float Damage = 30f; 
    [SerializeField]
    private Animator animator;
    public GameDataObject gameData;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Hp = MaxHp+(gameData.KillCount*10);
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == WeaponTag)
        {
            Debug.Log("Hit");
            Hp -= Damage;
            if (Hp <= 0)
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            else
                animator.SetTrigger("Hit");
        }
    }
    void SaveGameData()
    {
        UnityEditor.EditorUtility.SetDirty(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}

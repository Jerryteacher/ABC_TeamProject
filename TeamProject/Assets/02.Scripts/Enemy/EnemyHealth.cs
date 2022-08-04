using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private const string WeaponTag = "WEAPON";
    public float Hp = 0f;
    [SerializeField]
    private float maxHp = 0f;
    public float initHp = 100f;
    public float Damage = 30f;
    [SerializeField]
    private Animator animator;
    public GameDataObject gameData;

    public float MaxHp 
    { 
        get
        {
            return maxHp;
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        //maxHp = initHp + (gameData.KillCount * 10);
        maxHp = Hp = initHp + (gameData.KillCount * 10);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == WeaponTag)
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
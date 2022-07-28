using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private const string WeaponTag = "WEAPON";
    [SerializeField]
    private float Hp = 0f;
    public float MaxHp = 100f;
    public float Damage = 30f; 
    [SerializeField]
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Hp = MaxHp;
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == WeaponTag)
        {
            Debug.Log("Hit");
            animator.SetTrigger("Hit");
            Hp -= Damage;
            if(Hp<=0)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            }
        }
    }
}

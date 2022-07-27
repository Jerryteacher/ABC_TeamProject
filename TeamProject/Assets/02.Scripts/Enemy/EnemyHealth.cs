using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Hp = 0f;
    public float MaxHp = 100f;
    public float Damage = 30f;
    private const string WeaponTag = "WEAPON";
    private Animator Ani;
    void Start()
    {
        Ani = GetComponent<Animator>();
        Hp = MaxHp;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == WeaponTag)
        {
            Ani.SetTrigger("Hit");
            Hp -= Damage;
            
            if(Hp<=0)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
               
            }
        }
    }
}

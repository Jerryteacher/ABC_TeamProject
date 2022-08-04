using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class test_EnemyHP : MonoBehaviour
    {
        Rigidbody rbody;
        Collider capColl;
        public float enemyHP = 100;

        DamageCollider damageCollider;


        void Start()
        {
            rbody = GetComponent<Rigidbody>();
            capColl = GetComponent<Collider>();

            damageCollider = GetComponent<DamageCollider>();
        }

    }
}

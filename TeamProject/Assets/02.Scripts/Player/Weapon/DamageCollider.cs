﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        [SerializeField]
        private GameObject HitEffect;
        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
            HitEffect = Resources.Load<GameObject>("Hits/Hit_01");
            
        }
    
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                //임시 확인용////////////////////////////////////////////////
                EnemyStats enemyStats = other.GetComponent<EnemyStats>();

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                }
                /////////////////////////////////////////////////////////////


                IDamageable _damage = other.GetComponent<IDamageable>();
                other.GetComponent<Animator>().SetTrigger("Hit");              
                StartCoroutine(Hit(damageCollider));
                if (_damage != null)
                {
                    Debug.Log(other.name);
                    _damage.OnDamaged(currentWeaponDamage);
                }
                else
                {
                    Debug.Log("오류");
                }
            }
        }
        IEnumerator Hit(Collider other)
        {
            Debug.Log("이펙트");
            yield return new WaitForSeconds(0.01f);
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            Instantiate(HitEffect, pos, rot);
        }
    }


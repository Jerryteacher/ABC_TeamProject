using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        [SerializeField]
        private GameObject HitEffect;
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
                
                IDamageable _damage = other.GetComponent<IDamageable>();
                other.GetComponent<Animator>().SetTrigger("Hit");
                StartCoroutine(Hit(damageCollider));
                if (_damage != null)
                {
                    Debug.Log(other.name);
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
}

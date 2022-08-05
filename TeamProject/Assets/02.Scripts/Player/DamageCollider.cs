using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
       
        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.enabled = false;
        }
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }
        
    }
}

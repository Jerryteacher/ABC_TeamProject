using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    [SerializeField]
    Collider damageCollider;
    [SerializeField]
    private GameObject HitEffect;
    public int currentWeaponDamage = 25;
    public GameObject Enemy;
    private EnemyAttack enemyAttack; 
    public GameDataObject gameData;

    public GameObject player;


    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
        enemyAttack = Enemy.GetComponent<EnemyAttack>();
        HitEffect = Resources.Load<GameObject>("Hits/Hit_02");
        StartCoroutine(DamageData());
        player = GameObject.Find("Player");
    }
    IEnumerator DamageData()
    {
        currentWeaponDamage = gameData.E_Damage;
        yield return new WaitForSeconds(0.2f);
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
        if (other.tag == "PLAYER")
        {
            IDamageable _damage = other.GetComponent<IDamageable>();
            StartCoroutine(Hit(damageCollider));
            if (_damage != null)
            {
                _damage.OnDamaged(currentWeaponDamage);
                Debug.Log(other.name);
                Debug.Log(-currentWeaponDamage);
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(currentWeaponDamage);
                }
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

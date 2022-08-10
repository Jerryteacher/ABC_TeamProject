using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp;
    [SerializeField] float curHp;

    [SerializeField] private AudioClip[] HitClips;
    [SerializeField] private AudioClip[] DieClips;
    [SerializeField] private AudioSource audioSource;

    public float nextHit = 2.0f;

    AnimatorHandler animatorHandler;

    void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorHandler>();

        HitClips = Resources.LoadAll<AudioClip>("HitSound");
        DieClips = Resources.LoadAll<AudioClip>("DieSound");
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        maxHp = 100;
        SetHp(maxHp);
    }

    public void TakeDamage(int damage)
    {
        //죽음 판정
        if (Time.time >= nextHit)
        {
            if (curHp <= 0)
            {
                Die();
                //Time.timeScale = 0;           
            }
            //hit, death 사운드
            if (curHp > 0)
            {
                animatorHandler.PlayTargetAnimation("Player_Hit", true);
                audioSource.PlayOneShot(HitClips[Random.Range(0, HitClips.Length)], 0.5f);
            }
            else
            {
                audioSource.PlayOneShot(DieClips[Random.Range(0, DieClips.Length)], 0.5f);
            }
        }
    }
    void Die()
    {
        curHp = 0;
        animatorHandler.PlayTargetAnimation("Player_Death", true);
        this.gameObject.tag = "Untagged";
        this.gameObject.layer = 0;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnDamaged(float dmg)
    {
        float _hp = curHp;
        //계산
        _hp -= dmg;
        SetHp(_hp);
    }
    void SetHp(float hp)
    {
        curHp = Mathf.Clamp(hp, 0, maxHp);
        if(UIManager.getInstance != null)
            UIManager.getInstance.UpdateHealth((int)maxHp, (int)curHp);
    }
}

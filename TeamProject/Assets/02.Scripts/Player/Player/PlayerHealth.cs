using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp;
    public float curHp;

    [SerializeField] private AudioClip[] HitClips;
    [SerializeField] private AudioClip[] DieClips;
    [SerializeField] private AudioSource audioSource;

    public float nextHit = 2.0f;
    BGMController bgmController;
    PlayerLocomotion playerLocomotion;
    AnimatorHandler animatorHandler;
    GameObject Point;
    void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        HitClips = Resources.LoadAll<AudioClip>("HitSound");
        DieClips = Resources.LoadAll<AudioClip>("DieSound");
        audioSource = GetComponent<AudioSource>();
        bgmController = GetComponentInChildren<BGMController>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        Point = GameObject.Find("StartPoint");
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
            //hit, death 판정
            animatorHandler.PlayTargetAnimation("Player_Hit", true);

            if (curHp <= 0)
            {
                StartCoroutine(Die());
            }
            //hit, death 사운드
            if (curHp > 0)
            {
                audioSource.PlayOneShot(HitClips[Random.Range(0, HitClips.Length)], 0.5f);
            }
            else
            {
                audioSource.PlayOneShot(DieClips[Random.Range(0, DieClips.Length)], 0.5f);
            }
        }
    }

    IEnumerator Die()
    {
        animatorHandler.PlayTargetAnimation("Player_Death", true);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(5f);
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.tag = "PLAYER";
        animatorHandler.PlayTargetAnimation("Player_Death", false);
        animatorHandler.Initialize();
        bgmController.EnemyCount = 0;
        SetHp(maxHp);
        SceneManager.LoadScene("Field");
        //GetComponent<SearchInteraction>().enabled = false;
        //GetComponent<EnemyAI>().enabled = false;
        //GetComponent<MoveAgent>().enabled = false;
        yield return new WaitForSeconds(5f);
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
        if (UIManager.getInstance != null)
            UIManager.getInstance.UpdateHealth((int)maxHp, (int)curHp);
    }
    
}
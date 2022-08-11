using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class EnemyHealth : MonoBehaviour, IDamageable
{
    private const string WeaponTag = "WEAPON";



    public float Hp = 0f;
    [SerializeField]
    private float maxHp = 0f;
    public float initHp = 100f;
    [SerializeField]
    private Animator animator;
    public GameDataObject gameData;
    Collider Enemycollider;
    [SerializeField]
    private AudioClip[] HitClips;
    [SerializeField]
    private AudioClip[] DieClips;
    [SerializeField]
    private AudioSource audioSource;
    EnemyAI enemyAI;

    [Header("[Enemy]")]
    public int exp;
    public string enemyname;
    public float MaxHp
    {
        get
        {
            return maxHp;
        }
    }
    [SerializeField]
    private GameObject HitEffect;
    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
        Enemycollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        HitClips = Resources.LoadAll<AudioClip>("HitSound");
        DieClips = Resources.LoadAll<AudioClip>("DieSound");
        HitEffect = Resources.Load<GameObject>("Hits/Hit_01");
    }
    private void OnEnable()
    {
        maxHp = Hp = gameData.EnemyLevel * 50 + 150;
    }
    public void OnDamaged(float dmg)
    {
        float _hp = Hp;
        _hp -= dmg;
        GetComponent<EnemyHpbar>().ShowDamage(dmg);
        SetHp(_hp);
        if (Hp <= 0)
        {
            enemyAI.state = EnemyAI.State.DIE;
            PlayerManager.instance.MonsterKillEvent(this);
        }
        else
        {
            animator.SetTrigger("Hit");
            StartCoroutine(Hit());
        }

    }
    public void SetHp(float hp) => Hp = Mathf.Clamp(hp, 0, maxHp);
    void SaveGameData()
    {
        #if UNITY_EDITOR
        EditorUtility.SetDirty(gameData);
#endif
    }
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WEAPON")
        {
            if (Hp > 0)
            {
                audioSource.PlayOneShot(HitClips[Random.Range(0, HitClips.Length)], 0.5f);
            }
            else
            {
                audioSource.PlayOneShot(DieClips[Random.Range(0, DieClips.Length)], 0.5f);
            }
        }
    }
    IEnumerator Hit()
    {
        Debug.Log("이펙트");
        yield return new WaitForSeconds(0.01f);
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        Instantiate(HitEffect, pos, rot);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealth : MonoBehaviour ,IDamageable
{
    private const string WeaponTag = "WEAPON";
    public float Hp = 0f;
    [SerializeField]
    private float maxHp = 0f;
    public float initHp = 100f;
    [SerializeField]
    private Animator animator;
    public GameDataObject gameData;
    Collider  Enemycollider;
    [SerializeField]
    private AudioClip[] HitClips;
    [SerializeField]
    private AudioClip[] DieClips;
    [SerializeField]
    private AudioSource audioSource;
    

    public float MaxHp 
    { 
        get
        {
            return maxHp;
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        Enemycollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        HitClips = Resources.LoadAll<AudioClip>("HitSound");
        DieClips = Resources.LoadAll<AudioClip>("DieSound");
    }
    private void OnEnable()
    {
        maxHp = Hp = initHp + (gameData.KillCount * 10);
    }
    public void OnDamaged(float dmg)
    {
        float _hp = Hp;
        //계산
        _hp -= dmg;
        SetHp(_hp);
    }
    public void SetHp(float hp) => Hp = Mathf.Clamp(hp, 0, maxHp);
    void SaveGameData()
    {
        UnityEditor.EditorUtility.SetDirty(gameData);
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
                audioSource.PlayOneShot(HitClips[Random.Range(0, HitClips.Length)], 0.5f);
            else
                audioSource.PlayOneShot(DieClips[Random.Range(0, DieClips.Length)], 0.5f);
        }
    }
}
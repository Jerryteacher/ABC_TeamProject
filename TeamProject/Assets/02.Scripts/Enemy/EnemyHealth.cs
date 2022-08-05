using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private const string WeaponTag = "WEAPON";
    public float Hp = 0f;
    [SerializeField]
    private float maxHp = 0f;
    public float initHp = 100f;
    public float Damage = 30f;
    [SerializeField]
    private Animator animator;
    public GameDataObject gameData;

    [SerializeField]
    private GameObject HitEffect;
    [SerializeField]
    private AudioClip[] audioClips;
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
        HitEffect = Resources.Load<GameObject>("Hits/Hit_01");
        audioSource = GetComponent<AudioSource>();
        audioClips = Resources.LoadAll<AudioClip>("HitSound");
    }
    private void OnEnable()
    {
        //maxHp = initHp + (gameData.KillCount * 10);
        maxHp = Hp = initHp + (gameData.KillCount * 10);
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == WeaponTag)
        {
            Debug.Log("Hit");
            StartCoroutine(Hit(col));
            Hp -= Damage;
            if (Hp <= 0)
            {

                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            }
            else
            {
                animator.SetTrigger("Hit");
                audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)],0.5f);
            }
        }
    }
    void SaveGameData()
    {
        UnityEditor.EditorUtility.SetDirty(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGameData();
    }
    IEnumerator Hit(Collision col)
    {
        Debug.Log("이펙트");
        yield return new WaitForSeconds(0.01f);
        Vector3 pos = col.contacts[0].point;
        Vector3 _normal = col.contacts[0].normal;
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
        Instantiate(HitEffect, pos, rot);
    }
}
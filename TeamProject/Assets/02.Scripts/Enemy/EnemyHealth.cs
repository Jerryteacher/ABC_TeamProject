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
   
}
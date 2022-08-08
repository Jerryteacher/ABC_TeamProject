using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHp;
    [SerializeField] float curHp;


    // Start is called before the first frame update
    void Start()
    {
        maxHp = 100;
        SetHp(maxHp);
    }


    // Update is called once per frame
    void Update()
    {
        
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

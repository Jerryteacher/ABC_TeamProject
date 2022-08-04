using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        private void Awake()
        {


            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if(weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if(weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        //public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        //{
        //    if(isLeft)
        //    {
        //        leftHandSlot.LoadWeaponModel(weaponItem);
        //    }
        //    else
        //    {
        //        rightHandSlot.LoadWeaponModel(weaponItem);
        //    }
        //}

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isRight)
        {
            if (isRight)
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
            }
            //else
            //{
            //    leftHandSlot.LoadWeaponModel(weaponItem);
            //    LoadLeftWeaponDamageCollider();
            //}
        }




        #region weapon collider 관리


        //weaponslot에 있는 무기의 damage collider를 가져옴
        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        } 


        //애니메이션 작동시만 collider open / close
        public void OpenRightDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void OpenLeftDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }

        public void CloseRightDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void CloseleftDamageCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }


        #endregion

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        public string lastAttack;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == "great_sword_slash_1")
                {
                    animatorHandler.PlayTargetAnimation("great_sword_slash_2", true);
                    lastAttack = "great_sword_slash_2"; 
                }

                else if (lastAttack == "great_sword_slash_2")
                {
                    animatorHandler.PlayTargetAnimation("great_sword_slash_3", true);
                    lastAttack = "great_sword_slash_3";
                }

                else if (lastAttack == "great_sword_slash_3")
                {
                    animatorHandler.PlayTargetAnimation("great_sword_slash_4", true);
                }
            }
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation("great_sword_slash_1", true);
            lastAttack = "great_sword_slash_1";
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation("Skill Spin Attack", true);
            lastAttack = "Skill Spin Attack";
        }
    }
}

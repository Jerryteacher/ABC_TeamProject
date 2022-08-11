using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] FootClips;
        [SerializeField] private AudioClip[] WeaponClips;
        private AudioSource audioSource;

        //싱글턴
        InputHandler inputHandler;
        PlayerManager playerManager;


        void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            playerManager = GetComponentInParent<PlayerManager>();
            inputHandler = GetComponent<InputHandler>();
        }

        //////////////////////////////////////////////////////////////////

        private void Step()
        {
        if (playerManager.isInteracting == false)
        {
            AudioClip clip = RandomFootClip();
            audioSource.PlayOneShot(clip);
        }
        else return;
    }

        private AudioClip RandomFootClip()
        {
            return FootClips[UnityEngine.Random.Range(0, FootClips.Length)];
        }

        ////////////////////////////////////////////////////////////////////////

        private void WeaponSwing()
        {
            AudioClip clip = RandomWeaponClip();
            audioSource.PlayOneShot(clip);
        }

        private AudioClip RandomWeaponClip()
        {
            return WeaponClips[UnityEngine.Random.Range(0, WeaponClips.Length)];
        }
    }

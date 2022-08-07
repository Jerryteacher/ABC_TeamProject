using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGMController : MonoBehaviour
{
    Collider BGMCollider;
    [SerializeField]
    private AudioClip BGMClips;
    [SerializeField]
    private AudioSource audioSource;
    void Start()
    {
        BGMCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        BGMClips = Resources.Load<AudioClip>("BGM/Normal/FieldBGM");
        audioSource.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name == "Field")
        {
            if (other.tag == "Enemy")
            {
                BGMClips = Resources.Load<AudioClip>("BGM/Battle/BattleBGM");
                audioSource.Play();
            }
        }
    }
}

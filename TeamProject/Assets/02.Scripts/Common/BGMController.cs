using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGMController : MonoBehaviour
{
    [SerializeField]
    Collider BGMCollider;
    [SerializeField]
    private AudioClip[] BGMClips;
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    public AudioSource BattleSource;
    public int EnemyCount = 0;
    void Start()
    {
        BGMCollider = GetComponent<Collider>();
        BGMClips = Resources.LoadAll<AudioClip>("BGM");
    }
    private void OnTriggerEnter(Collider other)
    {
            if (other.tag == "Enemy")
            {
                EnemyCount++;
            }
    }
    private void OnTriggerExit(Collider other)
    { 
            if (other.tag == "Enemy")
            {
                EnemyCount--;
            }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Field")
        {
            if (EnemyCount > 0 && BattleSource.isPlaying == false)
            {
                audioSource.Stop();
                BattleSource.PlayOneShot(BGMClips[0]);
            }
            else if (EnemyCount == 0 && audioSource.isPlaying == false)
            {
                BattleSource.Stop();
                audioSource.PlayOneShot(BGMClips[1]);
            }
        }
        else if (SceneManager.GetActiveScene().name == "Village" && audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(BGMClips[2]);
        }
    }
}

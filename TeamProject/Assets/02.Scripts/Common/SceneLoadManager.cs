using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField]
    Collider Portalcollider;
    GameObject Portal;
    void Start()
    {
        Portalcollider = GetComponent<Collider>();
        Portal = GameObject.Find("Portal");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            if (SceneManager.GetActiveScene().name == "Field")
            {
                SceneManager.LoadScene("Village");
            }
            else if (SceneManager.GetActiveScene().name == "Village")
            {
                SceneManager.LoadScene("Field");
            }
           
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField]
    Collider Portalcollider;
    [SerializeField]
    GameObject Point;
    [SerializeField]
    GameObject Player;
    void Start()
    {
        Portalcollider = GetComponent<Collider>();
        Player = GameObject.Find("Player");
        Point = GameObject.Find("StartPoint");
        Player.transform.position = Point.transform.position;
        Player.transform.rotation = Point.transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            if (SceneManager.GetActiveScene().name == "Field")
            {
                SceneManager.LoadScene("Village");
                Debug.Log("Village");
            }
            else if (SceneManager.GetActiveScene().name == "Village")
            {
                SceneManager.LoadScene("Field");
                Debug.Log("Field");                     
            }        
        }
    }
}

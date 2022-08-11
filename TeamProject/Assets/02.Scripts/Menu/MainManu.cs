using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManu : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Village");
    }
}

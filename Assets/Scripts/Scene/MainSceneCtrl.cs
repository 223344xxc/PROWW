using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneCtrl : MonoBehaviour
{
    public void StartButtonDown()
    {
        SceneManager.LoadScene("Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneHandler : MonoBehaviour
{
    public int group; // 1 for attacking, 0 for defending
    public void StartGame()
    {
        PlayerPrefs.SetInt("group",group);
        SceneManager.LoadScene(1);
    }
}

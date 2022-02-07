using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
}

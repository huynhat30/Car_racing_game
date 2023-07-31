using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OpenGame()
    {
        Debug.Log("Open Game");
    }

    public void instruction()
    {
        Debug.Log("open instruction page");
        SceneManager.LoadScene("InstructionScreen");

    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}

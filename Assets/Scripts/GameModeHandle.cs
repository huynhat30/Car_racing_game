using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeHandle : MonoBehaviour
{
    public void singlePlayer()
    {
        SceneManager.LoadScene("GameScreenOnCountDown_Single");
    }

    public void multiPlayer()
    {
        SceneManager.LoadScene("GameScreenOnCountDown_Multi");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionPage : MonoBehaviour
{
    public void backButton() {
        SceneManager.LoadScene("MainMenu");
    }
}

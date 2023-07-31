using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownController_Single : MonoBehaviour
{
    public int countDown;
    public Text countDownDisplay;
    TopDownCarController topDownCarController;

    private void Start()
    {
        StartCoroutine(CountDowntoStart());
    }

    IEnumerator CountDowntoStart() {
        while(countDown > 0)
        {
            countDownDisplay.text = countDown.ToString();
            yield return new WaitForSeconds(1f);
            countDown--;
        }

        countDownDisplay.text = "GO >>";
        yield return new WaitForSeconds(1f);
        countDownDisplay.gameObject.SetActive(false);
        SceneManager.LoadScene("Gameplay_Single");
    }
}

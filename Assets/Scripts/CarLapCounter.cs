using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CarLapCounter : MonoBehaviour
{
    public Text carPositionText;

    public LapDisplay lapDisplay;
    int passedCheckPointNumber = 42;
    float timeAtLastPassedCheckPoint = 0;

    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;
    const int lapsToComplete = 5;

    bool isRaceCompleted = false;

    int carPosition = 0;

    bool isHideRoutineRunning = false;
    float hideUIDelayTime;

    public event Action<CarLapCounter> OnPassCheckpoint;

    public void SetCarPosition(int position)
    {
        carPosition = position;
    }

    public int getNumberCheckpointsPassed()
    {
        return numberOfPassedCheckpoints;
    }

    public float GetTimeAtLastCheckpoint()
    {
        return timeAtLastPassedCheckPoint;
    }

    IEnumerator ShowPositionCO(float delayUntilHidePosition)
    {
        hideUIDelayTime += delayUntilHidePosition;

        carPositionText.text = carPosition.ToString();

        carPositionText.gameObject.SetActive(true);

        if (!isHideRoutineRunning)
        {
            isHideRoutineRunning = true;

            yield return new WaitForSeconds(hideUIDelayTime);
            carPositionText.gameObject.SetActive(false);

            isHideRoutineRunning = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            if (isRaceCompleted)
                return;

            CheckPoint checkPoint = collision.GetComponent<CheckPoint>();

            if (passedCheckPointNumber == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber + 1;

                numberOfPassedCheckpoints++;

                timeAtLastPassedCheckPoint = Time.time;

                if(checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 1;
                    lapsCompleted++;

                    if(lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
                    }
                    Debug.Log("Complete " + lapsCompleted);
                    lapDisplay.updateLap(lapsCompleted);
                }

                OnPassCheckpoint?.Invoke(this);

                if (isRaceCompleted)
                {
                    StartCoroutine(ShowPositionCO(100));
                } else
                {
                    StartCoroutine(ShowPositionCO(1.5f));
                }
            }

        }
    }
}

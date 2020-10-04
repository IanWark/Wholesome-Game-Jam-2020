using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringController : MonoBehaviour
{
    [SerializeField, Tooltip("How many times you can mess up before game over.")]
    private int strikesUntilOut = 3;

    [SerializeField]
    private ScoringUI scoringUI = null;

    private int currentScore = 0;
    private int currentStrikes = 0;

    private void Start()
    {
        scoringUI.SetScoreValue(0);
        scoringUI.SetStrikesValue(0);
        scoringUI.SetStrikesMax(strikesUntilOut);
    }

    public void IncreaseScore(int points)
    {
        currentScore += points;

        // Update UI
        scoringUI.SetScoreValue(currentScore);
    }

    public void IncreaseStrikes()
    {
        currentStrikes += 1;
        if (currentStrikes >= strikesUntilOut)
        {
            // End game
        }

        // Update UI
        scoringUI.SetStrikesValue(currentStrikes);
    }
}

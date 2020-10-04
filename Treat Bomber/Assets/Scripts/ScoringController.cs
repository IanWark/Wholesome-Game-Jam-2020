using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringController : MonoBehaviour
{
    [SerializeField, Tooltip("How many times you can mess up before game over.")]
    private int strikesUntilOut = 3;

    [SerializeField]
    private string gameOverBeforeScoreString = "Your score was ";

    [SerializeField]
    private string gameOverAfterScoreString = "!";

    [SerializeField]
    private ScoringUI scoringUI = null;

    [SerializeField]
    private GameObject gameOverPanel = null;

    [SerializeField]
    private TextMeshProUGUI gameOverBodyText = null;

    [SerializeField]
    private SpriteRenderer redFilter = null;

    [SerializeField]
    private float redOpacityTwoStrikesLeft = 0.1f;

    [SerializeField]
    private float redOpacityOneStrikeLeft = 0.25f;

    [SerializeField]
    private float redOpacityGameOver = 0.50f;

    private int currentScore = 0;
    private int currentStrikes = 0;

    private bool gameEnded = false;

    private void Start()
    {
        scoringUI.SetScoreValue(0);
        scoringUI.SetStrikesValue(0);
        scoringUI.SetStrikesMax(strikesUntilOut);
    }

    public void IncreaseScore(int points)
    {
        if (!gameEnded)
        {
            currentScore += points;

            // Update UI
            scoringUI.SetScoreValue(currentScore);
        }
    }

    public void IncreaseStrikes()
    { 
        if (!gameEnded)
        {
            currentStrikes += 1;
            if (currentStrikes >= strikesUntilOut)
            {
                EndGame();
            }
            else if (currentStrikes == strikesUntilOut - 2)
            {
                redFilter.enabled = true;
                Color newColor = redFilter.color;
                newColor.a = redOpacityTwoStrikesLeft;
                redFilter.color = newColor;
            }
            else if (currentStrikes == strikesUntilOut -1)
            {
                redFilter.enabled = true;
                Color newColor = redFilter.color;
                newColor.a = redOpacityOneStrikeLeft;
                redFilter.color = newColor;
            }

            // Update UI
            scoringUI.SetStrikesValue(currentStrikes);
        }
    }

    private void EndGame()
    {
        FindObjectOfType<Player>().gameObject.SetActive(false);
        FindObjectOfType<Spawner>().gameObject.SetActive(false);

        // For some reason making all little beasts leave here will sometimes miss one, so do it in update.

        redFilter.enabled = true;
        Color newColor = redFilter.color;
        newColor.a = redOpacityGameOver;
        redFilter.color = newColor;

        gameOverPanel.SetActive(true);
        gameOverBodyText.text = gameOverBeforeScoreString + currentScore.ToString() + gameOverAfterScoreString;

        gameEnded = true;
    }

    private void Update()
    {
        if(gameEnded)
        {
            // All beasts must immediately evacuate the premises
            List<LittleBeast> littleBeasts = new List<LittleBeast>(FindObjectsOfType<LittleBeast>());
            for (int i = 0; i < littleBeasts.Count; ++i)
            {
                littleBeasts[i].Leave();
            }
        }
    }
}

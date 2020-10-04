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

            // Update UI
            scoringUI.SetStrikesValue(currentStrikes);
        }
    }

    private void EndGame()
    {
        FindObjectOfType<Player>().gameObject.SetActive(false);
        FindObjectOfType<Spawner>().gameObject.SetActive(false);

        gameOverPanel.SetActive(true);
        gameOverBodyText.text = gameOverBeforeScoreString + currentScore.ToString() + gameOverAfterScoreString;

        gameEnded = true;
    }
}

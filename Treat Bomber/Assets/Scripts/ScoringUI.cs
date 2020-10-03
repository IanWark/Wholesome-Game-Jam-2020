using UnityEngine;
using TMPro;

public class ScoringUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreValue = null;

    [SerializeField]
    private TextMeshProUGUI strikesValue = null;
    [SerializeField]
    private TextMeshProUGUI strikesMax = null;

    public void SetScoreValue(int value)
    {
        scoreValue.text = value.ToString();
    }

    public void SetStrikesValue(int value)
    {
        strikesValue.text = value.ToString();
    }

    public void SetStrikesMax(int value)
    {
        strikesMax.text = value.ToString();
    }

}

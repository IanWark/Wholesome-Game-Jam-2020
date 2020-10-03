using UnityEngine;

public abstract class LittleBeast : MonoBehaviour
{
    // Must be set by derived class!
    protected eCandyType preferredCandyType;

    // Can be set by derived class!
    protected int pointsValue = 1;

    public eCandyType GetPreferredCandyType()
    {
        return preferredCandyType;
    }

    // Candy controls the interaction, because of problems with multiple LBs being hit with 1 candy.
    public void RecieveCandy(eCandyType candyType)
    {
        ScoringController scoringController = FindObjectOfType<ScoringController>();

        if (candyType == preferredCandyType)
        {
            scoringController.IncreaseScore(pointsValue);
        }
        else
        {
            scoringController.IncreaseStrikes();
        }

        // fukken die
        // TODO run off screen instead
        Destroy(gameObject);
    }
}

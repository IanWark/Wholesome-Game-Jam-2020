using UnityEngine;

public abstract class LittleBeast : MonoBehaviour
{
    // Must be set by derived class!
    protected eCandyType preferredCandyType;

    // Can be set by derived class!
    protected int pointsValue = 1;

    protected Vector2 movement = new Vector2();

    protected Rigidbody2D rigidBody = null;
    protected Collider2D ourCollider = null;
    protected SpriteRenderer sprite = null;

    protected bool isLeaving = false;

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

        Leave();
    }

    protected void Leave()
    {
        // Set flag to use leaving speed
        isLeaving = true;

        // Ignore walls
        ourCollider.isTrigger = true;

        // Go to closest edge
        if (transform.position.x > 0)
        {
            movement.x = 1;
        }
        else
        {
            movement.x = -1;
        }
    }

    // If going right, flip sprite
    // If going left, unflip sprite
    // If not moving, do not change spirte
    protected void FlipToMovement()
    {
        if (movement.x > 0)
        {
            sprite.flipX = true;
        }
        else if (movement.x < 0)
        {
            sprite.flipX = false;
        }
    }

    // On colliding with a wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Change movement direction
        movement.x *= -1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If we left the background, despawn
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}

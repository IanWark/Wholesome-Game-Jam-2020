using UnityEngine;

public abstract class LittleBeast : MonoBehaviour
{
    // Must be set by derived class!
    protected eCandyType preferredCandyType;

    // On colliding with a candy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Candy candyObject = collision.gameObject.GetComponent<Candy>();
        if (candyObject != null && candyObject.GetCandyType() == preferredCandyType)
        {
            // fukken die
            Destroy(gameObject);
        }
    }
}

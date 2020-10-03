using UnityEngine;

public enum eCandyType
{
    NONE,
    ZOMBIE,
}

public class Candy : MonoBehaviour
{
    private eCandyType candyType = eCandyType.NONE;

    private SpriteRenderer sprite = null;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public eCandyType GetCandyType()
    {
        return candyType;
    }

    public void SetCandy(CandyDataList.CandyDataObject newCandy)
    {
        candyType = newCandy.candyType;
        sprite.sprite = newCandy.sprite;
    }

    // On colliding with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<LittleBeast>() != null)
        {
            // fukken die
            Destroy(gameObject);
        }
    }
}

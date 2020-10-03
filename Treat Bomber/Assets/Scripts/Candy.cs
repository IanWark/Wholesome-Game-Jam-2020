using UnityEngine;

public enum eCandyType
{
    NONE,
    ZOMBIE,
    ROBOT,
    SCARECROW,
    VAMPIRE,
}

public class Candy : MonoBehaviour
{
    private Vector2 movement = new Vector2(0, -0.25f);

    private eCandyType candyType = eCandyType.NONE;

    private Rigidbody2D rigidBody = null;
    private SpriteRenderer sprite = null;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidBody.MovePosition(rigidBody.position + movement);
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
        // If touching a kid or a wall
        if (collision.gameObject.GetComponent<LittleBeast>() != null || collision.gameObject.layer == 8)
        {
            // fukken die
            Destroy(gameObject);
        }
    }
}

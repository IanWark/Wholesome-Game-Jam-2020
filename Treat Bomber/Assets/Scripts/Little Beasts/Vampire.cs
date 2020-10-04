using UnityEngine;

public class Vampire : LittleBeast
{
    private Vector2 speed = new Vector2(0.15f, 0);
    protected Vector2 movement = new Vector2();

    protected Rigidbody2D rigidBody = null;
    protected SpriteRenderer sprite = null;

    private Vector2 raiseVampire = new Vector2(1, 0.25f);
    private Vector2 sinWave = new Vector2(0, 0);
    private float sinVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        preferredCandyType = eCandyType.VAMPIRE;

        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Randomly start moving to the left or right.
        int[] values = { -1, 1 };
        movement.x = values[Random.Range(0, 2)];

        // Spawn vampire above other monsters
        rigidBody.MovePosition(rigidBody.position * raiseVampire);
    }

    // Update is called once per frame
    void Update()
    {
        // Make vampire fly in sine wave pattern
        sinVal += Time.deltaTime * 10;
        sinWave.y = Mathf.Sin(sinVal) / 10;

        DoMove();
        FlipToMovement();
    }

    private void DoMove()
    {
        rigidBody.MovePosition(rigidBody.position + (movement * speed) + sinWave);
    }

    // If going right, flip sprite
    // If going left, unflip sprite
    // If not moving, do not change spirte
    private void FlipToMovement()
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
}

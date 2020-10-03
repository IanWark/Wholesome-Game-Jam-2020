using UnityEngine;

public class Zombie : LittleBeast
{
    private Vector2 speed = new Vector2(0.05f, 0);

    protected Rigidbody2D rigidBody = null;
    protected BoxCollider2D bodyCollider = null;
    protected SpriteRenderer sprite = null;

    protected Vector2 moveInput = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        preferredCandyType = eCandyType.ZOMBIE;

        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Randomly start moving to the left or right.
        int[] values = { -1, 1 };
        moveInput.x = values[Random.Range(0, 2)];
    }

    // Update is called once per frame
    void Update()
    {
        DoMove();
        FlipToMovement();
    }

    private void DoMove()
    {
        rigidBody.MovePosition(rigidBody.position + (moveInput * speed));
    }

    // If going right, flip sprite
    // If going left, unflip sprite
    // If not moving, do not change spirte
    private void FlipToMovement()
    {
        if (moveInput.x > 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput.x < 0)
        {
            sprite.flipX = false;
        }
    }

    // On colliding with a wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Change movement direction
        moveInput.x *= -1;
    }
}

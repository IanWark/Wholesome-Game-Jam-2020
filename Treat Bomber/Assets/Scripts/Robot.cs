using UnityEngine;

public class Robot : LittleBeast
{
    private Vector2 speed = new Vector2(0.1f, 0);
    protected Vector2 movement = new Vector2();

    protected Rigidbody2D rigidBody = null;
    protected SpriteRenderer sprite = null;

    // Robot will move immediately
    private float lurchTime = 1.0f;
    private float timeLeft = 0;
    private bool moving = false;
    

    // Start is called before the first frame update
    void Start()
    {
        preferredCandyType = eCandyType.ROBOT;

        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Randomly start moving to the left or right.
        int[] values = { -1, 1 };
        movement.x = values[Random.Range(0, 2)];
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            timeLeft = lurchTime;
            moving = !moving;
        }
        if (moving)
        {
            DoMove();
            FlipToMovement();
        }
    }

    private void DoMove()
    {
        rigidBody.MovePosition(rigidBody.position + (movement * speed));
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

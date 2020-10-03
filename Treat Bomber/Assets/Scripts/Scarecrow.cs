using UnityEngine;

public class Scarecrow : LittleBeast
{
    protected Rigidbody2D rigidBody = null;
    protected SpriteRenderer sprite = null;

    private Vector2 xSpeed = new Vector2(0.125f, 0);
    protected Vector2 movement = new Vector2();

    private float ySpeed;
    private float jumpSpeed = 0.05f;
    private float acceleration = -0.005f; // Gravity
    
    private Vector2 height = new Vector2(0, 0);
    private Vector2 groundHeight = new Vector2();
    private Vector2 zeroY = new Vector2(1, 0); // Help snap to ground height


    // Start is called before the first frame update
    void Start()
    {
        preferredCandyType = eCandyType.SCARECROW;

        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        // Randomly start moving to the left or right.
        int[] values = { -1, 1 };
        movement.x = values[Random.Range(0, 2)];

        // Set the starting ground height to where the scarecrow is standing
        groundHeight.x = 0;
        groundHeight.y = rigidBody.position.y;

        ySpeed = jumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Accelerate the jump/fall
        height.y += ySpeed + acceleration / 2;
        ySpeed += acceleration;
        
        DoMove();
        FlipToMovement();

        // If hitting the ground, start jumping again
        if (rigidBody.position.y < groundHeight.y)
        {
            height.y = 0;
            ySpeed = jumpSpeed;
            rigidBody.MovePosition((rigidBody.position * zeroY) + groundHeight);
        }
    }

    private void DoMove()
    {
        rigidBody.MovePosition(rigidBody.position + (movement * xSpeed) + height);
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

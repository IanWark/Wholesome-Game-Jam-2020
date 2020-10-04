using UnityEngine;

public class Robot : LittleBeast
{
    private Vector2 speed = new Vector2(0.1f, 0);
    protected float leavingSpeedMultiplier = 5;

    private float lurchTime = 1.0f;
    private float timeLeft = 1.0f;
    private bool moving = true;
    

    // Start is called before the first frame update
    void Start()
    {
        preferredCandyType = eCandyType.ROBOT;

        rigidBody = GetComponent<Rigidbody2D>();
        ourCollider = GetComponent<Collider2D>();
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
        float speedMultiplier = isLeaving ? leavingSpeedMultiplier : 1;
        rigidBody.MovePosition(rigidBody.position + (movement * speed * speedMultiplier));
    }
}

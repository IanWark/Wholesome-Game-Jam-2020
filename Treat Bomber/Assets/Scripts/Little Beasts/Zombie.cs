using UnityEngine;

public class Zombie : LittleBeast
{
    private Vector2 speed = new Vector2(0.05f, 0);
    protected float leavingSpeedMultiplier = 10;

    // Start is called before the first frame update
    void Start()
    {
        preferredCandyType = eCandyType.ZOMBIE;

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
        DoMove();
        FlipToMovement();
    }

    private void DoMove()
    {
        float speedMultiplier = isLeaving ? leavingSpeedMultiplier : 1;
        rigidBody.MovePosition(rigidBody.position + (movement * speed * speedMultiplier));
    }
}

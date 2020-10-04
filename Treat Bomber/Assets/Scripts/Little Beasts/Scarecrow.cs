using UnityEngine;

public class Scarecrow : LittleBeast
{
    private bool moving = true;
    private float pause = 0.1f;
    private float timeLeft = 0.1f;

    private Vector2 xSpeed = new Vector2(0.125f, 0);
    protected float leavingSpeedMultiplier = 10;

    private float ySpeed;
    private float jumpSpeed = 0.05f;
    private float acceleration = -0.005f; // Gravity
    
    private Vector2 height = new Vector2(0, 0);
    private Vector2 groundHeight = new Vector2();
    private Vector2 zeroY = new Vector2(1, 0); // Help snap to ground height


    // Start is called before the first frame update
    protected override void Start()
    {
        preferredCandyType = eCandyType.SCARECROW;

        // Set the starting ground height to where the scarecrow is standing
        groundHeight.x = 0;
        groundHeight.y = rigidBody.position.y;

        ySpeed = jumpSpeed;

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (moving)
        {
            // Accelerate the jump/fall
            height.y += ySpeed + acceleration / 2;
            ySpeed += acceleration;
        
            DoMove();
            FlipToMovement();

            // If hitting the ground, land at correct level and pause
            if (rigidBody.position.y < groundHeight.y)
            {
                height.y = 0;
                ySpeed = jumpSpeed;
                rigidBody.MovePosition((rigidBody.position * zeroY) + groundHeight);
                moving = false;
            }
        }

        else
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0.0f)
            {
                timeLeft = pause;
                moving = true;
            }
        }

        base.Update();
    }

    private void DoMove()
    {
        float speedMultiplier = isLeaving ? leavingSpeedMultiplier : 1;
        rigidBody.MovePosition(rigidBody.position + (movement * xSpeed * speedMultiplier) + height);
    }
}

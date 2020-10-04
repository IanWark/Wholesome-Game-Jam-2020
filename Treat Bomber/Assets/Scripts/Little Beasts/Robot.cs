using UnityEngine;

public class Robot : LittleBeast
{
    private Vector2 speed = new Vector2(0.1f, 0);
    protected float leavingSpeedMultiplier = 5;

    private float lurchTime = 1.0f;
    private float timeLeft = 1.0f;
    private bool moving = true;


    // Start is called before the first frame update
    protected override void Start()
    {
        preferredCandyType = eCandyType.ROBOT;

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
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

        base.Update();
    }

    private void DoMove()
    {
        float speedMultiplier = isLeaving ? leavingSpeedMultiplier : 1;
        rigidBody.MovePosition(rigidBody.position + (movement * speed * speedMultiplier));
    }
}

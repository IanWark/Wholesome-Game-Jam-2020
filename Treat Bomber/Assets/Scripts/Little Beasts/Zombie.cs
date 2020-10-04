using UnityEngine;

public class Zombie : LittleBeast
{
    private Vector2 speed = new Vector2(0.05f, 0);
    protected float leavingSpeedMultiplier = 10;

    // Start is called before the first frame update
    protected override void Start()
    {
        preferredCandyType = eCandyType.ZOMBIE;

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        DoMove();
        FlipToMovement();

        base.Update();
    }

    private void DoMove()
    {
        float speedMultiplier = isLeaving ? leavingSpeedMultiplier : 1;
        rigidBody.MovePosition(rigidBody.position + (movement * speed * speedMultiplier));
    }
}

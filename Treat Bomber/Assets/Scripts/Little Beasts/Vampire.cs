using UnityEngine;

public class Vampire : LittleBeast
{
    private Vector2 speed = new Vector2(0.15f, 0);
    protected float leavingSpeedMultiplier = 5;

    private Vector2 raiseVampire = new Vector2(1, 0.25f);
    private Vector2 sinWave = new Vector2(0, 0);
    private float sinVal = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        preferredCandyType = eCandyType.VAMPIRE;

        // Spawn vampire above other monsters
        rigidBody.MovePosition(rigidBody.position * raiseVampire);

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        // Make vampire fly in sine wave pattern
        sinVal += Time.deltaTime * 10;
        sinWave.y = Mathf.Sin(sinVal) / 10;

        DoMove();
        FlipToMovement();

        base.Update();
    }

    private void DoMove()
    {
        float speedMultiplier = isLeaving ? leavingSpeedMultiplier : 1;
        rigidBody.MovePosition(rigidBody.position + (movement * speed * speedMultiplier) + sinWave);
    }
}

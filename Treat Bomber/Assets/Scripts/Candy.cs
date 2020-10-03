using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCandyType
{
    NONE,
    ZOMBIE,
    ROBOT,
    SCARECROW,
    VAMPIRE,
}

public class Candy : MonoBehaviour
{
    private Vector2 movement = new Vector2(0, -0.25f);

    private eCandyType candyType = eCandyType.NONE;

    private Rigidbody2D rigidBody = null;
    private SpriteRenderer sprite = null;

    bool shouldDie = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        rigidBody.MovePosition(rigidBody.position + movement);
    }

    public eCandyType GetCandyType()
    {
        return candyType;
    }

    public void SetCandy(CandyDataList.CandyDataObject newCandy)
    {
        candyType = newCandy.candyType;
        sprite.sprite = newCandy.sprite;
    }

    // On colliding with something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shouldDie)
        {
            // We may hit multiple things at once
            List<Collider2D> colliders = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            collision.OverlapCollider(filter.NoFilter(), colliders);

            LittleBeast bestLittleBeast = null;
            for (int i = 0; i < colliders.Count; ++i)
            {
                Collider2D currentCollider = colliders[i];

                LittleBeast currentLittleBeast = currentCollider.gameObject.GetComponent<LittleBeast>();
                if (currentLittleBeast != null) // if we hit a kid
                {
                    // If we hit multiple LBs, only give the candy to one of them
                    // If we hit any that prefer this candy, give it to them
                    // Otherwise, just give it to the first

                    if (currentLittleBeast.GetPreferredCandyType() == GetCandyType())
                    {
                        bestLittleBeast = currentLittleBeast;
                        shouldDie = true;
                        break;
                    }
                    else if (bestLittleBeast == null)
                    {
                        bestLittleBeast = currentLittleBeast;
                        shouldDie = true;
                    }
                }
                else if (currentCollider.gameObject.layer == 8) // if we hit a wall
                {
                    shouldDie = true;
                }
            }

            if (bestLittleBeast != null)
            {
                bestLittleBeast.RecieveCandy(GetCandyType());
            }

            if (shouldDie)
            {
                Destroy(gameObject);
            }
        }
    }
}

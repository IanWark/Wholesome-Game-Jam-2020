using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Game variables")]
    [SerializeField]
    private Vector2 speed = new Vector2(0.2f, 0);

    private Rigidbody2D rigidBody = null;
    private SpriteRenderer sprite = null;

    private Vector2 moveInput = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        // Since we're not using much physics for movement, do movement in Update instead of FixedUpdate (?)
        DoMove();

        DoAnimations();
    }

    private void HandleInput()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
    }

    private void DoMove()
    {
        rigidBody.MovePosition(rigidBody.position + (moveInput * speed));
    }

    private void DoAnimations()
    {
        // If going right, flip sprite
        // If going left, unflip sprite
        // If not moving, do not change spirte
        if (moveInput.x > 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput.x < 0)
        {
            sprite.flipX = false;
        }
    }

}

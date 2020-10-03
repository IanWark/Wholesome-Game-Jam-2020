using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Game Variables")]
    [SerializeField]
    private Vector2 speed = new Vector2(0.2f, 0);

    protected Vector2 moveInput = new Vector2();

    [Header("Technical References")]
    [SerializeField]
    private Transform candyPrefab = null;

    [SerializeField]
    private CandyDataList candyDataList = null;

    protected Rigidbody2D rigidBody = null;
    protected SpriteRenderer sprite = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        // Spawn candy
        if(Input.GetButtonDown("Fire1"))
        {
            var candyObject = Instantiate(candyPrefab, transform.position, transform.rotation);
            Candy candyScript = candyObject.GetComponent<Candy>();

            candyScript.SetCandy(candyDataList.GetCandyDataObject(eCandyType.ZOMBIE)); // TODO do it based off currently selected candy
        }

        DoMove();
        FlipToMovement();
    }

    private void DoMove()
    {
        rigidBody.MovePosition(rigidBody.position + (moveInput * speed));
    }

    // If going right, flip sprite
    // If going left, unflip sprite
    // If not moving, do not change spirte
    private void FlipToMovement()
    {
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

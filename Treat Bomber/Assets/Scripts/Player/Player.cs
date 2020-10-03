using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Game Variables")]
    [SerializeField, Tooltip("How quickly they can move in X and Y directions.")]
    private Vector2 speed = new Vector2(0.2f, 0);
    private Vector2 moveInput = new Vector2();

    [SerializeField, Tooltip("Time we must wait before dropping another candy.")]
    private float timeBetweenCandy = 1;
    private float candyTimer = 0;

    [Header("Technical References")]
    [SerializeField]
    private Transform candyPrefab = null;

    [SerializeField]
    private CandyDataList candyDataList = null;

    [SerializeField]
    private CandySelectionUIController candySelectionUI = null;

    private const int candyMinIndex = 0;
    private const int candyMaxIndex = 3;
    private int selectedCandyIndex = 1;
    private CandyDataList.CandyDataObject selectedCandy = null;

    private Rigidbody2D rigidBody = null;
    private SpriteRenderer sprite = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        candySelectionUI.Setup(candyDataList);
        SelectCandy(0);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        DoMove();
        FlipToMovement();
    }

    private void HandleInput()
    {
        // Movement
        moveInput.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        moveInput.y = transform.position.y;

        // Candy Selection
        if (Input.GetButtonDown("Select 1"))
        {
            SelectCandy(0);
        }
        else if (Input.GetButtonDown("Select 2"))
        {
            SelectCandy(1);
        }
        if (Input.GetButtonDown("Select 3"))
        {
            SelectCandy(2);
        }
        if (Input.GetButtonDown("Select 4"))
        {
            SelectCandy(3);
        }
        if (Input.GetButtonDown("Select 5"))
        {
            SelectCandy(4);
        }
        if (Input.GetButtonDown("Select 6"))
        {
            SelectCandy(5);
        }
        if (Input.GetButtonDown("Select 7"))
        {
            SelectCandy(6);
        }
        else
        {
            // Scrolling mouse also changes selected candy
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            // round input to either -1 or 1
            scrollInput = Mathf.Sign(scrollInput) * Mathf.Ceil(Mathf.Abs(scrollInput));
            scrollInput = Mathf.Clamp(scrollInput, -1, 1);

            selectedCandyIndex += (int)scrollInput;

            // Wrap around if needed
            if (selectedCandyIndex < candyMinIndex)
            {
                selectedCandyIndex = candyMaxIndex;
            }
            else if (selectedCandyIndex > candyMaxIndex)
            {
                selectedCandyIndex = candyMinIndex;
            }

            // Select next candy
            SelectCandy(selectedCandyIndex);
        }

        if (candyTimer > 0)
        {
            candyTimer -= Time.deltaTime;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            SpawnCandy();
        }
    }

    // Change which candy is selected
    private void SelectCandy(int candyIndex)
    {
        // If index is valid, change
        // else, do nothing
        if (candyIndex >= candyMinIndex && candyIndex <= candyMaxIndex)
        {
            selectedCandyIndex = candyIndex;
            selectedCandy = candyDataList.GetCandyDataObject(selectedCandyIndex);
            candySelectionUI.SelectCandy(candyIndex);
        }
    }

    private void SpawnCandy()
    {
        var candyObject = Instantiate(candyPrefab, transform.position, transform.rotation);
        Candy candyScript = candyObject.GetComponent<Candy>();

        candyScript.SetCandy(selectedCandy);

        candyTimer = timeBetweenCandy;
    }

    private void DoMove()
    {
        rigidBody.MovePosition(moveInput);
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

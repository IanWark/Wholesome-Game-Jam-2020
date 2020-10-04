using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Game Variables")]
    [SerializeField, Tooltip("How quickly they can move in X and Y directions.")]
    private Vector2 speed = new Vector2(0.2f, 0);
    private Vector2 newPosition = new Vector2();

    [SerializeField, Tooltip("Time we must wait before dropping another candy.")]
    private float timeBetweenCandy = 1;
    private float candyTimer = 0;

    [SerializeField, Tooltip("The distance that must be traveled in a frame to make the sprite flip.")]
    private float distanceToFlip = 0.1f;

    [Header("Technical References")]
    [SerializeField]
    private Transform candyPrefab = null;

    [SerializeField]
    private CandySelectionUIController candySelectionUI = null;

    [SerializeField]
    private AudioClip dropAudioClip = null;

    private CandyDataList candyDataList = null;
    private const int candyMinIndex = 0;
    private const int candyMaxIndex = 3;
    private int selectedCandyIndex = 1;
    private CandyDataList.CandyDataObject selectedCandy = null;

    private Rigidbody2D rigidBody = null;
    private SpriteRenderer sprite = null;
    private AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        candyDataList = FindObjectOfType<CandyDataHolder>().candyDataList;
        candySelectionUI.Setup(candyDataList);
        SelectCandy(0);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        DoMove();
    }

    private void HandleInput()
    {
        // Movement
        newPosition.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        newPosition.y = transform.position.y;

        // Candy Selection
        if (Input.GetButtonDown("Select 1"))
        {
            SelectCandy(0);
        }
        else if (Input.GetButtonDown("Select 2"))
        {
            SelectCandy(1);
        }
        else if(Input.GetButtonDown("Select 3"))
        {
            SelectCandy(2);
        }
        else if(Input.GetButtonDown("Select 4"))
        {
            SelectCandy(3);
        }
        else if(Input.GetButtonDown("Select 5"))
        {
            SelectCandy(4);
        }
        else if(Input.GetButtonDown("Select 6"))
        {
            SelectCandy(5);
        }
        else if(Input.GetButtonDown("Select 7"))
        {
            SelectCandy(6);
        }
        else if (Input.GetButtonDown("Select Next"))
        {
            // A/D or left/right also changes selected candy
            SelectCandyFromInputAxis(Input.GetAxis("Select Next"));
        }
        else
        {
            // Scrolling mouse also changes selected candy
            SelectCandyFromInputAxis(-1 * Input.GetAxis("Mouse ScrollWheel"));
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

    private void SelectCandyFromInputAxis(float input)
    {
        // round input to either -1 or 1
        input = Mathf.Sign(input) * Mathf.Ceil(Mathf.Abs(input));
        input = Mathf.Clamp(input, -1, 1);

        selectedCandyIndex += (int)input;

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

    private void SpawnCandy()
    {
        var candyObject = Instantiate(candyPrefab, transform.position, transform.rotation);
        Candy candyScript = candyObject.GetComponent<Candy>();

        candyScript.SetCandy(selectedCandy);
        audioSource.PlayOneShot(dropAudioClip);

        candyTimer = timeBetweenCandy;
    }

    private void DoMove()
    {
        FlipToMovement();
        rigidBody.MovePosition(newPosition);
    }

    // If going right, flip sprite
    // If going left, unflip sprite
    // If not moving, do not change spirte
    private void FlipToMovement()
    {
        float difference = newPosition.x - transform.position.x;

        if (Mathf.Abs(difference) > distanceToFlip)
        {
            if (difference > 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            else if (difference < 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
        }
    }
}

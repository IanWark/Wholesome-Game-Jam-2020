using UnityEngine;

public abstract class LittleBeast : MonoBehaviour
{
    [Header("Speech Bubble")]
    [SerializeField]
    private SpriteRenderer speechBubble = null;

    [SerializeField]
    private Sprite sadSprite = null;
    [SerializeField]
    private Sprite happySprite = null;

    [Header("Audio")]
    [SerializeField]
    private AudioClip sadAudioClip = null;
    [SerializeField]
    private AudioClip happyAudioClip = null;

    // Must be set by derived class!
    protected eCandyType preferredCandyType;
    protected CandyDataList.CandyDataObject preferredCandyData;

    // Can be set by derived class!
    protected int pointsValue = 1;

    protected float candySpeechTime = 10f;  // Time until candy demand speech bubbles appears
    protected float giveUpTime = 30f;       // Time until giving up and leaving

    public Vector2 movement = new Vector2();

    protected Rigidbody2D rigidBody = null;
    protected Collider2D ourCollider = null;
    protected SpriteRenderer sprite = null;
    private AudioSource audioSource = null;

    protected bool showingSpeechBubble = false;
    protected bool isLeaving = false;

    public eCandyType GetPreferredCandyType()
    {
        return preferredCandyType;
    }

    protected virtual void Start()
    {
        // Assumes preferredCandyType has been set by derived class!
        preferredCandyData = FindObjectOfType<CandyDataHolder>().candyDataList.GetCandyDataObject(preferredCandyType);

        rigidBody = GetComponent<Rigidbody2D>();
        ourCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = FindObjectOfType<MonsterAudioPlayer>().audioSource;

        speechBubble.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (!isLeaving)
        {
            candySpeechTime -= Time.deltaTime;
            if (!showingSpeechBubble && candySpeechTime <= 0)
            {
                ShowSpeechBubble(preferredCandyData.speechBubbleSprite);
            }

            giveUpTime -= Time.deltaTime;
            if (giveUpTime <= 0)
            {
                Unhappy();
            }
        }
    }

    // Candy controls the interaction, because of problems with multiple LBs being hit with 1 candy.
    public void RecieveCandy(eCandyType candyType)
    {
        if (candyType == preferredCandyType)
        {
            ShowSpeechBubble(happySprite);
            ScoringController scoringController = FindObjectOfType<ScoringController>();
            scoringController.IncreaseScore(pointsValue);
            audioSource.PlayOneShot(happyAudioClip);
        }
        else
        {
            Unhappy();
        }

        Leave();
    }

    private void Unhappy()
    {
        ShowSpeechBubble(sadSprite);
        ScoringController scoringController = FindObjectOfType<ScoringController>();
        scoringController.IncreaseStrikes();
        audioSource.PlayOneShot(sadAudioClip);
        Leave();
    }

    protected void Leave()
    {
        // Set flag to use leaving speed
        isLeaving = true;

        // Ignore walls/candy
        ourCollider.isTrigger = true;

        // Go to closest edge
        if (transform.position.x > 0)
        {
            movement.x = 1;
        }
        else
        {
            movement.x = -1;
        }
    }

    private void ShowSpeechBubble(Sprite sprite)
    {
        speechBubble.gameObject.SetActive(true);
        speechBubble.sprite = sprite;
        showingSpeechBubble = true;
    }

    // If going right, flip sprite
    // If going left, unflip sprite
    // If not moving, do not change spirte
    protected void FlipToMovement()
    {
        if (movement.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (movement.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // On colliding with a wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Change movement direction
        movement.x *= -1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If we left the walls, become real
        if (collision.gameObject.layer == 8)
        {
            ourCollider.isTrigger = false;
        }
        // If we left the background, despawn
        else if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}

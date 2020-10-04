using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CandySelectorUI : MonoBehaviour
{
    [SerializeField]
    private Image candyImage = null;
    [SerializeField]
    private Image selectorImage = null;
    [SerializeField]
    private TextMeshProUGUI numberText = null;

    [SerializeField]
    public Sprite activatedSprite = null;
    [SerializeField]
    public Sprite deactivatedSprite = null;

    public void Setup(CandyDataList.CandyDataObject candyData, int number)
    {
        candyImage.sprite = candyData.sprite;
        numberText.text = number.ToString();
    }

    public void Activate(bool activate)
    {
        selectorImage.sprite = activate ? activatedSprite : deactivatedSprite;
    }
}

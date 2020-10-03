using UnityEngine;
using UnityEngine.UI;

public class CandySelectorUI : MonoBehaviour
{
    [SerializeField]
    private Image candyImage = null;
    [SerializeField]
    private Image selectorImage = null;

    [SerializeField]
    public Sprite activatedSprite = null;
    [SerializeField]
    public Sprite deactivatedSprite = null;

    public void Setup(CandyDataList.CandyDataObject candyData)
    {
        candyImage.sprite = candyData.sprite;
    }

    public void Activate(bool activate)
    {
        selectorImage.sprite = activate ? activatedSprite : deactivatedSprite;
    }
}

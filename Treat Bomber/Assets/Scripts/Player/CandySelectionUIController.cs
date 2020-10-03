using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandySelectionUIController : MonoBehaviour
{
    [SerializeField]
    private float selectorWidth = 40;

    [SerializeField]
    private float selectorHeight = 40;

    [SerializeField]
    private Transform candySelectorPrefab = null;

    private List<CandySelectorUI> candySelectors = new List<CandySelectorUI>();

    public void Setup(CandyDataList candyList)
    {
        for (int i = 0; i < candyList.GetCount(); ++i)
        {
            CandySelectorUI newSelector = Instantiate(candySelectorPrefab, transform).gameObject.GetComponent<CandySelectorUI>();
            newSelector.Setup(candyList.GetCandyDataObject(i));

            candySelectors.Add(newSelector);
        }

        // Resize our component to fit everything
        RectTransform rectTransform = GetComponent<RectTransform>();
        HorizontalLayoutGroup horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        float width = (selectorWidth * candySelectors.Count) + (horizontalLayoutGroup.spacing * candySelectors.Count - 1);
        rectTransform.sizeDelta = new Vector2(width, selectorHeight);
        rectTransform.anchoredPosition = new Vector2((width / 2) + (horizontalLayoutGroup.spacing * 2), rectTransform.anchoredPosition.y);
    }

    public void SelectCandy(int index)
    {
        for(int i = 0; i < candySelectors.Count; ++i)
        {
            candySelectors[i].Activate(false);
        }

        candySelectors[index].Activate(true);
    }
}

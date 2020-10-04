using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "WholesomeGameJam/CandyData", order = 1)]
public class CandyDataList : ScriptableObject
{
    [System.Serializable]
    public class CandyDataObject
    {
        public eCandyType candyType;
        public Sprite sprite;
        public Sprite speechBubbleSprite;
        public string candyName;
    }
    
    [SerializeField]
    private List<CandyDataObject> candyList = new List<CandyDataObject>();

    public int GetCount()
    {
        return candyList.Count;
    }

    public CandyDataObject GetCandyDataObject(int index)
    {
        if(index < 0 || index > candyList.Count-1)
        {
            index = 0;
        }

        return candyList[index];
    }

    public CandyDataObject GetCandyDataObject(eCandyType candyType)
    {
        CandyDataObject value = candyList[0];

        for(int i = 0; i < candyList.Count; ++i)
        {
            if (candyList[i].candyType == candyType)
            {
                value = candyList[i];
            }
        }

        return value;
    }
}

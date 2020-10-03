using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "WholesomeGameJam/CandyData", order = 1)]
public class CandyDataList : ScriptableObject
{
    [System.Serializable]
    public struct CandyDataObject
    {
        public eCandyType candyType;
        public Sprite sprite;
        public string candyName;
    }
    
    // Would be nice if it was a dictionary, those don't serialize so well (I think)
    [SerializeField]
    private List<CandyDataObject> candyList = new List<CandyDataObject>();

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

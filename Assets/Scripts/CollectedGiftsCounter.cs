using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectedGiftsCounter : MonoBehaviour
{
    public static CollectedGiftsCounter instance;
    public TextMeshProUGUI text;
    int giftAmount;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdateCollectedGiftsCount(int giftValue)
    {
        giftAmount += giftValue;
        text.text = "Gift count: " + giftAmount.ToString();
    }
}

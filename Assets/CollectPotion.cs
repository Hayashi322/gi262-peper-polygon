using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectPotion : MonoBehaviour
{
    public static int PotionCount;
    public GameObject PotionCountDisplay;


    private void Start()
    {
        PotionCount = 0;
    }

    private void Update()
    {
        if (PotionCount != null)
        {
            PotionCountDisplay.GetComponent<TMP_Text>().text = "" + PotionCount;
        }

    }
}

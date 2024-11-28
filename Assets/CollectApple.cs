using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CollectApple : MonoBehaviour
{
    public static int AppleCount;
    public GameObject AppleCountDisplay;


    private void Start()
    {
        AppleCount = 0;
    }

    private void Update()
    {
        if (AppleCount != null)
        {
            AppleCountDisplay.GetComponent<TMP_Text>().text = "" + AppleCount;
        }

    }

}

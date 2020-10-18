using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CandyTextScript : MonoBehaviour
{
    Text text;
    public static int candyAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (candyAmount > 0)
        {
            text.text = "Candy: " + candyAmount;
        } else
        {
            text.text = "Candy: 0";
        }
    }
}

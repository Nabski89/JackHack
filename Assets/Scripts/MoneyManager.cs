using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static int MoneyAmount = 100;
    public static int BetDefault = 10;
    public static int BetWin = 10;
    public static int BetLose = 10;
    Text Textfield;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text =
        "Banked $ " + MoneyAmount.ToString()
        + "\n On Win $" + BetWin.ToString()
        + "\n On Lose $" + BetLose.ToString()
        ;
    }


}

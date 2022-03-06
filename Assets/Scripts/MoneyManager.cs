using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static float MoneyAmount = 100;
    public static float BetDefault = 10;
    public static float BetWin = 10;
    public static float BetLose = 10;
    public static float CursePoints = 0;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public DiceController myDiceController;

    private bool hasClicked;
    private bool isHolding;

    void Update()
    {


        if (Input.GetKey(KeyCode.Mouse0))
        {
            //USER is holding down Key
            hasClicked = true;
            isHolding = true;
        }
        else
        {
            isHolding = false;
        }

        if (hasClicked == true && isHolding == false)
        {
            myDiceController.PerformThrow();
        }




        if (Input.GetKeyDown(KeyCode.R))
        {
            myDiceController.PerformReset();
            hasClicked = false;
        }


        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

}

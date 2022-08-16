  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class key : MonoBehaviour
{
    #region gameKeyboard

    public Text spacebox;
    public Text text;
    private CanvasGroup cv;
    public CanvasGroup All_Buttons;

    private void Start()
    {  
        cv = GetComponent<CanvasGroup>();
        spacebox.text = "";
        
    }
    public void typeNumbers()
    {
        spacebox.text = spacebox.text + text.text;

        gamemanager.codeSend.decipher();

        cv.interactable = false;
        if (cv.interactable == false)
        {
            cv.alpha = 0.05f;
        }
        

        if (spacebox.text.Length == 4)
        {
            All_Buttons.interactable = false;
           
        }

       
    }
   
   
    #endregion

}



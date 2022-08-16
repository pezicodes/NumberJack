using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class clear : MonoBehaviour
{
    public Text spacebox;
    public static clear codeClear;
    public CanvasGroup[] All_Buttons;
    
    public void cleraAll()
    {
        spacebox.text = "";
        for (int i = 0; i < All_Buttons.Length; i++)
        {
            resetNumbers();
        }
       
    
    }

    public void resetNumbers()
    {
         for (int i = 0; i < All_Buttons.Length; i++)
        {
             All_Buttons[i].interactable = true;
             All_Buttons[i].alpha = 1;
        }   
        
    }

    private void Start()
    {
        codeClear = this;

    }
}

using UnityEngine;
using UnityEngine.UI;

public class quickkeys : MonoBehaviour
{
    #region gameKeyboard
    public Text text;
    private CanvasGroup cv;
    public CanvasGroup All_Buttons;

    int temp = 0;

    public QuickJackGameManager quickjackscript;

    private void Start()
    {  
        cv = GetComponent<CanvasGroup>();
        temp = quickjackscript.movesint;
        quickjackscript.Mytextbox[temp].text = "";
        
    }
    public void typeNumbers()
    {
        quickjackscript.Mytextbox[temp].text = 
        quickjackscript.Mytextbox[temp].text + text.text;

        cv.interactable = false;
        if (cv.interactable == false)
        {
            cv.alpha = 0.05f;
        }
        

        if (quickjackscript.Mytextbox[temp].text.Length == 4)
        {
            All_Buttons.interactable = false;
            //temp++;
           
        }

       
    }
   
   
    #endregion

    void Update()
    {
        temp = quickjackscript.movesint;
    }

}



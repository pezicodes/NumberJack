using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float secondsCount;
    public int minuteCount;
    public int hourCount;
    
    public GameObject Gold, Silver;

    public void UpdateTimerUI(){
         //set timer UI
        secondsCount += Time.deltaTime;
       
        if(secondsCount >= 60){
            minuteCount++;
            secondsCount = 0;
        }

        else if(minuteCount >= 60){
            hourCount++;
            minuteCount = 0;     
        }   
    }

    
    public void checkfortimebar(){
        if(minuteCount >= 0 && minuteCount < 4){
            Gold.SetActive(true);
            Silver.SetActive(false); 
            QuickJackGameManager.quickjackscript.WinMedal = true;
        }

        else if(minuteCount >= 4 && minuteCount < 8){

            Gold.SetActive(false);
            Silver.SetActive(true); 
            QuickJackGameManager.quickjackscript.WinMedal = true;
        }

        else{
            Gold.SetActive(false);
            Silver.SetActive(false); 
            QuickJackGameManager.quickjackscript.WinMedal = true;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
        checkfortimebar();
    }

}

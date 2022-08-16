using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTimer : MonoBehaviour
{
    public float secondsCount;
    public int minuteCount;
    public int hourCount;
    
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



    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
    }

}

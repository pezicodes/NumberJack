using System.Collections.Generic;
using UnityEngine;           
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class MultiplayerAddNumber : MonoBehaviour
{
    #region Main Game Play Variables 

    public static MultiplayerAddNumber multiplayerNumber;

    public Text Mytextbox;
    public Button StartBtn;
    string myEntries;

    public GameObject _gameplay;
    public GameObject _room;



    #endregion

    #region GameManager's Methods
    public void Start()
    {
        multiplayerNumber = this;

        

        ColorUtility.TryParseHtmlString
        ("#9DD300", out greenColor);

        ColorUtility.TryParseHtmlString
        ("#FFFFFF", out defaultColor);
    
        myEntries = Mytextbox.text;
        myEntries.ToArray();   
        #endregion
    }

    public void clearMemory()
    {
        reset();
    }
 
    private void Update()
    {  
        
        #region Check input before play
        if (Mytextbox.text.Length > 3)
        {
            StartBtn.interactable = true;
        }

        else{
            StartBtn.interactable = false;
        }

        #endregion

    } 

    #region Select Tool
    //CHEAT
    [Space]
    [Space]
    [Header("Select Numbers Tool")]
    public Button[] buttons;
    //public Text[] texts;
    public Color greenColor;
    public Color defaultColor;  
    
    public void reset(){

        clear.codeClear.cleraAll();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = defaultColor;
            Text btntext = buttons[i].GetComponentInChildren<Text>();
            btntext.text = "";
        } 
    }
    
    public void decipher(){
        //pezicodes 
        myEntries = Mytextbox.text;
        myEntries.ToArray();

        for (int i = 0; i < myEntries.Length; i++)
        {
            buttons[i].image.color = greenColor;
            Text btntext = buttons[i].GetComponentInChildren<Text>();
            btntext.text = myEntries[i].ToString();
           
        }

    }
    #endregion  

    public void Send()
    {
        AddNumberManager.InstanceAddNumber.SendChat(AddNumberManager.InstanceAddNumber.chatView_INPUT.text);
        _gameplay.SetActive(true);
        _room.SetActive(false);
    }
}

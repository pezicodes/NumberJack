using System.Collections.Generic;
using UnityEngine;           
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class MultiplayerManager : MonoBehaviour
{
    #region Main Game Play Variables 
    public static MultiplayerManager multiPlay;

    public PracticeTimer timerscript;
    public CanvasGroup PlayButton;
    List<string> Results = new List<string>();
    List<string> OppNum = new List<string>();
    List<string> returned = new List<string>();

    char first_opp, second_opp, third_opp, fourth_opp;
    char first_my, second_my, third_my, fourth_my;
    public Text Mytextbox;
    
    string myEntries;
    string OppEntries;
    string result;
    string Opptextbox;
    
    public GameObject GuessChatObject;
    public Transform LocalPlayerHistory;
    public Transform OtherPlayerHistory;
    //movescounters
    int moveCounter;
    public Text moveCount;

    int D, W;

    public GameObject GAMEPLAY, VICTORY, FAILED;

    
    #endregion

    #region GameManager's Methods
    public void Start()
    {
        PlayerPrefs.SetInt("Dead", 0); //clear dead count - restarting game 
        
        multiPlay = this;
        timerscript.enabled = true;

        print(moveCounter);
        moveCount.text = "Moves: " + moveCounter.ToString();

        if(!(LocalPlayerHistory.childCount <= 0)){
            {
                Destroy(gameObject);
            }
        }

        ColorUtility.TryParseHtmlString
        ("#9DD300", out greenColor);

        ColorUtility.TryParseHtmlString
        ("#FFFFFF", out defaultColor);
    
        myEntries = Mytextbox.text;
        myEntries.ToArray();

        moveCount.text = moveCounter.ToString();
        GAMEPLAY.SetActive(false);
        VICTORY.SetActive(false);
        FAILED.SetActive(false);
        
        D = 0;
        W = 0;

        
    }

    public void Restart()
    {
       
        SceneManager.LoadScene("Practice");
        
    }

    public void Menu()
    {   
        SceneManager.LoadScene("Menu");
    }

    [TextArea]
    public string[] Pezi_NumberJack_Console;
    public int MessageInt = 0;
    public string ChatText;
    public string test;

    void checkDeadandWounded()
    {
        for (int i = 0; i < 4; i++)
        {
            if (myEntries[0] == Opptextbox[i])
            {
                if ((i + 1) == 1)
                {
                    Results.Add("X");
                }

                else
                {
                    Results.Add("Y");
                }

            }

            else if (myEntries[1] == Opptextbox[i])
            {
                if ((i + 1) == 2)
                {
                    Results.Add("X");
                }

                else
                {
                    Results.Add("Y");
                }
            }

            else if (myEntries[2] == Opptextbox[i])
            {
                if ((i + 1) == 3)
                {
                    Results.Add("X");
                }

                else
                {
                    Results.Add("Y");
                }

            }

            else if (myEntries[3] == Opptextbox[i])
            {
                if ((i + 1) == 4)
                {
                    Results.Add("X");
                }

                else
                {
                    Results.Add("Y");
                }

            }

            else
            {
                Results.Add("O");
            }

        }

        foreach (var item in Results)
        {
            result += item.ToString() + ",";
        }
        string[] temp;
        temp = result.Split(',');

        for (int i = 0; i < 4; i++)
        {
            if (temp[i].Equals("X"))
            {
                D = D + 1;
            }

            else if (temp[i].Equals("Y"))
            {
                W = W + 1;
            }
        }

        //Local Relay

        
        //Server Relay
        ChatManager.InstanceChat.SendChat(ChatManager.InstanceChat.chatView_INPUT.text + "-" + D.ToString() + "d" + "-" + W.ToString() + "w");
        ChatText = ChatManager.InstanceChat.chatView_TEXT.text;

        test += "Test:" + myEntries + "-" + D.ToString() + "d" + "-" + W.ToString() + "w" + "\n";
        Debug.LogWarning(test);
        
        //DOUBLE UPDATE

        //String Formatting
        if (test.Contains("\n"))
        {
            Pezi_NumberJack_Console = test.Split("\n");
        }

        else
        {
            Pezi_NumberJack_Console[MessageInt] = test;
        }
        

       /* // entry =  [Aniki]:5678-0d-0w
        string[] div1 = Pezi_NumberJack_Console[MessageInt].Split(":");
        Debug.LogWarning(Pezi_NumberJack_Console[MessageInt]);
        // result = [Aniki], 5678-0d-0w

        // entry =  5678-0d-0w
        string[] div2 = div1[1].Split("-");
        // result = 5678, 0d, 0w*/


        /*GameObject OtherPlayerGuess = Instantiate(GuessChatObject, OtherPlayerHistory);
        Text[] OtherPlayerTexts = OtherPlayerGuess.GetComponentsInChildren<Text>();
        OtherPlayerTexts[0].text = div2[0];
        OtherPlayerTexts[1].text = div2[1];
        OtherPlayerTexts[2].text = div2[2];*/

        GameObject LocalPlayerGuess = Instantiate(GuessChatObject, LocalPlayerHistory);
        Text[] LocalPlayerTexts = LocalPlayerGuess.GetComponentsInChildren<Text>();
        LocalPlayerTexts[0].text = myEntries;
        LocalPlayerTexts[1].text = D.ToString() + "d";
        LocalPlayerTexts[2].text = W.ToString() + "w";
        PlayerPrefs.SetInt("Dead", D);


        //Clearing Local and Server Prefs
        ChatManager.InstanceChat.chatView_INPUT.text = "";
        clear.codeClear.cleraAll();
        clearMemory();

      


    }

    public void generateNumber()
    {
        //yield return Opptextbox;
        Opptextbox = PlayerPrefs.GetString("PLAYER_NUM");
        Debug.Log("PlayerNumber = " + Opptextbox);
/*      
        for (int i = 0; i < 4; i++)
        {
            int rush = UnityEngine.Random.Range(0, OppNum.Count);

            Opptextbox = Opptextbox + OppNum.ElementAt(rush);

            OppNum.RemoveAt(rush);

            print(Opptextbox);

        }*/

        Opptextbox.ToArray();
               
        winscreenText();
        //losescreenText();

    }

    public void clearMemory()
    {
        result = "";
        Results.Clear();
        reset();

        D = 0;
        W = 0;
    }
    
    void Win()
    {   
        GAMEPLAY.SetActive(false);
        VICTORY.SetActive(true);
    }

    
    private void Update()
    {
        #region ChatManager Branch

        ChatText = ChatManager.InstanceChat.chatView_TEXT.text;
        //DOUBLE UPDATE

        #endregion

        moveCount.text = "Moves: " + moveCounter.ToString();
        #region Check input before play
        if (Mytextbox.text.Length > 3)
        {
            PlayButton.interactable = true;
        }

        else{
            PlayButton.interactable = false;
        }

        #endregion

        //Win
        if (PlayerPrefs.GetInt("Dead") == 4)
        {
            Invoke("Win", 1f);                  
        }


    }

    public void Send_Play()
    {   
        moveCounter++;
        
        moveCount.text = "Moves: " + moveCounter.ToString();

        #region Setting Entries
        myEntries = Mytextbox.text;
        myEntries.ToArray();
        OppEntries = Opptextbox;
        OppEntries.ToArray();

        // Me(player1)
        first_my = myEntries[0];
        second_my = myEntries[1];
        third_my = myEntries[2];
        fourth_my = myEntries[3];

        // Opponent
        first_opp = OppEntries[0];
        second_opp = OppEntries[1];
        third_opp = OppEntries[2];
        fourth_opp = OppEntries[3];
        #endregion
        checkDeadandWounded();

    }

    public void Quit() => Application.Quit();

    #endregion

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

    #region Win Screen Function
    [Space]
    [Space]
    [Header("Win Screen Function")]
    public Text[] winText;

    public void winscreenText(){

        myEntries = Opptextbox;
        myEntries.ToArray();

        for (int i = 0; i < winText.Length; i++)
        {
            winText[i].text = myEntries[i].ToString();
        }
    }

    #endregion

    // #region Lose Screen Function
    // [Space]
    // [Space]
    // [Header("Lose Screen Function")]
    // public Text[] loseText;
    // public Text[] winloseText;
    // public Image[] losebox;

    // public void losescreenText(){

    //     myEntries = Mytextbox.text;
    //     myEntries.ToArray();

    //     string temp = Opptextbox;
    //     temp.ToArray();



    //     for (int i = 0; i < loseText.Length; i++)
    //     {
    //         winloseText[i].text = temp[i].ToString();
    //         loseText[i].text = myEntries[i].ToString();
    //         if(loseText[i].text != winloseText[i].text){
    //             losebox[i].color = greenColor;
    //         }
            
    //         else{
    //             losebox[i].color = practice;
    //         }
    //     }
    // }

    // #endregion

    //public Text timerText;
     

     //

        
    
}

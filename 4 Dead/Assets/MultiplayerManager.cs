using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class MultiplayerManager : MonoBehaviour
{
    #region Main Game Play Variables 
    //public static gamemanager codeSend;

    public PracticeTimer timerscript;
    public CanvasGroup PlayButton;
    List<string> Results = new List<string>();
    List<string> OppNum = new List<string>();
    List<string> hammerchoices = new List<string>();
    List<string> scannerchoices = new List<string>();
    List<string> returned = new List<string>();

    char first_opp, second_opp, third_opp, fourth_opp;
    char first_my, second_my, third_my, fourth_my;
    public Text Mytextbox;
    
    string myEntries;
    string OppEntries;
    string result;
    string Opptextbox;
    public Text trialresults, deadresults, woundedresults;


    //movescounters
    int moveCounter;
    public Text moveCount;
    public Text winpractice_moves;

    //time counters
    public Text winpractice_time;

    int D, W;

    public GameObject GAMEPLAY, VICTORY, FAILED;

    public GameObject[] PowerUps_Holder;

    
    #endregion

    #region GameManager's Methods
    public void Start()
     {
       // codeSend = this;
        timerscript.enabled = true;
        trialresults.text = "";

        deadresults.text = "";

        woundedresults.text = "";

        ColorUtility.TryParseHtmlString
        ("#A5C87C", out greenColor);

        ColorUtility.TryParseHtmlString
        ("#94C5D3", out defaultColor);

        ColorUtility.TryParseHtmlString
        ("#E7666B", out redColor);

        ColorUtility.TryParseHtmlString
        ("#EF90BA", out hammercolor);
    
        myEntries = Mytextbox.text;
        myEntries.ToArray();

        moveCount.text = moveCounter.ToString();
        GAMEPLAY.SetActive(true);
        VICTORY.SetActive(false);
        FAILED.SetActive(false);
        
        D = 0;
        W = 0;

        #region Opponent's Number Creation
        OppNum.Add("0");
        OppNum.Add("1");
        OppNum.Add("2");
        OppNum.Add("3");
        OppNum.Add("4");
        OppNum.Add("5");
        OppNum.Add("6");
        OppNum.Add("7");
        OppNum.Add("8");
        OppNum.Add("9");

        
        #endregion

        generateNumber();

        
        hammerupdate();
        Hammerin.SetActive(false);
        Hammerout.SetActive(false);

        scanupdate();
        Scanin.SetActive(false);
        Scanout.SetActive(false);
    
        bombupdate();
        Bombin.SetActive(false);
        Bombout.SetActive(false);
    }

    public void UseOnce(){
        for (int i = 0; i < 1; i++){
            PowerUps_Holder[i].SetActive(true);
            PowerUps_Holder[i+1].SetActive(false);
        } 
        //lazy, dumb codeeeeeeeeeee
        //This was very lazy of you bro, 
        //you're a mumu for writing this abomination
    }
 
    public void Restart()
    {
       
        SceneManager.LoadScene("Practice");
        
    }

    public void Menu()
    {   
        SceneManager.LoadScene("Menu");
    }

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
        
       
        

        if(trialresults.text == ""){
            trialresults.text = myEntries;
            deadresults.text = D.ToString();
            woundedresults.text = W.ToString();
        }

        else{

            trialresults.text = trialresults.text + "\n" + myEntries;
            deadresults.text = deadresults.text + "\n" + D;
            woundedresults.text = woundedresults.text + "\n" + W;
        }
        //losescreenText();
        
        clear.codeClear.cleraAll();
        clearMemory();

    }
  
    public void generateNumber()
    {
        //yield return Opptextbox;
        Opptextbox = "";

        for (int i = 0; i < 4; i++)
        {
            int rush = UnityEngine.Random.Range(0, OppNum.Count);

            Opptextbox = Opptextbox + OppNum.ElementAt(rush);

            OppNum.RemoveAt(rush);

            print(Opptextbox);

            
            
        }
            
        Opptextbox.ToArray();
        for (int j = 0; j < Opptextbox.Count(); j++){   
            hammerchoices.Add(Opptextbox[j].ToString());
            scannerchoices.Add(Opptextbox[j].ToString());
        }    
        
        winscreenText();
        //losescreenText();


    }

    public void clearMemory()
    {
        result = "";
        //displayResult.text = "";
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

    // void Lose()
    // {
    //     GAMEPLAY.SetActive(false);
    //     FAILED.SetActive(true);
        
    // }
 
    private void Update()
    {   
        #region Time Formatting
        if((int)timerscript.secondsCount < 10){
            timerText.text = 
        (timerscript.minuteCount +":"+"0"+(int)timerscript.secondsCount).ToString();
        }

        else{
            timerText.text = 
        (timerscript.minuteCount +":"+ (int)timerscript.secondsCount)
        .ToString(); 
        }
        #endregion
        // timerText.text = (timerscript.minuteCount +":"+ (int)timerscript.secondsCount).ToString();


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
        if (deadresults.text.Contains("4"))
        {

            FastestPlayMoveTimer();
            
            winpractice_moves.text = moveCounter.ToString();
            winpractice_time.text = timerText.text;
            Invoke("Win", 1f);
            
            
        }
    }

    void FastestPlayMoveTimer(){
        
        #region Live Time
        string livetemp = timerText.text;
        string[] livetime = livetemp.Split(':');  
        string livemin = livetime[0];
        string livesec = livetime[1];

        int liveminutes = int.Parse(livemin);
        int liveseconds = int.Parse(livesec);

        int livetotaltime = liveseconds + (liveminutes * 60);
        //print(livetotaltime);
        #endregion
        
        #region Saved Time
        string savedtemp = PlayerPrefs.GetString("PracticeTime");
        string[] savedtime = savedtemp.Split(':');  
        string savedmin = savedtime[0];
        string savedsec = savedtime[1];

        int savedminutes = int.Parse(savedmin);
        int savedseconds = int.Parse(savedsec);

        int savedtotaltime = savedseconds + (savedminutes * 60);
        //print(savedtotaltime);

        #endregion

        
        timerscript.enabled = false;
        int temp = int.Parse(PlayerPrefs.GetString("PracticeMoves"));
           
        if(temp == 0){
            PlayerPrefs.SetString("PracticeMoves", moveCounter.ToString());
            PlayerPrefs.SetString("PracticeTime", timerText.text);
        }

        else{
            if(temp > moveCounter && livetotaltime  < savedtotaltime){
                PlayerPrefs.SetString("PracticeMoves", moveCounter.ToString());
                PlayerPrefs.SetString("PracticeTime", timerText.text);   
            }

            else{
                PlayerPrefs.SetString("PracticeMoves", temp.ToString());
                    
            }
        }

    }

    
    public void Send_Play()
    {   //SlideEntry.SE.checkifempty();

        moveCounter++;
        
        moveCount.text = moveCounter.ToString();

        //PlayerPrefs.SetString("PracticeMoves", moveCount.text);
        //PlayerPrefs.SetString("PracticeTime", timerText.text);

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
    public Color redColor;  

    public Color hammercolor; 

    public Color practice; 
    public Color practicewounded; 

    

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

        //retryy

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

     public Text timerText;
     

}

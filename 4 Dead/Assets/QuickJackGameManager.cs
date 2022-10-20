using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

//pezicodes

public class QuickJackGameManager : MonoBehaviour
{
    #region Main Game Play Variables 

    public Timer timerscript;
    public CanvasGroup PlayButton;
    List<string> Results = new List<string>();
    List<string> OppNum = new List<string>();
    char first_opp, second_opp, third_opp, fourth_opp;
    char first_my, second_my, third_my, fourth_my;
    public Text[] Mytextbox;
    string myEntries;
    string OppEntries;
    string result;
    string Opptextbox;
    public Text[] trialresults, deadresults, woundedresults;

    public static QuickJackGameManager quickjackscript;

    public Text timerText;

    //time counters
    public Text winpractice_time;
    int D, W;
    public GameObject GAMEPLAY, VICTORY, FAILED;
    public GameObject coinsplash, gemsplash;

    public GameObject NumbersandButtons, Forward;

    public Text[] playerCoins, playerGems;
    public int playerCoinsCount, playerGemsCount;
    public int movesint;


    #endregion

    void CoinGemUpdates() {

        playerCoinsCount = PlayerPrefs.GetInt("PCOINS");
        for (int i = 0; i < playerCoins.Length; i++)
        {
            playerCoins[i].text = playerCoinsCount.ToString();
        }


        playerGemsCount = PlayerPrefs.GetInt("PGEMS");
        for (int i = 0; i < playerGems.Length; i++)
        {
            playerGems[i].text = playerGemsCount.ToString();
        }
    }

    #region GameManager's Methods
    public void Start()
    {

        NumbersandButtons.SetActive(true);
        Forward.SetActive(false);
        coinsplash.SetActive(true);
        gemsplash.SetActive(true);

        quickjackscript = this;
        movesint = 0;

        for (int i = 0; i < Mytextbox.Length; i++) {
            trialresults[i].text = "";
            deadresults[i].text = "";
            woundedresults[i].text = "";
            Mytextbox[i].text = "";
        }


        myEntries = Mytextbox[movesint].text;
        myEntries.ToArray();



        VICTORY.SetActive(false);
        FAILED.SetActive(false);

        D = 0;
        W = 0;

        CoinGemUpdates();

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


        if (trialresults[movesint].text == "") {
            trialresults[movesint].text = myEntries;
            deadresults[movesint].text = D.ToString();
            woundedresults[movesint].text = W.ToString();
            movesint++;
        }

        //Win
        for (int i = 0; i < deadresults.Length; i++) {

            if (deadresults[i].text.Contains("4"))
            {

                // PlayerPrefs.SetString("Time", timerText.text);


                Invoke("Win", 0.25f);

                timerscript.enabled = false;


            }

        }

        //LOSE
        if (movesint >= 5 && timerscript.enabled == true) {
            timerscript.enabled = false;

            winpractice_time.text = PlayerPrefs.GetString("Time");
            Invoke("Lose", 0.5f);


        }
        clear.codeClear.cleraAll();
        clearMemory();
        CoinGemUpdates();

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

        //winscreenText();
        //losescreenText();


    }

    public void ViewHistory() {
        GAMEPLAY.SetActive(true);
        FAILED.SetActive(false);
    }

    public void ExitHistory() {
        GAMEPLAY.SetActive(false);
        FAILED.SetActive(true);
    }

    public void clearMemory()
    {

        if (movesint < 5) {
            Mytextbox[movesint].text = "";
        }

        result = "";
        //displayResult.text = "";
        Results.Clear();


        D = 0;
        W = 0;
    }

    void Win()
    {
        GAMEPLAY.SetActive(false);
        VICTORY.SetActive(true);

        winscreenText();

        Invoke("OffParticles", 5f);

    }

    void OffParticles() {
        coinsplash.SetActive(false);
        gemsplash.SetActive(false);
    }

    void Lose()
    {
        GAMEPLAY.SetActive(false);
        FAILED.SetActive(true);

        NumbersandButtons.SetActive(false);
        Forward.SetActive(true);

        losescreenText();


    }

    private void Update()
    {

        #region Time Formatting
        if ((int)timerscript.secondsCount < 10) {
            timerText.text =
        (timerscript.minuteCount + ":" + "0" + (int)timerscript.secondsCount).ToString();
        }

        else {
            timerText.text =
        (timerscript.minuteCount + ":" + (int)timerscript.secondsCount)
        .ToString();
        }
        #endregion

        if (movesint < 5) {
            //Check input before play
            if (Mytextbox[movesint].text.Length > 3)
            {
                PlayButton.interactable = true;
            }

            else {
                PlayButton.interactable = false;
            }
        }

        PlayerPrefs.SetString("Time", timerText.text);

    }

    public void Send_Play()
    {
        #region Setting Entries
        myEntries = Mytextbox[movesint].text;
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

    #endregion
    public Color jack;
    public Color jackwounded;

    #region Win Screen Function
    [Space]
    [Space]
    [Header("Win Screen Function")]
    public Text[] winText;

    public Text[] rewards;
    public Text[] extrarewards;

    public GameObject[] Medals;

    public bool WinMedal = true;
    public void winscreenText() {

        myEntries = Opptextbox;
        myEntries.ToArray();

        for (int i = 0; i < winText.Length; i++)
        {
            winText[i].text = myEntries[i].ToString();
        }

        #region private variables
        int x = 0;
        string coinreward = QuickJackManager.quickjackManagerscript.jackCoinRewardtext.text;
        string gemreward = QuickJackManager.quickjackManagerscript.jackGemRewardtext.text;

        string GOLDcoinreward = "+" + QuickJackManager.quickjackManagerscript.jackGoldCoin.ToString();
        string GOLDgemreward = "+" + QuickJackManager.quickjackManagerscript.jackGoldGem.ToString();

        string SILVERcoinreward = "+" + QuickJackManager.quickjackManagerscript.jackSilverCoin.ToString();
        string SILVERgemreward = "+" + QuickJackManager.quickjackManagerscript.jackSilverGem.ToString();
        #endregion

        rewards[x].text = coinreward;
        rewards[x + 1].text = gemreward;

        int coins = int.Parse(coinreward);
        int gems = int.Parse(gemreward);

        if (timerscript.minuteCount >= 0 && timerscript.minuteCount < 4) {
            extrarewards[x].text = GOLDcoinreward;
            extrarewards[x + 1].text = GOLDgemreward;
            Medals[x].SetActive(true);

            int totalGoldCoins = coins + int.Parse(GOLDcoinreward) + PlayerPrefs.GetInt("PCOINS");
            int totalGoldGems = gems + int.Parse(GOLDgemreward) + PlayerPrefs.GetInt("PGEMS");

            PlayerPrefs.SetInt("PCOINS", totalGoldCoins);
            PlayerPrefs.SetInt("PGEMS", totalGoldGems);

        }

        else if (timerscript.minuteCount >= 4 && timerscript.minuteCount < 8) {
            extrarewards[x].text = SILVERcoinreward;
            extrarewards[x + 1].text = SILVERgemreward;
            Medals[x + 1].SetActive(true);

            int totalSilverCoins = coins + int.Parse(SILVERcoinreward) + PlayerPrefs.GetInt("PCOINS");
            int totalSilverGems = gems + int.Parse(SILVERgemreward) + PlayerPrefs.GetInt("PGEMS");

            PlayerPrefs.SetInt("PCOINS", totalSilverCoins);
            PlayerPrefs.SetInt("PGEMS", totalSilverGems);


        }

        else {
            extrarewards[x].text = "0";
            extrarewards[x + 1].text = "0";
            Medals[x + 2].SetActive(true);

            int newcoins = coins + PlayerPrefs.GetInt("PCOINS");
            int newgems = gems + PlayerPrefs.GetInt("PGEMS");

            PlayerPrefs.SetInt("PCOINS", newcoins);
            PlayerPrefs.SetInt("PGEMS", newgems);

        }

        winpractice_time.text = PlayerPrefs.GetString("Time");
        timerscript.enabled = false;





    }

    #endregion

    #region Lose Screen Function
    [Space]
    [Space]
    [Header("Lose Screen Function")]
    public Text[] winloseText;

    public void losescreenText() {

        myEntries = Opptextbox;
        myEntries.ToArray();

        for (int i = 0; i < winText.Length; i++)
        {
            winloseText[i].text = myEntries[i].ToString();
        }

    }

    #endregion




}


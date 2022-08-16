using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
public class Loading : MonoBehaviour
{
    private string emailInput, passwordInput;
    public Text errors;
    public Text loadingText;

    public Text loadCounter;
    public int load;
    public GameObject LOGIN;
    public GameObject Loading_Screen;
    public GameObject ENTER_USERNAME;
    private void Start()
    {


        emailInput = PlayerPrefs.GetString("LOG_E");
        passwordInput = PlayerPrefs.GetString("LOG_P");
        //print(emailInput);
        //print(passwordInput);

        Error.SetActive(false);
        Loader.SetActive(true);
        AppTitle.SetActive(true);
        RequestTimedOut.SetActive(false);


    }

    int TOTALPOINTS;

    public void CachePoints()
    {
        //Only two games are functioning at the moment.
        TOTALPOINTS = PlayerPrefs.GetInt("rocker_HSC") + PlayerPrefs.GetInt("catch_HSC") + PlayerPrefs.GetInt("ana_HSC");
        PlayerPrefs.SetInt("HIGHSCORE", TOTALPOINTS);

        TOTALPOINTS = PlayerPrefs.GetInt("HIGHSCORE");
        SendLeaderboardMainHighScore(TOTALPOINTS);
        //print("HighScore Saved");
        //print("Pezi this is Yutiee's HighScore: " + TOTALPOINTS.ToString() + " pts");

    }


    IEnumerator loader()
    {
        load = 0;
        int temp = Random.Range(18, 79);
        while (load < temp)
        {
            load += Random.Range(0, temp - load);
            loadCounter.text = load.ToString() + "%";
            yield return new WaitForSeconds(3);
        }




    }

   

    public void LoginButton()
    {
        StartCoroutine(loader());
        emailInput = PlayerPrefs.GetString("LOG_E");
        passwordInput = PlayerPrefs.GetString("LOG_P");
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput,
            Password = passwordInput,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        string name = "";

        loadingText.text = "Creating Profile......";

        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            

        }

        else
        {
            
            Loading_Screen.SetActive(false);
            LOGIN.SetActive(false);
            ENTER_USERNAME.SetActive(true);
        }

        print("this is the name" + name + "here");

        if (name == "")
        {
        
            Loading_Screen.SetActive(false);
            LOGIN.SetActive(false);
            ENTER_USERNAME.SetActive(true);
            
        }

        else 
        { 
        
        CachePoints();
        string loginID;

        loginID = result.PlayFabId;
        PlayerPrefs.SetString("ID", loginID);


        

        if (PlayerPrefs.GetInt("HIGHSCORE").Equals(""))
        {
            PlayerPrefs.SetInt("HIGHSCORE", 0);
            int val = PlayerPrefs.GetInt("HIGHSCORE");
            SendLeaderboardMainHighScore(val);
        }

        else
        {
            int val = PlayerPrefs.GetInt("HIGHSCORE");
            SendLeaderboardMainHighScore(val);
        }




        loadingText.text = "Loading Menu...";

        //PlayerPrefs.SetString("Username", name);

        }



    }

    public Loading l;
    public GameObject Error;
    public Animator loading;

    public GameObject AppTitle;
    public GameObject Loader;



    void OnError(PlayFabError error)
    {
        loading.enabled = false;
        errors.text = "Connection error, please check your internet connection and try again";
        Error.SetActive(true);
        //Debug.Log(error.GenerateErrorReport());

        l.enabled = false;
        Loader.SetActive(false);
        AppTitle.SetActive(false);
    }




    public void SendLeaderboardMainHighScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {

            Statistics = new List<StatisticUpdate> {

                new StatisticUpdate{
                    StatisticName = "Leaderboard",
                    Value = score,
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);

    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {

        
        load = 80;
        loadCounter.text = load.ToString() + "%";

        if (loadCounter.text == "80%")
        {
            Invoke("Dash", 1f);
        }
        


    }


    void Dash()
    {
        SceneManager.LoadScene("Menu");
    }
    

    public GameObject RequestTimedOut;
    public Text request;
 
    public void ErrorTooLong()
    {
        loading.enabled = false;
        request.text = "We're sorry, but your request has expired. Please reload the game.";
        RequestTimedOut.SetActive(true);
        Loader.SetActive(false);
        AppTitle.SetActive(false);
        l.enabled = false;

        Invoke("Quit", 1f);
    }

    public void Quit()
    {
        Application.Quit();
    }


}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;

public class LoadingDashboard : MonoBehaviour
{
    public static LoadingDashboard loadDB;
    //Normal Screens
    [Header("UI Elements")]
    public Text DisplayNameText;
    public Text PositionText;
    public Text TotalText;
    
    public GameObject l;
    public GameObject dashscreen;
    public GameObject Error;

    public Text loadingText;

    public Animator loading;
   
    void Start()
    {
     
        //CachePoints();
        l.SetActive(true);
        StartCoroutine(loader());
        dashscreen.SetActive(false);

        loadDB = this;

        GetLeaderboard();
        DisplayNameText.text = PlayerPrefs.GetString("Username");
        PositionText.text = PlayerPrefs.GetString("Position");
        TotalText.text = PlayerPrefs.GetInt("HIGHSCORE").ToString() + "pts";
        TOTALPOINTS = PlayerPrefs.GetInt("HIGHSCORE");
     

        //for all games
        if ((PlayerPrefs.HasKey("rocker_HSC") && PlayerPrefs.HasKey("catch_HSC") && PlayerPrefs.HasKey("ana_HSC")) ||
            PlayerPrefs.GetInt("catch_HSC") != 0 && PlayerPrefs.GetInt("rocker_HSC") != 0 && PlayerPrefs.GetInt("ana_HSC") != 0)
        {
            return;
        }
        else
        {
            PlayerPrefs.SetInt("catch_HSC", 0);
            PlayerPrefs.SetInt("rocker_HSC", 0);
            PlayerPrefs.SetInt("ana_HSC", 0);
        }

    }

    public Text loadCounter;
    public int load = 50;


    int TOTALPOINTS;


    
   

    public void CachePoints()
    {
        //Only two games are functioning at the moment.
        TOTALPOINTS = PlayerPrefs.GetInt("rocker_HSC") + PlayerPrefs.GetInt("catch_HSC") + PlayerPrefs.GetInt("ana_HSC");

        //print(PlayerPrefs.GetInt("HIGHSCORE"));
        //print(PlayerPrefs.GetInt("rocker_HSC") + " SkyFall points");
        //print(PlayerPrefs.GetInt("catch_HSC") + " Catch points");

        PlayerPrefs.SetInt("HIGHSCORE", TOTALPOINTS);
        TOTALPOINTS = PlayerPrefs.GetInt("HIGHSCORE");
        
        dashboardManager.dm.SendLeaderboardMainHighScore(TOTALPOINTS);
       // print("HighScore Saved");

    }

    IEnumerator loader()
    {
        load = 50;
        int temp = Random.Range(68, 96);
        while (load < temp)
        {
            load += Random.Range(0, temp - load);
            loadCounter.text = load.ToString() + "%";
            yield return new WaitForSeconds(3);
        }


    }


    #region LEADERBOARDS
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScoreTable",
            StartPosition = 0,
            MaxResultsCount = 3
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {

        int i = 0;
        DisplayNameText.text = PlayerPrefs.GetString("Username");
        ColorUtility.TryParseHtmlString("#F0BA3B", out Color default_1Position);
        ColorUtility.TryParseHtmlString("#6FEC49", out Color default_2Position);
        ColorUtility.TryParseHtmlString("#EC4995", out Color default_3Position);

        ColorUtility.TryParseHtmlString("#b2beb5", out Color defaultPos);

        Color[] Ragers = { default_1Position, default_2Position, default_3Position };
        
        loadingText.text = "Refreshing Time Manager..";
        foreach (Transform item in dashboardManager.dm.rowParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(dashboardManager.dm.rowPrefab, dashboardManager.dm.rowParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();

            texts[0].text = "#" + (item.Position + 1).ToString();

            texts[1].text = item.DisplayName;

            if (i < 3)
            {
                texts[1].color = Ragers[i];
            }

            else
            {
                texts[1].color = defaultPos;
            }
            
            
            i += 1;
            texts[2].text = item.StatValue.ToString() + " pts";

         
            //for profile color
            if (item.DisplayName == PlayerPrefs.GetString("Username"))
            {
                
                TotalText.text = item.StatValue.ToString() + " pts";
                
                if(item.StatValue == PlayerPrefs.GetInt("HIGHSCORE"))
                {
                    //
                    //print("HIGHSCORE INTEGRITY !!!!!");
                    TotalText.text = item.StatValue.ToString() + " pts";

                }

                PositionText.text = "#" + (item.Position + 1).ToString();
                for (int x = 0; x < dashboardManager.dm.profileIcon.Length; x++)
                {
                    dashboardManager.dm.profileIcon[x].color = texts[1].color;                  
                }

                for (int x = 0; x < dashboardManager.dm.profileIconText.Length; x++)
                {
                    dashboardManager.dm.profileIconText[x].color = texts[1].color;
                }

            }

            loadingText.text = "Preparing Dashboard...";

        }
            load = 100;
            loadCounter.text = load.ToString() + "%";

            if (loadCounter.text == "100%")
            {
                loadingText.text = "Welcome " + PlayerPrefs.GetString("Username");
                Invoke("DashLoader", 1f);

            }   
    }

    void DashLoader()
    {
        l.SetActive(false);
        loading.enabled = false;

        dashscreen.SetActive(true);
    }


    void OnError(PlayFabError error)
    {
        
        Error.SetActive(true);
        Debug.Log(error.ErrorMessage);
        Debug.Log(error.GenerateErrorReport());
    }
    #endregion
    public void Quit()
    {
        Application.Quit();
    }




    public void DashboardRefresh()
    {
        GetLeaderboard();
        DisplayNameText.text = PlayerPrefs.GetString("Username");
        PositionText.text = PlayerPrefs.GetString("Position");
        TotalText.text = PlayerPrefs.GetInt("HIGHSCORE").ToString() + "pts";
        TOTALPOINTS = PlayerPrefs.GetInt("HIGHSCORE");

        //for all games
        if ((PlayerPrefs.HasKey("rocker_HSC") && PlayerPrefs.HasKey("catch_HSC") && PlayerPrefs.HasKey("ana_HSC")) ||
            PlayerPrefs.GetInt("catch_HSC") != 0 && PlayerPrefs.GetInt("rocker_HSC") != 0 && PlayerPrefs.GetInt("ana_HSC") != 0)
        {
            return;
        }
        else
        {
            PlayerPrefs.SetInt("catch_HSC", 0);
            PlayerPrefs.SetInt("rocker_HSC", 0);
            PlayerPrefs.SetInt("ana_HSC", 0);


        }
    }
}

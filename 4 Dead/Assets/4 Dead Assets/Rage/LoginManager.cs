using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;



public class LoginManager : MonoBehaviour
{
    public static LoginManager LM;
    
    [Header ("UI Elements")]
    public Text[] messageText;
    
    public InputField emailInput;
    public InputField passwordInput;
    public InputField usernameInput;

   

    [Header("Objects")]
    public GameObject LOGIN;
    public GameObject Loading_Screen;
    public GameObject ENTER_USERNAME;

   

    void Start()
    {

     
        LM = this;

        //no account logged in
        if ((PlayerPrefs.GetString("LOG_E").Equals(null) & PlayerPrefs.GetString("LOG_P").Equals(null)) | 
            (PlayerPrefs.GetString("LOG_E").Equals("") & PlayerPrefs.GetString("LOG_P").Equals("")))
        {
            Loading_Screen.SetActive(false);
            LOGIN.SetActive(true);
            ENTER_USERNAME.SetActive(false);
            

            PlayerPrefs.SetInt("HIGHSCORE", 0);
            int val = PlayerPrefs.GetInt("HIGHSCORE");
           
        }

        
        
        //Yes there's an account
        else
        {
            Loading_Screen.SetActive(true);
            LOGIN.SetActive(false);
            ENTER_USERNAME.SetActive(false);
            

            int val = PlayerPrefs.GetInt("HIGHSCORE");

            
        }
    }
    public void LoginButton()
    {
        
        var request = new LoginWithEmailAddressRequest {
            Email = emailInput.text,
            Password = passwordInput.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
            //Pezi wrote this
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);

    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "8C9DE"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "Password Reset Mail Sent";
            messageText[i].color = Color.green;
        }
        
    }
    string loginID;
    public void OnLoginSuccess(LoginResult result)
    {
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
        

        

        PlayerPrefs.SetString("LOG_E", emailInput.text);
        PlayerPrefs.SetString("LOG_P", passwordInput.text);
        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "Logged In Successfully !";
            messageText[i].color = Color.green;
        }
        string name = "";

        
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
            print("there is a profile");

        }

        else
        {
            print("there is no profile");
            Loading_Screen.SetActive(false);
            LOGIN.SetActive(false);
            ENTER_USERNAME.SetActive(true);
        }

        print("this is the name" + name + "here");

        if (name == "" || name == null)
        {
            Loading_Screen.SetActive(false);
            LOGIN.SetActive(false);
            ENTER_USERNAME.SetActive(true);
            print("no name");
        }

   
       
        else
        {
            PlayerPrefs.SetString("Username", name);
            print(PlayerPrefs.GetString("Username") + "Hmm");
            SceneManager.LoadScene("LOGIN");
        }

        

    }

    public void SaveNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest { 
        DisplayName = usernameInput.text,

        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        PlayerPrefs.SetString("Username", usernameInput.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "Saved Username Successfully !";
            messageText[i].color = Color.green;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "Registered Successfully !";
            messageText[i].color = Color.green;
        }
       
    }

    void OnSuccess(LoginResult result)
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "Successful, Login/Account Created !";
            messageText[i].color = Color.green;
        }
     
    }

    void OnError(PlayFabError error)
    {
        Debug.Log(error.Error);
       // Debug.Log(error.GenerateErrorReport());
        for (int i = 0; i < messageText.Length; i++)
        {
            if (error.Error.ToString() == "InvalidParams")
            {
                messageText[i].text = "wrong password or email address";
                messageText[i].color = Color.red;
            }

            else if (error.Error.ToString() == "ServiceUnavailable")
            {
                messageText[i].text = "no internet connection";
                messageText[i].color = Color.red;
            }

            else
            {
                messageText[i].text = error.Error.ToString();
                messageText[i].color = Color.red;
            }
            
        }

    }

    public void clean()
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "";
        }
    }

    #region LEADERBOARDS
    //MAIN
   

    //SKYFALL
    public void GetLeaderboardSkyFall()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScoreTable_Jappa",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGetSkyFall, OnError);
        //LEADERBOARD.SetActive(true);
        //LOGIN.SetActive(false);
        //ENTER_USERNAME.SetActive(false);
        //Loading_Screen.SetActive(false);
    }
    void OnLeaderboardGetSkyFall(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            PlayerPrefs.SetInt("rocker_HSC", item.StatValue);
        }
    }

    //CATCH
    public void GetLeaderboardCatch()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScoreTable_Catch",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGetCatch, OnError);
        
    }
    void OnLeaderboardGetCatch(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {

            PlayerPrefs.SetInt("catch_HSC", item.StatValue);
        }
    }

    public void GetLeaderboardAna()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScoreTable_Anagram",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGetAna, OnError);


    }
    void OnLeaderboardGetAna(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
          
             PlayerPrefs.SetInt("ana_HSC", item.StatValue);
         
        }

    }



    #endregion




    public void SendLeaderboardMainHighScore(int score)
    {
        
        var request = new UpdatePlayerStatisticsRequest
        {

            Statistics = new List<StatisticUpdate> {

                new StatisticUpdate{
                    StatisticName = "HighScoreTable",
                    Value = score,
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);

    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {

        Debug.Log("Successful Leaderboard Sent !");

      //  for (int i = 0; i < dashboardManager.dm.DisplayName.Length; i++)
      //  {
       //     dashboardManager.dm.DisplayName[i].text = PlayerPrefs.GetString("Username");
       // }
        
        GetLeaderboardSkyFall();
        GetLeaderboardCatch();
        GetLeaderboardAna();
    }



    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    public void Quit()
    {
        Application.Quit();
    }


}

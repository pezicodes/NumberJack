
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;


public class ProfileManager : MonoBehaviour
{
    public Text playername, uiText;

    public void LoadMenu(){
        SceneManager.LoadScene("Menu");   
    }   


    public void Update(){
            uiText.text = playername.text;
    }

    public static ProfileManager Profile;
    
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
        Profile = this;

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
        clean(); 
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
        clean();
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
            TitleId = "59186"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "Password Reset Mail Sent";
            messageText[i].color = Color.white;
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
            messageText[i].text = "Logged In successfully !";
            messageText[i].color = Color.white;
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
            SceneManager.LoadScene("Username");
        }

        

    }

    public void SaveNameButton()
    {
        clean();
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
            messageText[i].text = "Profile Created !";
            messageText[i].color = Color.white;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            //messageText[i].text = "Registered Successfully !";
            messageText[i].color = Color.white;
        }

        LoginButton();
    }

    void OnSuccess(LoginResult result)
    {
        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "Sign in Successful";
            messageText[i].color = Color.white;
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
                messageText[i].text = "invalid email or password too short";
                messageText[i].color = Color.red;
            }

            else if (error.Error.ToString() == "ServiceUnavailable")
            {
                messageText[i].text = "no internet connection";
                messageText[i].color = Color.red;
            }

            else if (error.Error.ToString().Contains("AccountNot"))
            {
                messageText[i].text = "Account not found, check email or Sign up?";
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

        Debug.Log("Successful Leaderboard Sent !");

      //  for (int i = 0; i < dashboardManager.dm.DisplayName.Length; i++)
      //  {
       //     dashboardManager.dm.DisplayName[i].text = PlayerPrefs.GetString("Username");
       // }
    
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


    public void Skip()
    {
        //skip to menu, lock all extras
        SceneManager.LoadScene("MenuLocked");
        int x = Random.Range(0,999);
        PlayerPrefs.SetString("Username", "player" + x);
    }


    public void omiNartsSignUp(){
        //Application.OpenURL();

        clean();
        passwordInput.text = "";

        for (int i = 0; i < messageText.Length; i++)
        {
            messageText[i].text = "Sign In";
        }
    
    }

     public void omiNartsHome(){
        //Application.OpenURL();
    
    }


    public void omiNartsInstagaram(){
        Application.OpenURL("https://www.instagram.com/ominarts/");
        
    }
    

}



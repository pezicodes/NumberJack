using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuickJackManager : MonoBehaviour
{
    //for quick jack landing page
    public GameObject jackLogo, jackLandingPage, jackGamePage;
    public Timer timescript;

    public static QuickJackManager quickjackManagerscript;


    #region Menu Functions
    public void quitJack(){
        SceneManager.LoadScene("Menu");
    }

    public void startJack(){

        jackLogo.SetActive(false);
        jackLandingPage.SetActive(false);
        jackGamePage.SetActive(true);

        timescript.enabled = true;
    }

    #endregion

    #region Reward Manager
    [Space]
    [Header("Reward Mechanism")]

    //reward texts
    public Text jackCoinRewardtext;
    public Text jackGemRewardtext;

    //[Range(1,100)]
    [SerializeField] 
    int jackCoinReward;

    //[Range(1,6)]
    [SerializeField]
    int jackGemReward;
    
    int CreateRewards(int val){

        int rewards = UnityEngine.Random.Range(6, val);
        return rewards;
    }

    //pezicodes 

    void RandomCoinReward()
    {
        int i = CreateRewards(20); //200 coins max
            i *= 10;
            jackCoinReward = i;
            string v = i.ToString();
            jackCoinRewardtext.text = v;
            return;
        
    }

    void RandomGemReward()
    {
        int i = CreateRewards(15); //15 gems max
        jackGemReward = i;
        string v = i.ToString();
        jackGemRewardtext.text = v;
        return;
        
    }

    #region Random Variables
    public int jackGoldCoin;
    [SerializeField] Text jackGoldCointext;
    public int jackGoldGem;
    [SerializeField] Text jackGoldGemtext;

    public int jackSilverCoin;
    [SerializeField] Text jackSilverCointext;

    public int jackSilverGem;
    [SerializeField] Text jackSilverGemtext;

    void RandomGold(){
        int i = Mathf.Abs(jackCoinReward/2);
        jackGoldCoin = i;
        string v = i.ToString();
        jackGoldCointext.text = "+" + v;

        int j = Mathf.Abs(jackGemReward/2);
        jackGoldGem = j;
        string x = j.ToString();
        jackGoldGemtext.text = "+" + x;


        return;
    } 

    void RandomSilver(){
        int i = Mathf.Abs(jackCoinReward/3);
        jackSilverCoin = i;
        string v = i.ToString();
        jackSilverCointext.text = "+" + v;

        int j = Mathf.Abs(jackGemReward/3);
        jackSilverGem = j;
        string x = j.ToString();
        jackSilverGemtext.text = "+" + x;


        return;
    }
   
    #endregion
    
    public void jackStart(){
        jackLogo.SetActive(false);
        jackLandingPage.SetActive(false);
        jackGamePage.SetActive(true);
    }

    #endregion
    
    void LoadRewards(){
        RandomCoinReward();
        RandomGemReward();
        RandomSilver();
        RandomGold();
    }
    //pezicodes
    void Start()
    {
        quickjackManagerscript = this;

        timescript.enabled = false;

        jackLogo.SetActive(true);
        jackLandingPage.SetActive(true);
        jackGamePage.SetActive(false);
        
        LoadRewards();
        
    }

   

}

using System.Threading;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//highscoreT.text = TimeSpan.FromSeconds(timeInSeconds).ToString("mm:ss");


public class MenuManager : MonoBehaviour
{
    [Space]
    public GameObject practicemenu, _MainMenu, _GameModesHolder, Dialog_box, 
    Buttons, Multimenu, LoadingScreen, CreateRoom, JoinRoom;

    public GameObject[] MenuScreens;

    [Space]
    public Text playername, practice_moves, practice_time;

    
    [Space]
    public Text playerCoins, playerGems;
    public int playerCoinsCount, playerGemsCount;

    public static MenuManager InstanceMenu;  



    void CoinGemUpdate(){
        if(PlayerPrefs.HasKey("PCOINS") && PlayerPrefs.HasKey("PGEMS")){
            playerCoinsCount = PlayerPrefs.GetInt("PCOINS");
            playerCoins.text = playerCoinsCount.ToString();

            playerGemsCount = PlayerPrefs.GetInt("PGEMS");
            playerGems.text = playerGemsCount.ToString();


        }

        else{
            PlayerPrefs.SetInt("PCOINS", 0);
            playerCoinsCount = PlayerPrefs.GetInt("PCOINS");
            playerCoins.text = playerCoinsCount.ToString();

            PlayerPrefs.SetInt("PGEMS", 0);
            playerGemsCount = PlayerPrefs.GetInt("PGEMS");
            playerGems.text = playerGemsCount.ToString();

        }
    }

    void Start()
    {   
        //print(PlayerPrefs.GetInt("PGEMS"));
        PlayerPrefs.SetString("Username", "pezicodes");

        InstanceMenu = this;
        CoinGemUpdate();
        
        _GameModesHolder.SetActive(false);
        _MainMenu.SetActive(true);

        Dialog_box.SetActive(true);

        playername.text = PlayerPrefs.GetString("Username");

        FastestPractice();
        
        Multimenu.SetActive(false);
        practicemenu.SetActive(false);
        LoadingScreen.SetActive(false);
        
        
    }

    void FastestPractice(){
        //moves
        if(PlayerPrefs.HasKey("PracticeMoves")){

            if(PlayerPrefs.GetString("PracticeMoves") == "1"){
                practice_moves.text = PlayerPrefs.GetString("PracticeMoves") + " move";
            }
            else{
                practice_moves.text = PlayerPrefs.GetString("PracticeMoves") + " moves";
            }

            practice_time.text = PlayerPrefs.GetString("PracticeTime");
        }
        
        else{
            PlayerPrefs.SetString("PracticeMoves", "0");
            PlayerPrefs.SetString("PracticeTime", "0:00");
        }

        
    }

    

    public void menuPlay()
    {
        _GameModesHolder.SetActive(true);
        Dialog_box.SetActive(true);
        _MainMenu.SetActive(false);
        CoinGemUpdate();
    }

    public void backmenuPlay()
    {
        _GameModesHolder.SetActive(false);
        _MainMenu.SetActive(true);
        CoinGemUpdate();
    }

    public void menuPractice()
    {
        _GameModesHolder.SetActive(false);
        practicemenu.SetActive(true);
    }

    public void quitPractice()
    {
        _GameModesHolder.SetActive(true);
        practicemenu.SetActive(false);
    }

    public void menuMulti()
    {
        _GameModesHolder.SetActive(false);
        LoadingScreen.SetActive(true);
        
    }

    public void quitMulti()
    {
        _GameModesHolder.SetActive(true);
        Multimenu.SetActive(false);
        LoadingScreen.SetActive(false);
    }

    public void playPractice(){
        SceneManager.LoadScene("Practice");
    }

     public void playQuickJack(){
        SceneManager.LoadScene("QuickJack");
    }

    public void offDialog(){
        Dialog_box.SetActive(false);
    }

    public void MenuButtons(int i){
        
        MenuScreens[i].SetActive(true);
        Buttons.SetActive(false);
    
    }

    public void offMenuButtons(int i){
        
        MenuScreens[i].SetActive(false);
        Buttons.SetActive(true);
    }

    public void multiCreate(){
        CreateRoom.SetActive(true);
        JoinRoom.SetActive(false);
        Multimenu.SetActive(false);
    }

    public void multiJoin(){
        CreateRoom.SetActive(false);
        JoinRoom.SetActive(true);
        Multimenu.SetActive(false);
    }

    public void BackFromMultModes(){
        CreateRoom.SetActive(false);
        JoinRoom.SetActive(false);
        Multimenu.SetActive(true);
    }

    public void omiNartsSignUp(){
        //Application.OpenURL();
    
    }

    public void omiNartsAbout(){
        //Application.OpenURL();
        //website
    
    }

    public void omiNartsInstagaram(){
        //omiNarts instagram 
        Application.OpenURL("https://www.instagram.com/ominarts/");
        
    }

    // public void numberjackInstagaram(){
    //     //numberjack instagram 
    //     Application.OpenURL("https://www.instagram.com/ominarts/");
        
    // }

}
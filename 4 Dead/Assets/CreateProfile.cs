using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CreateProfile : MonoBehaviour
{
    public Text Username;
    public Text UsernameHolder;

    public void SaveUser(){

        PlayerPrefs.SetString("Username", Username.text);
        StartCoroutine(JoinLobby());
    }

    IEnumerator JoinLobby()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Menu");
        Debug.Log("Loading Menu... ");
    }

    void Update(){
        UsernameHolder.text = Username.text;
    }

}
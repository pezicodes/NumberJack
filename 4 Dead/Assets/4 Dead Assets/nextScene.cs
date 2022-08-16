using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    public void next(){

        
        //load menu if username is saved 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        
    }
}


//pezicodes
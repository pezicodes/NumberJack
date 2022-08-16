using UnityEngine;
using UnityEngine.SceneManagement;
public class checkUsername : MonoBehaviour
{
    // Start is called before the first frame update
    public void checkUser()
    {
        if(!(PlayerPrefs.HasKey("Username"))){

            //to save player's username
            SceneManager.LoadScene("Username");
            return;
        }

        //load menu if username is saved 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        
    }

    
}

using UnityEngine;
using UnityEngine.SceneManagement;
public class checkUsername : MonoBehaviour
{
    // Start is called before the first frame update
    public void checkUser()
    {
        if(!(PlayerPrefs.HasKey("Username"))){

            //to save player's username
            AppManager.Instance.ChangeScene(AppManager.eSceneState.Username);
            return;
        }

        //load menu if username is saved 
        AppManager.Instance.ChangeScene(AppManager.eSceneState.omiN);
        
        
    }

    
}

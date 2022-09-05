using System.Collections;
using UnityEngine;

public class IntroManager : ServerManager
{

    private void AfterSplashScreen()
    {
        AppManager.Instance.ChangeScene(AppManager.eSceneState.Username);
    }
   

    
}

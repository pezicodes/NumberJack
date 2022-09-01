using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }
    #endregion

    #region LifeCycle
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public virtual void Exit_BTN()
    {
        Application.Quit();
    }
}

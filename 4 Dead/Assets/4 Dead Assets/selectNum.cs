using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectNum : MonoBehaviour
{
    public CanvasGroup[] CG = new CanvasGroup[10]; 

    void Start()
    {
        for (int i = 0; i < CG.Length; i++)
        {
            CG[i].alpha = 1;           
            CG[i].interactable = true;
        }
    }

    public void num_sel(int x = 0)
    {
        CG[x].alpha = 0.4f;
        //CG[x].interactable = false;
    }

    public void undo(int y = 0)
    {
        if (CG[y].alpha == 0.4f)
        {
            CG[y].alpha = 1;
        }
        else
        {
            return;
        }
    }

}


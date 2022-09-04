using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialog : MonoBehaviour
{
    public GameObject dialogbox;

    public void No() => dialogbox.SetActive(false);

    public void Yes(string temp){
        // dialogbox.SetActive(true);
        SceneManager.LoadScene(temp);
    }
}

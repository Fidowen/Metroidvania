using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuContral : MonoBehaviour
{ 
    // Start is called before the first frame update
    public void GameStart()
    {
        SceneManager.LoadScene(0);
    }
    public void Setting()
    {
        
    }
    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveHistory : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private string savescene;
    [SerializeField] private Vector2 savepos;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);//不刪
        SaveEnemy();//先存檔
    }

    public void SaveEnemy()//存檔
    {
        savescene = SceneManager.GetActiveScene().name;
        savepos = player.gameObject.transform.position;
    }
    public void DeadandLoad()//讀檔加載
    {
        SceneManager.LoadScene(savescene);
        player.transform.position = savepos;
    }
}

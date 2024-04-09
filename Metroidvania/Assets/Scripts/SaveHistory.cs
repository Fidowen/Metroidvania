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
        DontDestroyOnLoad(this.gameObject);//���R
        SaveEnemy();//���s��
    }

    public void SaveEnemy()//�s��
    {
        savescene = SceneManager.GetActiveScene().name;
        savepos = player.gameObject.transform.position;
    }
    public void DeadandLoad()//Ū�ɥ[��
    {
        SceneManager.LoadScene(savescene);
        player.transform.position = savepos;
    }
}

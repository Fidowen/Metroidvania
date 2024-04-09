using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryGameObj : MonoBehaviour
{

    public float speed = 0.1f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed*Time.deltaTime,0,0);
        Invoke("Distable", 1);
    }
    private void Distable()
    {
        this.gameObject.SetActive(false);
    }

}

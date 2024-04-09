using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContorler : MonoBehaviour
{
    float speed;
    float existTime;
    float radian;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float delteTime = Time.deltaTime;
        timer += delteTime ;
        if (timer > existTime)
        {
            Destroy(this.gameObject);
        }
        this.transform.position += new Vector3(speed * delteTime * Mathf.Sign(radian), speed * delteTime * Mathf.Cos(radian), 0);
    }
}

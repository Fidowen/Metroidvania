using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float moveSpeed;
    float attackMoveSpeed;
    Rigidbody2D rb;
    public bool isChase; //正在追擊 
    public bool isRturn;//正在返回
    public enum EnemyState
    {
        patrol,//巡邏
        chase, //追擊
        Return //失去目標
    }
    private EnemyState currentMode;
    private List<EnemyState> modeList = new List<EnemyState>();
    private void Awake()
    {
        currentMode = EnemyState.patrol;
        modeList.Add(currentMode);
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentMode)
        {
            case EnemyState.patrol:
                patrol();
                break;
            case EnemyState.chase:
                chase();
                break;
            case EnemyState.Return:
                EnemyReturn();
                break;
        }
    }
    public void SwitchMode(EnemyState newState)
    {
        //modeList.RemoveAt(modeList.Count-1);
        currentMode = newState;
        //modeList.Add(currentMode);
    }
    public virtual void patrol()
    {

    }
    public virtual void chase()
    {

    }
    public virtual void EnemyReturn()
    {

    }
    public bool CheckState()
    {
        return true;
    }


}

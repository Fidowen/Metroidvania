using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float moveSpeed;
    float attackMoveSpeed;
    Rigidbody2D rb;
    public bool isChase; //���b�l�� 
    public bool isRturn;//���b��^
    public enum EnemyState
    {
        patrol,//����
        chase, //�l��
        Return //���h�ؼ�
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

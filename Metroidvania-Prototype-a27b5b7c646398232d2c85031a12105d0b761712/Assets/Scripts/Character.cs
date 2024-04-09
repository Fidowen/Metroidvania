using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("血量")]
    public float maxHealth;
    public float CurrentHealth;

    [Header("無敵")]
    public float invulnerableDuration;
    private float invulnerableCount;
    public bool invulnerable;

    [Header("事件")]
    public UnityEvent<Transform> OnTakeDamege;
    public UnityEvent onDie;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCount -= Time.deltaTime;
            if (invulnerableCount <= 0) { invulnerable = false; }
        }
    }

    // Update is called once per frame
    public void TakeDamege(Attacker attack)
    {
        if (invulnerable) return;
        if (CurrentHealth-attack.damege>0)
        {
            CurrentHealth -= attack.damege;
            TriggerInvulnerable();
            OnTakeDamege?.Invoke(attack.transform);
        }
        else
        {
            CurrentHealth = 0;
            onDie?.Invoke();
        }        
    }
    private void TriggerInvulnerable()
    {
        if (!invulnerable) 
        {
            invulnerable = true;
            invulnerableCount = invulnerableDuration;
        }
    }
}

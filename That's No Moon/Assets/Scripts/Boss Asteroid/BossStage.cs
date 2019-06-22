using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossStage
{
    public float StageHealth { get; protected set; }
    protected System.Action<string> TransitionTo;

    public BossStage(float stageHealth)
    {
        StageHealth = stageHealth;
    }

    public virtual void Update()
    {

    }

    public virtual void OnEnter()
    {
        
    }

    public virtual void OnExit()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        StageHealth -= damage;

        if (StageHealth <= 0)
            StageDeath();
    }

    protected virtual void StageDeath()
    {
        Debug.Log("Oh no, big boy is dead.");
    }

    public void InitialiseTransitionTo(System.Action<string> func)
    {
        TransitionTo = func;
    }
}

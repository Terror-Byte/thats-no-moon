using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageOne : BossStage
{
    // public GameObject asteroidPrefab;

    public BossStageOne(float stageHealth) : base(stageHealth)
    {
        
    }

    public override void Update()
    {
        // Slowly move towards player and launch asteroids at them
    }

    public override void OnEnter()
    {
        
    }

    public override void OnExit()
    {
        
    }

    //public override void TakeDamage(float damage)
    //{

    //}

    protected override void StageDeath()
    {
        Debug.Log("Oh no, stage one has been defeated!");

        if (TransitionTo != null)
            TransitionTo("StageTwo");
        else
            Debug.LogError("BossStageOne TransitionTo delegate is null.");
    }
}

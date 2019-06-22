using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAsteroid : Enemy
{
    BossStage currentStage;
    Dictionary<string, BossStage> bossStages;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseStages();
    }

    // Update is called once per frame
    void Update()
    {
        currentStage.Update();
    }

    public override void TakeDamage(float damage)
    {
        currentStage.TakeDamage(damage);
    }

    private void InitialiseStages()
    {
        bossStages = new Dictionary<string, BossStage>();
        bossStages.Add("StageOne", new BossStageOne(1000));

        foreach (KeyValuePair<string, BossStage> stage in bossStages)
        {
            stage.Value.InitialiseTransitionTo(TransitionStage);
        }

        currentStage = bossStages["StageOne"];
        currentStage.OnEnter();
    }

    private void TransitionStage(string stageName)
    {
        if (!bossStages.ContainsKey(stageName))
        {
            Debug.LogError("There is no such boss stage to transition to: " + stageName);
            return;
        }

        currentStage.OnExit();
        currentStage = bossStages[stageName];
        currentStage.OnEnter();
    }

    //protected override void HealthZero()
    //{
    //    // Call current stages healthzero? Either transition to the next stage or die.
    //    if (parent)
    //        Destroy(parent);
    //}

    public override float Health
    {
        get
        {
            return currentStage.StageHealth;
        }
    }
}

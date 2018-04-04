using UnityEngine;
using System.Collections;

public class MonsterIA : IA{
    Transform Target;
    float StartRemainingDistance = 0;
    float StartSpeed;
    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        StartRemainingDistance = Vector3.Distance(Target.position, transform.position);
        //StartSpeed = GetAgentSpeed();
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SetDestination(Target);
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination(Target);

        float Distance = 0;

        Distance = Vector3.Distance(Target.position, transform.position);

        if (Distance > StartRemainingDistance)
        {
            SetAgentSpeed(0.2f);

        }else if (Distance < StartRemainingDistance)
        {
            SetAgentSpeed(-0.2f);
            if (GetAgentSpeed() < 3)
            {
                SetAgentSpeed(4f);
            }
        }

        
        //check distance between player and monster
        //if player slow down -> monster slow down
        //if monster stop -> speed up and reach on player
    }

    //on collision enter - > kill player
}

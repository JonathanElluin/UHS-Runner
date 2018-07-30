using UnityEngine;
using System.Collections;

public class MonsterIA : IA{
    Transform Target;
    float StartDistance = 0;
    float MaxSpeed;
    float MinSpeed;
    bool IsSlowing = false;
    bool IsSpeeding = false;
    bool CanReachTarget = false;

    public float SprintTime;

    IEnumerator CurrentCoroutine = null;
    // Use this for initialization
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        StartDistance = Vector3.Distance(Target.position, transform.position);
        MinSpeed = 4;
        MaxSpeed = 9;
        Init();
    }

    protected override void Init()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnNavmesh()) SetDestination(Target);
        if (!Target) return;
        if (CanReachTarget) return;
        float CurrentDistance = 0;

        CurrentDistance = Vector3.Distance(Target.position, transform.position);

        CheckDestinationReached(CurrentDistance);
        if (CurrentDistance <= 3) return;
        //check distance between player and monster
        //player moves away -> monster accelerate
        if (CurrentDistance > StartDistance && !IsSpeeding)
        {
            if (CurrentCoroutine != null)StopCoroutine(CurrentCoroutine);
            CurrentCoroutine = Accelerate();
            StartCoroutine(CurrentCoroutine);
            IsSpeeding = true;
            IsSlowing = !IsSpeeding;
        }
        //player slow down -> monster slow down
        else if (CurrentDistance < StartDistance && !IsSlowing)
        {
            if (CurrentCoroutine != null)StopCoroutine(CurrentCoroutine);
            CurrentCoroutine = SlowDown();
            StartCoroutine(CurrentCoroutine);
            IsSpeeding = false;
            IsSlowing = !IsSpeeding;
        }
    }

    IEnumerator Accelerate()
    {
        while (GetAgentSpeed() < MaxSpeed)
        {
            yield return new WaitForSeconds(0.1f);
            SetAgentSpeed(+0.5f);
        }
    }

    //speed monster < min speed -> smonster sprint
    IEnumerator SlowDown()
    {
        while(GetAgentSpeed() >= MinSpeed)
        {
            yield return new WaitForSeconds(0.1f);
            SetAgentSpeed(-0.5f);
        }

        CurrentCoroutine = Sprint();
        StartCoroutine(CurrentCoroutine);
    }

    IEnumerator Sprint()
    {
        CanReachTarget = true;
        SetAgentSpeed(MaxSpeed - GetAgentSpeed());
        yield return new WaitForSeconds(SprintTime);
        CanReachTarget = false;
    }
        
        
        
        

    //on collision enter - > kill player

    protected override GameObject InstantiateChara()
    {
        GameObject chara = (GameObject)Instantiate(Resources.Load("Monster", typeof(GameObject)), transform.position, Quaternion.identity);
        chara.transform.SetParent(transform);
        StartCoroutine("LaunchAgentSound");
        return chara;
    }

    protected override void AgentCollide()
    {
        SetAgentAnimation("Attack", true);
        PlayAgentAudio(Clips.Length - 1);
        base.AgentCollide();
        StopAllCoroutines();
        
    }

    IEnumerator LaunchAgentSound()
    {
        int Index = Random.Range(0, Clips.Length-2);
        float WaitTime = Random.Range(5, 10);
        yield return new WaitForSeconds(WaitTime);
        PlayAgentAudio(Index);
        StartCoroutine("LaunchAgentSound");
    }
}

using UnityEngine;
using System.Collections;

public class ScoutIA : IA{
    Transform Player;
    bool IsOnSprint;
    public int SprintTime;
    float StartDistance;
    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (OnNavmesh() && DestinationCache)
        {
            SetDestination(DestinationCache);
            DestinationCache = null;
        }
        if (!CurrentTile)
        {
            Destroy(gameObject);
            return;
        }
         

        if (!Player)
        {
            if (GameObject.FindGameObjectWithTag("Player") == true)Player = GameObject.FindGameObjectWithTag("Player").transform;
            if (Player) StartDistance = Vector3.Distance(Player.position, transform.position);
        }
        else
        {
            float CurrentDistance = 0;
            CurrentDistance = Vector3.Distance(Player.position, transform.position);
            if (CurrentDistance < StartDistance && !IsOnSprint)
            {
                StartCoroutine("Sprint");
            }
        }

        if (CanRotate)
        {

            int Dir = Random.Range(-1,2);
            if (Dir == 0) Dir = 1;
            RotateAgent(Dir);
        }

        if (CanJump)
        {
            JumpAgent(1);
        }

    }

    IEnumerator Sprint()
    {
        IsOnSprint = true;
        float OldSpeed = GetAgentSpeed();
        SetAgentSpeed(+5);
        yield return new WaitForSeconds(SprintTime);
        SetAgentSpeed(-5);
        IsOnSprint = false;
    }

    protected override void RotateAgent(int _dir)
    {
        base.RotateAgent(_dir);
        Transform nextDestination = CurrentTile.GetDestination(_dir);
        if (!nextDestination)
        {
            RotateAgent(_dir * -1);
        }
    }

    protected override GameObject InstantiateChara()
    {
        string Name = GameManager.GameMngr.ScoutName;
        GameObject chara = (GameObject)Instantiate(Resources.Load(Name, typeof(GameObject)), transform.position, Quaternion.identity);
        chara.transform.SetParent(transform);
        Clips = new AudioClip[2];
        for (int i =1; i<= Clips.Length; i++)
        {
            Clips[i-1] = (AudioClip)Resources.Load(Name +"Voix" +i, typeof(AudioClip));
        }
        StartCoroutine("LaunchAgentSound");
        return chara;
    }

    IEnumerator LaunchAgentSound()
    {
        int Index = Random.Range(0, Clips.Length);
        float WaitTime = Random.Range(10, 30);
        yield return new WaitForSeconds(WaitTime);
        PlayAgentAudio(Index);
        StartCoroutine("LaunchAgentSound");
    }

    protected override void Fall()
    {
        base.Fall();
        SetAgentAnimation("IsFall", false);
    }
}

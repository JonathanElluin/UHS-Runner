using UnityEngine;
using System.Collections;

public class ScoutIA : IA{

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!CurrentTile)
        {
            Destroy(gameObject);
            return;
        }
        if (CanRotate)
        {

            int Dir = 1;

            RotateAgent(Dir);
        }

        if (CanJump)
        {
            JumpAgent(1);
        }
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
}

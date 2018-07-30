using UnityEngine;
using System.Collections;

public class JumpTile : StraightTile
{
    Transform JumpPoint;
    // Use this for initialization
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        JumpPoint = transform.Find("JumpPoint");

    }

    protected override Transform GetDestination()
    {
        return JumpPoint;
    }

    public override Transform GetDestination(int _dir)
    {
        if (_dir == 0)
        {
            return null;
        }
        else return base.GetDestination();
    }


    protected override void IAEnter(Collider _col)
    {
        base.IAEnter(_col);
        _col.GetComponent<IA>().CanJumpAgent(true);
    }
}

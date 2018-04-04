using UnityEngine;
using System.Collections;

public class TurnTile : EnvElement
{
    Transform TurnPoint;
    int DirTurn = 0;
    // Use this for initialization
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        TurnPoint = transform.Find("TurnPoint");
        //check fork or corner
        //dir turn value 
        //0 : two exits
        //1 : right
        //-1 : left
        if (GetExits().Count < 2)
        {
            //check if is right or left
            DirTurn = (TurnPoint.localRotation.y > 0)? 1 : -1;
        }
        
    }

    //First destination : turning point
    protected override Transform GetDestination()
    {
        return TurnPoint;
    }

    public override Transform GetDestination(int _dir)
    {
        if (_dir == 0) return null;

        if (DirTurn != 0)
        {
            if (_dir == DirTurn) return GetExits()[0];
             else return null; 
        }
        else return (_dir > 0)? GetExits()[1] : GetExits()[0];
    }

    protected override void IAEnter(Collider _col)
    {
        base.IAEnter(_col);
        _col.GetComponent<IA>().CanRotateAgent(true);
    }

}

using UnityEngine;
using System.Collections;

public class TurnTile : EnvElement
{
    protected Transform TurnPoint;
    protected int DirTurn = 0;
    protected int PathToRemove = -1;
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
        if (Exits.Count < 2)
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
        if (Exits.Count < 2) return (_dir == DirTurn) ? Exits[0] : null;
        else if (_dir > 0)
        {
            PathToRemove = 0;
            return Exits[1];
        }
        else
        {
            PathToRemove = 1;
            return Exits[0];
        }
    }

    protected override void IAEnter(Collider _col)
    {
        base.IAEnter(_col);
        _col.GetComponent<IA>().CanRotateAgent(true);
    }

    protected override void OnMonsterExit()
    {
        if (PathToRemove > -1)
        {
            EnvManager.EnvMngr.RemovePath(PathToRemove);
            
        }
        base.OnMonsterExit();
    }



}

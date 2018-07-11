using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventTile : TurnTile
{
    public GameObject EventPrefab;
    GameObject EventObject; 
    List<Transform> EventSpawnPoints  = new List<Transform>();
    int EventExit;
    // Use this for initialization
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        foreach (Transform child in transform)
        {
            if (child.tag == "SpawnEvent")
            {
                EventSpawnPoints.Add(child);
            }
        }
        EventExit = Random.Range(0, 2);
        EventObject = Instantiate(EventPrefab, EventSpawnPoints[EventExit].position, Quaternion.identity);
        EventObject.transform.SetParent(transform);


    }


    protected override void PlayerEnter(Collider _col)
    {
        //anim 
        EventObject.GetComponent<Animator>().SetBool("StartEvent", true);
        //delete one exit 
        DeleteExit(EventExit);
        
        base.PlayerEnter(_col);
    }
    protected override void IAEnter(Collider _col)
    {
        IA IaInstance = _col.GetComponent<IA>();
        if (_col.tag == "Scout") IaInstance.SetDestination(Exits[EventExit]);
        else
        {
            IaInstance.SetDestination(GetDestination());
            _col.GetComponent<IA>().CanRotateAgent(true);
        }
        IaInstance.SetCurrentTile(this);
    }

    void DeleteExit(int _index)
    {
        Exits.RemoveAt(_index);
        PathToRemove = _index;
        DirTurn = (Exits[0].localRotation.y > 0) ? 1 : -1;
    }
}

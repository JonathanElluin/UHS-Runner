using UnityEngine;
using System.Collections;

public class StraightTile : EnvElement
{

    // Use this for initialization
    void Start()
    {
        Init();
    }

    //1 exit , first exit
    protected override Transform GetDestination()
    {
        return Exits[0];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvElement : MonoBehaviour {
	public int pathNumber = 0;
	List<Transform> Exits = new List<Transform>();
	EnvManager EnvMngr;
	PlayerIA player; 

	// Use this for initialization
	//get Exit of tile 
	//1 : straight tile or 1 corner
	//2 :fork
	protected virtual void Init () {
		foreach (Transform child in transform)
		{
			if (child.tag == "Exit")
			{
				Exits.Add(child);
			}
		}
		EnvMngr = EnvManager.EnvMngr;
		EnvMngr.AddTile(pathNumber, gameObject);
	}


	//Create next tile
	public void CreateElement()
	{
		int _path = pathNumber;

		
		foreach (Transform exit in Exits)
		{
			GameObject tile = Instantiate(EnvMngr.TilesPrefab[EnvMngr.GetRandomTile()], exit.position, exit.rotation);
			tile.GetComponent<EnvElement>().pathNumber = _path;
			_path++;
		}
		
	}

	//Get next destination
	
	//default is first exist
	protected virtual Transform GetDestination()
	{
		return null;
	}

	//get next destination if corner tile or fork (_turn direction of IA)
	public virtual Transform GetDestination(int _dir)
	{
		return null;
	}

	//return tile exits
	protected List<Transform> GetExits()
	{
		return Exits;
	}

	void PlayerEnter(Collider _col)
	{
		EnvMngr.SetPlayerPath(pathNumber);
		player = _col.GetComponent<PlayerIA>();
	}

	protected virtual void IAEnter(Collider _col)
	{
		IA IaInstance = _col.GetComponent<IA>();
		IaInstance.SetDestination(GetDestination());
		IaInstance.SetCurrentTile(this);
	}

	protected PlayerIA GetPlayer()
	{
		return player;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			PlayerEnter(col);
			IAEnter(col);
		}
		if(col.tag == "Scout")
		{
			IAEnter(col);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Monster")
		{
			Destroy(gameObject, 2.0f);
		}
		if (col.tag == "Player")
		{
			EnvMngr.RemoveTile(pathNumber, gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvElement : MonoBehaviour {
	public int pathNumber = 0;
	public int newPathNumber = -1;
	protected List<Transform> Exits = new List<Transform>();
	protected EnvManager EnvMngr;
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
			if (EnvMngr.PathRemove == pathNumber) return;
			GameObject tile = Instantiate(EnvMngr.TilesPrefab[EnvMngr.GetRandomTile()], exit.position, exit.rotation);
			newPathNumber = (_path > pathNumber) ? EnvMngr.GetLastPath() + 1 : _path;
			tile.GetComponent<EnvElement>().pathNumber = newPathNumber;
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


	protected virtual void PlayerEnter(Collider _col)
	{
		EnvMngr.SetPlayerPath(pathNumber);
		IAEnter(_col);
		//Debug.Log(GetDestination().name + GetDestination().parent.name);
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

	protected virtual void OnMonsterExit()
	{
		Destroy(gameObject, 1);
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			PlayerEnter(col);
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
			OnMonsterExit();
		}
		if (col.tag == "Player")
		{
			EnvMngr.RemoveTile(pathNumber, gameObject);
		}
	}
}

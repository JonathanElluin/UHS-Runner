using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour {
	public GameObject[] TilesPrefab;
	public static EnvManager EnvMngr;

	List<List<GameObject>> Paths = new List<List<GameObject>>();
	public float cornerPercent;
	public float forkPercent;
	public int PlayerPath = 0;
	int TilesNumberNeed = 5;
	int EndPathTileCreate = 0;

	bool EnvIsInit = false;
	// Use this for initialization
	void Awake() {
		EnvMngr = this;
	}
	void Start () {
		//Create first path
		List<GameObject> firstPath = new List<GameObject>();
		Paths.Add(firstPath);
		//Create first tile
		GameObject tile = Instantiate(TilesPrefab[0], new Vector3(0, 0, 0), Quaternion.identity);
		
	}
	
	//Add tile reference to right path
	public void AddTile(int _path , GameObject _tile)
	{
		if (_path > Paths.Count - 1)
		{
			//Create new path
			List<GameObject> newPath = new List<GameObject>();
			Paths.Add(newPath);
		}
		Paths[_path].Add(_tile);
		EndPathTileCreate += 1;
		CheckNumberTileByPath();
	}

	void CheckNumberTileByPath()
	{
		if (TilesNumberNeed > 0)
		{
			if (EndPathTileCreate == Paths.Count)
			{
				TilesNumberNeed--;
				EndPathTileCreate = 0;
				foreach (List<GameObject> path in Paths)
				{
					if (path.Count > 0)path[path.Count - 1].GetComponent<EnvElement>().CreateElement();
				}
			}
		}
		else
		{
			if (!EnvIsInit)
			{
				GameManager.GameMngr.EnvIsInit();
				EnvIsInit = true;
			}
		}
	}

	public void RemoveTile(int _path , GameObject _tile)
	{
		Paths[_path].Remove(_tile);
		TilesNumberNeed++;
		CheckNumberTileByPath();
	}
 

	public void SetPlayerPath(int _playerPath)
	{
		if (PlayerPath != _playerPath)
		{
			/*int oldPath = PlayerPath;
			PlayerPath = _playerPath;*/
			//path has change
			//Remove each tile of old path
			foreach(GameObject tile in Paths[PlayerPath])
			{
				Destroy(tile);
			}
			//Remove old path
			Paths.RemoveAt(PlayerPath);

			//Reset the path number of each tile
			for (int i = PlayerPath; i < Paths.Count; i++)
			{
				foreach (GameObject tile in Paths[i])
				{
					tile.GetComponent<EnvElement>().pathNumber = i;
				}
			}
		}
	}

	public int GetRandomTile()
	{

		float _value = Random.value;
		if (_value <= forkPercent)
		{
			forkPercent += (forkPercent < 0.1f)? +0.1f : -0.1f;
			return TilesPrefab.Length-1;
			
		}

		else if (_value <= cornerPercent)
		{
			cornerPercent += (cornerPercent > 0.0f) ? -0.1f : 0.5f;
			return Random.Range(2, TilesPrefab.Length - 1);
		}
		else
		{
			return Random.Range(0, 2);
		}
	}

	public Transform GetTile(int _tile)
	{
		return Paths[PlayerPath][_tile].transform; 
	}
}

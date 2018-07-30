using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : MonoBehaviour {
    public GameObject[] TilesPrefab;
    public static EnvManager EnvMngr; 

    List<List<GameObject>> Paths = new List<List<GameObject>>();
    
    public int PlayerPath = 0;
    public int TilesByPath = 5;
    int TilesNumberNeed = 5;
    int EndPathTileCreate = 0;
    public int PathRemove = -1;
    int StraightTileNb = 0;
    public float TimeBeforeEvent = 0;
    bool EventActive = false;
    bool EnvIsInit = false;
    // Use this for initialization
    void Awake() {
        EnvMngr = this;
    }

    void Start () {
        TilesNumberNeed = TilesByPath;
        
    }
    public int GetLastPath()
    {
        return Paths.Count - 1;
    }
    
    //Add tile reference to right path
    public void AddTile(int _path , GameObject _tile)
    {
        if (PathRemove == _path)
        {
            Destroy(_tile);
            return;
        }

        if (_path > Paths.Count - 1)
        {
            //Create new path
            List<GameObject> newPath = new List<GameObject>();
            Paths.Add(newPath);
        }
        Paths[_path].Add(_tile);

        if (!EnvIsInit)
        {
            EndPathTileCreate += 1;
            InitPaths();
        }
    }

    //check number of tile 
    void InitPaths()
    {
        if (TilesNumberNeed > 0)
        {
            //create new tiles when all path add old tile
            if (EndPathTileCreate == Paths.Count)
            {
                TilesNumberNeed--;
                EndPathTileCreate = 0;
                CreateEndPathTiles();
            }
        }
        else if (!EnvIsInit)
        { 
            GameManager.GameMngr.EnvIsInit();
            EnvIsInit = true;
        }
    }

    //create new tile at the end of each path
    void CreateEndPathTiles()
    {
        foreach (List<GameObject> path in Paths)
        {
            if (path.Count > 0 && PathRemove!=Paths.IndexOf(path)) path[path.Count - 1].GetComponent<EnvElement>().CreateElement();
        }
        StraightTileNb += 1;
    }

    public void RemoveTile(int _path , GameObject _tile)
    {
        Paths[_path].Remove(_tile);
        CreateEndPathTiles();
    }
 

    public void SetPlayerPath(int _playerPath)
    {
        PlayerPath = _playerPath;
    }

    public void RemovePath(int _Path)
    {
        
        //Remove each tile of old path
        foreach (GameObject tile in Paths[_Path])
        {
            Destroy(tile);
        }
        //Remove old path
        Paths.RemoveAt(_Path);
        PathRemove = -1;
        //Reset the path number of each tile
        for (int i = _Path; i < Paths.Count; i++)
        {
            foreach (GameObject tile in Paths[i])
            {
                tile.GetComponent<EnvElement>().pathNumber = i;
            }
        }
    }

    IEnumerator EventActivate()
    {
        yield return new WaitForSeconds(TimeBeforeEvent);
        EventActive = true;
    }

    public int GetRandomTile()
    {
        float forkPercent = 0;
        float cornerPercent = 0;

        
        if (StraightTileNb > TilesByPath)
        {
            forkPercent = 0.5f;
            cornerPercent = 0.8f;
        }

        float _value = Random.value;
        if (_value <= forkPercent)
        {
            StraightTileNb = 0;
            if (EventActive) return Random.Range(TilesPrefab.Length - 2, TilesPrefab.Length);
            else return TilesPrefab.Length - 2;


        }

        else if (_value <= cornerPercent)
        {
            StraightTileNb = 0;
            return Random.Range(2, TilesPrefab.Length - 2);
        }
        else
        {
            return Random.Range(0, 2);
        }
    }
}

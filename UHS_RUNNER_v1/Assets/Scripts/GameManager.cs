using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public GameObject[] IAPrefab;
    public float TimeBtweenSpawn;
    public Transform SpawnPoint;
    public static GameManager GameMngr; 
    // Use this for initialization*
    private void Awake()
    {
        GameMngr = this;
    }

    public void EnvIsInit()
    {
        StartCoroutine("SpawnIA");
    }

    IEnumerator SpawnIA()
    {
        foreach (GameObject Ia in IAPrefab)
        {
            Instantiate(Ia, SpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(TimeBtweenSpawn);
        }
        Destroy(SpawnPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

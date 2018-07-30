using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string ScoutName = "Anna";
    public string CharaName = "Vildrac";
    public GameObject DeadCam;

    public GameObject[] IAPrefab;
    public float TimeBtweenSpawn;
    public Transform SpawnPoint;

    public static GameManager GameMngr; 

   
    // Use this for initialization
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
        yield return new WaitForSeconds(1);
        foreach (GameObject Ia in IAPrefab)
        {
            Instantiate(Ia, SpawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(TimeBtweenSpawn);
        }
        SpawnPoint.gameObject.SetActive(false);
    }


    public void PlayerDead()
    {
        DeadCam.transform.position = Camera.main.transform.position;
        DeadCam.transform.rotation = Camera.main.transform.rotation;
        Destroy(GameObject.FindGameObjectWithTag("Scout"));
        Destroy(GameObject.FindGameObjectWithTag("Monster"));
        Camera.main.enabled = false;
        StartCoroutine("ShowDead");
        
    }

    IEnumerator ShowDead()
    {
        yield return new WaitForSeconds(4);
        ResetScene();
    }

    void ResetScene()
    {
        //Facile
        SceneManager.LoadScene(0);

        //Difficile
        //Destroy element


        //Reset element
        //SpawnPoint.gameObject.SetActive(false);

        //Init element

    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text CountdownText;
    int Countdown = 3;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("CountDown");
    }

    IEnumerator CountDown()
    {
        
        while (Countdown >= 1)
        {
            CountdownText.text = Countdown.ToString();
            yield return new WaitForSeconds(1);
            Countdown--;
        }

        CountdownText.enabled = false;

    }
    // Update is called once per frame
    void Update()
    {

    }
}

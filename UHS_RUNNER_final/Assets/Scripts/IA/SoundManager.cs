using UnityEngine;
using System.Collections;


public class SoundManager : MonoBehaviour
{
    public AudioClip[] Clips;
    public float ClipToPlay = -1;

    AudioSource SoundSource;
    // Use this for initialization
    void Start()
    {
        SoundSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if(ClipToPlay > -1)
        {
            SoundSource.PlayOneShot(Clips[(int)ClipToPlay]);
        }
      
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;

public class RadioController : MonoBehaviour
{
    public AudioSource radioAudioSource;

    public void PlayNextSong()
    {
        radioAudioSource.Play();
        Debug.Log("PlayNextSong was called");
    }



// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

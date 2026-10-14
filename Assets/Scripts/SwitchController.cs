using UnityEngine;

public class SwitchController : MonoBehaviour
{
    
    
    public bool switchActive;

    
    void Start()
    {

    }

    
    void Update()
    {

    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Steve is at the switch");
            switchActive = true;
        }
    }
}
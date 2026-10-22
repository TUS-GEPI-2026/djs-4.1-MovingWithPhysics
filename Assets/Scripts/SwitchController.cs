using UnityEngine;
using UnityEngine.InputSystem;
public class SwitchController : MonoBehaviour
{

    public RadioController radioController;



    public bool switchActive;
    public SpriteRenderer aSR;

    void Start()
    {

    }


    

    void Update()
    {
        CheckSwitchKeys();
    }

    void CheckSwitchKeys()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame == true && switchActive == true)
        {
            aSR.color = Color.green;
            radioController.PlayNextSong();
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame == true && switchActive == true)
        {
            aSR.color = Color.white;
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Steve is at the switch");
            switchActive = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Steve left the switch");
            switchActive = false;
        }
    }
}
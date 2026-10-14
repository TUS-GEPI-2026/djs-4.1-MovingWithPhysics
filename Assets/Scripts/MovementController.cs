using UnityEngine;
using UnityEngine.InputSystem;


public class movementController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float speed;
    public float jumpForce;






    void Update()
    {
        MoveLeftAndRight();
        Jump();
    }
    void MoveLeftAndRight()
    {



        if (Keyboard.current.rightArrowKey.isPressed)
        {
            theRB.linearVelocity = new Vector2(speed, theRB.linearVelocity.y);
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            theRB.linearVelocity = new Vector2(-speed, theRB.linearVelocity.y);
        }
        
        }
    

        void Jump()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            theRB.AddForce(Vector2.up * jumpForce);
        }
    }
}

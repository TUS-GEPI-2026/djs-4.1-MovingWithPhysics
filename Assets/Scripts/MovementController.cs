using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class movementController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public SpriteRenderer aSR;
    public float speed;
    public float jumpForce;
    public float sprintSpeed = 10;

    Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }



    void Update()
    {
        MoveLeftAndRight();
        Jump();
        CheckIfFallen();
    }
    void MoveLeftAndRight()
    {

        float currentSpeed = speed;
        if (Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = sprintSpeed;
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            theRB.linearVelocity = new Vector2(currentSpeed, theRB.linearVelocity.y);
            aSR.flipX = false;
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            theRB.linearVelocity = new Vector2(-currentSpeed, theRB.linearVelocity.y);
            aSR.flipX = true;
        }

    }

    bool _isGrounded;
    void Jump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (_isGrounded)
                theRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump! Steve's velocity is " + theRB.linearVelocity);
        }
    }



void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Steve touched " + collision.gameObject.name);
    _isGrounded = true;
}
void OnCollisionExit2D(Collision2D collision)
{
    Debug.Log("Steve stopped touching " + collision.gameObject.name);
    _isGrounded = false;
}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin") == true)
        {
            Debug.Log("Coin collected!");
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Spike") == true)
        {
            transform.position = _startPosition;
            theRB.linearVelocity = Vector2.zero;
        }
    }




    void CheckIfFallen()
    {
        if (transform.position.y < -10)
        {
            transform.position = _startPosition;
            theRB.linearVelocity = Vector2.zero;
        }
    }
}
    

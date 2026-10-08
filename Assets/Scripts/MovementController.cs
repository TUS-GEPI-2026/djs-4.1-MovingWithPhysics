// LECTURER SOLUTION: 4.1 Moving With Physics, extension activities (ExtensionActivities.html).
//
// This is the lab's MovementController with every code-based extension activity (5 to 12) added. Each change
// is marked with the activity number, e.g. "Extension 6". Activities 1 to 4 are Editor-only, so they have no code.
//
// Setup in the Editor for this version:
//  - Steve: drag his Rigidbody2D into "The RB" and his SpriteRenderer into "Player Sprite Renderer".
//  - Extension 10 changes jumpForce from ~200 (Force) to ~7 (Impulse). If Steve already has the component,
//    the Inspector keeps its old value (e.g. 200), so set Jump Force to 7 by hand, or he'll launch off screen.
//  - Extension 12 needs two Tags, "Coin" and "Spike", to exist (Tag drop-down > Add Tag...). CompareTag gives an
//    error if the tag hasn't been created, even if there are no spikes in the scene yet.
//  - Coins and spikes need a Collider2D with "Is Trigger" ticked. They don't need a script.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    // ----- Public fields (set in the Inspector) -----

    public Rigidbody2D theRB;

    // Extension 7: the SpriteRenderer we flip to make Steve face the way he's moving.
    public SpriteRenderer playerSpriteRenderer;

    public float speed = 5;

    // Extension 6: the speed used while Left Shift is held down.
    public float sprintSpeed = 10;

    // Extension 10: with ForceMode2D.Impulse, 7 gives a jump of roughly 2.5 units (the lab's Force mode used 200).
    public float jumpForce = 7;

    // Extension 11 (stretch goal): how many jumps Steve can do before he has to land again. 2 = double jump.
    public int maxJumps = 2;

    // Extension 8: if Steve falls below this height, he's sent back to the start.
    public float fallLimit = -10;

    // ----- Private fields (not in the Inspector, so they start with _) -----

    // Extension 8: where Steve was when the game started.
    Vector3 _startPosition;

    // Extension 11 (stretch goal): how many times Steve has jumped since he last landed.
    int _jumpCount;

    // Extension 12 (stretch goal): how many coins Steve has collected.
    int _coinCount;

    // Start is called before the first frame update
    void Start()
    {
        // Extension 8: remember where Steve started, so we can send him back there later.
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeftAndRight();
        Jump();
        CheckIfFallen(); // Extension 8
    }

    void MoveLeftAndRight()
    {
        // Extension 6: work out which speed to use this frame, and store it in a local variable.
        float currentSpeed = speed;
        if (Keyboard.current.leftShiftKey.isPressed == true)
        {
            currentSpeed = sprintSpeed;
        }

        if (Keyboard.current.rightArrowKey.isPressed == true)
        {
            // Extension 5: the lab used Vector2.right * speed, which has a y of 0. That set Steve's up/down speed
            // to 0 every frame, which cancelled jumps and slowed his falling. Now we keep his current y speed.
            theRB.linearVelocity = new Vector2(currentSpeed, theRB.linearVelocity.y);

            // Extension 7: face right (this assumes the sprite is drawn facing right).
            playerSpriteRenderer.flipX = false;
        }
        else if (Keyboard.current.leftArrowKey.isPressed == true)
        {
            // Extension 5: moving left means a negative x speed.
            theRB.linearVelocity = new Vector2(-currentSpeed, theRB.linearVelocity.y);

            // Extension 7: face left.
            playerSpriteRenderer.flipX = true;
        }
        else
        {
            // Stop moving sideways, but keep falling.
            // (This line is also why Steve doesn't slide on the Ice material in Extension 2.)
            theRB.linearVelocity = new Vector2(0, theRB.linearVelocity.y);
        }
    }

    void Jump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame == true)
        {
            // Extension 11: only jump if Steve hasn't used up his jumps.
            // (The first part of Extension 11, before the double jump stretch goal, used a bool instead:
            //  set _isGrounded to true/false in OnCollisionEnter2D/Exit2D, and only jump if it's true.)
            if (_jumpCount < maxJumps)
            {
                // Extension 11: for a double jump, clear Steve's up/down speed first. Otherwise, if he's already
                // falling, the second jump only slows his fall instead of pushing him up.
                theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, 0);

                // Extension 10: an impulse is one big push, all at once (like a kick), instead of a force
                // applied for one physics step. So jumpForce is about 50 times smaller than in the lab.
                theRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                _jumpCount = _jumpCount + 1;

                // Extensions 9 and 10: an impulse changes the velocity straight away, so this shows the jump
                // speed. It is jumpForce divided by Mass (7 / 1 = 7).
                Debug.Log("Jump! Steve's velocity is " + theRB.linearVelocity);
            }
        }
    }

    // Extension 8: send Steve back to the start if he falls off the level.
    void CheckIfFallen()
    {
        if (transform.position.y < fallLimit)
        {
            Debug.Log("Steve fell off the world!");
            ResetToStart();
        }
    }

    // Extensions 8 and 12: move Steve back to where he started, and stop him moving.
    // It's a method because both falling and spikes need it.
    void ResetToStart()
    {
        transform.position = _startPosition;
        theRB.linearVelocity = Vector2.zero;
    }

    // Extension 11: Unity calls this when Steve's collider starts touching another (non-trigger) collider.
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Steve touched " + collision.gameObject.name);

        // Steve has landed, so he can jump again.
        // Note: touching a wall counts too, so Steve can climb a wall by jumping against it. That's the answer
        // to "Can Steve now jump off the walls? Why?". A fix (checking which way the collision is facing)
        // is beyond this week.
        _jumpCount = 0;
    }

    // Extension 12: Unity calls this when Steve's collider enters a collider that has "Is Trigger" ticked.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin") == true)
        {
            // Remove the coin that Steve touched (not Steve himself!).
            Destroy(other.gameObject);

            // Extension 12 (stretch goal): count the coins.
            _coinCount = _coinCount + 1;
            Debug.Log("Coin collected! Total coins: " + _coinCount);
        }
        else if (other.gameObject.CompareTag("Spike") == true)
        {
            Debug.Log("Ouch! Steve hit a spike.");
            ResetToStart();
        }
    }
}

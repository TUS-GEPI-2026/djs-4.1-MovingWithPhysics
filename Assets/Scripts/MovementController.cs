// "using" lines tell C# which libraries (collections of ready-made code) this script needs.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;             // Unity's main library: MonoBehaviour, Rigidbody2D, Vector2, etc.
using UnityEngine.InputSystem; // Unity's (new) Input System, which lets us read the keyboard

// A class is a blueprint for a script. The class name must match the file name (MovementController.cs).
// ": MonoBehaviour" means this script can be attached to a GameObject as a component.
public class MovementController : MonoBehaviour
{
    // ----- Fields -----
    // A field is a variable that belongs to the whole class, so every method in the class can use it.
    // "public" fields show up in the Inspector, so you can set them in the Unity Editor.

    // The Rigidbody2D component we want to move. Drag the player's Rigidbody2D into this slot in the Inspector.
    public Rigidbody2D theRB;

    // How fast the player moves left and right. A "float" is a number that can have a decimal point (e.g. 5.5).
    public float speed;

    // How hard the player is pushed upwards when they jump. It starts at 70 but can be changed in the Inspector.
    public float jumpForce = 70;

    // Start is called once, just before the first frame of the game.
    // We don't need to do anything here yet, so it's empty.
    void Start() { }

    // Update is called once every frame (many times per second).
    // We check for key presses here, so the game notices them straight away.
    void Update()
    {
        // Call (run) our two methods below, once per frame.
        MoveLeftAndRight();
        Jump();
    }

    // A method is a named block of code that does one job.
    // This one moves the player left or right depending on which arrow key is held down.
    void MoveLeftAndRight()
    {
        // "isPressed" is true for every frame that the key is being held down.
        if (Keyboard.current.rightArrowKey.isPressed == true)
        {
            // Vector2.right is the direction (1, 0). Multiplying it by speed gives a velocity of (speed, 0),
            // so the player moves to the right.
            theRB.linearVelocity = Vector2.right * speed;
        }
        // "else if" is only checked when the "if" above was false (the right arrow is NOT held down).
        else if (Keyboard.current.leftArrowKey.isPressed == true)
        {
            // Vector2.left is the direction (-1, 0), so this moves the player to the left.
            theRB.linearVelocity = Vector2.left * speed;
        }
        // "else" runs when neither arrow key is held down.
        else
        {
            // Stop moving sideways (x = 0), but keep the current up/down speed (y) so the player can still fall.
            theRB.linearVelocity = new Vector2(0, theRB.linearVelocity.y);
        }
    }

    // This method makes the player jump when the space bar is pressed.
    void Jump()
    {
        // "wasPressedThisFrame" is only true on the single frame when the key first goes down,
        // so holding the space bar doesn't make the player keep jumping.
        if (Keyboard.current.spaceKey.wasPressedThisFrame == true)
        {
            // Vector2.up is the direction (0, 1). AddForce pushes the Rigidbody2D upwards,
            // and the physics engine (gravity) brings it back down.
            theRB.AddForce(Vector2.up * jumpForce);
        }
    }
}

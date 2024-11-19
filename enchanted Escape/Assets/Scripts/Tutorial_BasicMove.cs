using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_BasicMove : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 50;
    private Rigidbody2D rb;
    private float direction;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // See Input section in Edit->Project Setting. That is where the key bindings are
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // get hoziontal movement. W or Left arrow is -1, D or Right arrow is 1
        direction = Input.GetAxisRaw("Horizontal");

        float newMovePos = direction * speed;

        // edit velocity in the X vector but not in the y.
        // There are better ways to this but this is easier to understand
        rb.velocity = new Vector2(newMovePos, rb.velocity.y);
    }
}

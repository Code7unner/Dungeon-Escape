using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 2.5f;

    private PlayerAnimation _anim;

    // Start is called before the first frame update
    void Start()
    {
        // assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement() 
    {
        // horizontal input for left/right
        float move = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true) 
        {
            Debug.Log("Jump!");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpNeededRoutine());
        }        

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _anim.Move(move);
    }

    bool IsGrounded() 
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);

        if (hitInfo.collider != null) 
        {
            if (_resetJump == false)
                return true;
        }

        return false;
    }

    IEnumerator ResetJumpNeededRoutine() 
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
}

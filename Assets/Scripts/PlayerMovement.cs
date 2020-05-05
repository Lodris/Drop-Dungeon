using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
#pragma warning disable 0649
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private float _runSpeed = 40f;
#pragma warning restore 0649

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private float _horizontalMove = 0f;
    private bool _isFacingRight = true;
    private Vector3 _velocity = Vector3.zero;

    public void Init() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        Move();
    }

    public void Right() {
        _horizontalMove = _runSpeed;
    }

    public void Left() {
        _horizontalMove = -_runSpeed;
    }

    private void Move() {
        float xVelocity = _horizontalMove * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(xVelocity * 10f, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

        if (xVelocity > 0 && !_isFacingRight) {
            Flip();
        } else if (xVelocity < 0 && _isFacingRight) {
            Flip();
        }
    }

    private void Flip() {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
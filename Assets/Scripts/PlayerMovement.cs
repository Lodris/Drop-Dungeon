using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool IsFacingRight = true;

#pragma warning disable 0649
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private float _runSpeed = 40f;
#pragma warning restore 0649

    private Collider2D _collider;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private float _horizontalMove = 0f;
    private Vector3 _velocity = Vector3.zero;

    public void Init() {
        _collider = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        Move();
    }

    public void Right() {
        if (_horizontalMove == 0) {
            PlayerManager.Instance.Animator.SetBool("isMoving", true);
        }
        _horizontalMove = _runSpeed;
    }

    public void Left() {
        if (_horizontalMove == 0) {
            PlayerManager.Instance.Animator.SetBool("isMoving", true);
        }
        _horizontalMove = -_runSpeed;
    }

    private void Move() {
        float xVelocity = _horizontalMove * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(xVelocity * 10f, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

        if (xVelocity > 0 && !IsFacingRight) {
            Flip();
        } else if (xVelocity < 0 && IsFacingRight) {
            Flip();
        }
    }

    private void Flip() {
        IsFacingRight = !IsFacingRight;
        Vector3 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }

    public void Stop() {
        _horizontalMove = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Walls")) {
            PlayerManager.Instance.Animator.SetBool("isMoving", false);
            Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
    }
}
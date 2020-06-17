using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
#pragma warning disable 0649
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private float _runSpeed = 10f;
#pragma warning restore 0649

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private float _minXPosition;
    private float _maxXPosition;
    private float _horizontalMove = 0;
    private Vector3 _velocity = Vector3.zero;
    private bool _isFacingRight = false;
    private bool _isInited = false;

    public void Init(int minXPosition, int maxXPosition) {
        _minXPosition = transform.position.x - minXPosition;
        _maxXPosition = transform.position.x + maxXPosition;

        if (Random.Range(0, 2) == 0) {
            _horizontalMove = -_runSpeed;
        } else {
            _horizontalMove = _runSpeed;
        }

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _isInited = true;
    }

    private void Update() {
        if (_isInited) {
            if (transform.position.x < _minXPosition) {
                _horizontalMove = _runSpeed;
            } else if (transform.position.x > _maxXPosition) {
                _horizontalMove = -_runSpeed;
            }
        }
    }

    private void FixedUpdate() {
        if (_isInited) {
            Move();
        }
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
        Vector3 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }
}
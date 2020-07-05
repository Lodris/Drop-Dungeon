using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField] private EnemyMovement _enemyMovement;
#pragma warning restore 0649

    private Animator _animator;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    public void Init(int minXPosition, int maxXPosition) {
        _enemyMovement.Init(minXPosition, maxXPosition);
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Hit() {
        _enemyMovement.Stop();
        gameObject.tag = "Dead";
        _animator.SetTrigger("Death");
    }

    public void Death() {
        Destroy(gameObject);
    }
}
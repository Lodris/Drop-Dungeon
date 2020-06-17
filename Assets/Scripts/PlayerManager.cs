using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance { get; private set; }

    [NonSerialized] public Animator _animator;

#pragma warning disable 0649
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAttack _playerAttack;
#pragma warning restore 0649

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }

        _animator = GetComponent<Animator>();
    }

    public void Init() {
        _playerMovement.Init();
        _playerAttack.Init();
    }

    public void OnAttackAnimate() {
        _animator.SetTrigger("Attack");
        _animator.SetBool("isMoving", false);
        _playerMovement.Stop();
    }

    private void OnDestroy() {
        Instance = null;
    }
}
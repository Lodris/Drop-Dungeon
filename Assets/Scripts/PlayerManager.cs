﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance { get; private set; }

    [NonSerialized] public Animator Animator;

#pragma warning disable 0649
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAttack _playerAttack;
    [SerializeField] private PlayerHitbox _playerHitbox;
#pragma warning restore 0649

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }

        Animator = GetComponent<Animator>();
    }

    public void Init() {
        _playerMovement.Init();
        _playerAttack.Init();
        _playerHitbox.Init();
    }

    public void OnAttack() {
        StartCoroutine(_playerHitbox.DisableHitbox());
        Animator.SetTrigger("Attack");
        Animator.SetBool("isMoving", false);
        _playerMovement.Stop();
    }

    public void EnableAttackCollider() {
        _playerAttack.EnableAttackCollider();
    }

    public void DisableAttackCollider() {
        _playerAttack.DisableAttackCollider();
    }

    public void Hit() {
        StartCoroutine(_playerHitbox.DisableHitbox());
        Animator.SetTrigger("Hit");
        Animator.SetBool("isMoving", false);
        _playerMovement.Stop();
    }

    private void OnDestroy() {
        Instance = null;
    }
}
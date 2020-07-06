using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance { get; private set; }

    [NonSerialized] public Animator Animator;
    [NonSerialized] public float EnvironmentMovementSpeedModifier = 2f;
    [NonSerialized] public bool IsAttacking = false;

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

    private void Update() {
        if (transform.position.y > TilemapManager.Instance.transform.position.y + 7f) {
            Destroy(gameObject);
            Time.timeScale = 0f;
            SceneManager.LoadSceneAsync("Death", LoadSceneMode.Additive);
        }
    }

    public void OnAttack() {
        TilemapManager.Instance.PlatformMovementSpeed /= EnvironmentMovementSpeedModifier;
        IsAttacking = true;
        _playerHitbox.DisableHitbox();
        Animator.SetTrigger("Attack");
    }

    public void EnableAttackCollider() {
        _playerAttack.EnableAttackCollider();
    }

    public void DisableAttackCollider() {
        _playerAttack.DisableAttackCollider();
    }

    public void ReturnToNormalSpeed() {
        TilemapManager.Instance.PlatformMovementSpeed *= EnvironmentMovementSpeedModifier;
        IsAttacking = false;
        _playerHitbox.EnableHitbox();
    }

    public void Hit() {
        IsAttacking = false;
        StartCoroutine(_playerHitbox.Blink());
        Animator.SetTrigger("Hit");
        Animator.SetBool("isMoving", false);
        _playerMovement.Stop();
    }

    private void OnDestroy() {
        Instance = null;
    }
}
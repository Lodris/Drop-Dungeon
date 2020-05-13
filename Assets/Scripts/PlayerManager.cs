using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager Instance { get; private set; }

#pragma warning disable 0649
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAttack _playerAttack;
#pragma warning restore 0649

    [HideInInspector] public Animator _animator;

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

    private void OnDestroy() {
        Instance = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance { get; private set; }

#pragma warning disable 0649
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAttack _playerAttack;
#pragma warning restore 0649

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    public void Init() {
        _playerMovement.Init();
        _playerAttack.Init();
    }

    private void OnDestroy() {
        instance = null;
    }
}
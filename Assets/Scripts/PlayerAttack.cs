using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField] private Collider2D _collider;
#pragma warning restore 0649

    public void Init() {

    }

    public void Attack() {
        PlayerManager.Instance.OnAttack();
        TilemapManager.Instance.PlatformMovementSpeed /= PlayerManager.Instance.EnvironmentMovementSpeedModifier;
    }

    public void EnableAttackCollider() {
        _collider.enabled = true;
    }

    public void DisableAttackCollider() {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<EnemyManager>().Hit();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Text _killsText;
#pragma warning restore 0649

    private int _killsCounter = 0;

    public void Init() {

    }

    public void Attack() {
        if (!PlayerManager.Instance.IsAttacking) {
            PlayerManager.Instance.OnAttack();
        }
    }

    public void EnableAttackCollider() {
        _collider.enabled = true;
    }

    public void DisableAttackCollider() {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            _killsCounter++;
            _killsText.text = _killsCounter.ToString();
            collision.GetComponent<EnemyManager>().Hit();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField] private float _blinkTime;
    [SerializeField] private Collider2D _collider;
#pragma warning restore 0649

    public void Init() {

    }

    public IEnumerator DisableHitbox() {
        _collider.enabled = false;
        yield return new WaitForSeconds(_blinkTime);
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            PlayerManager.Instance.Hit();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;

    public void Init() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack() {
        Debug.Log("Attacking");
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField] private bool _isBackground;
#pragma warning restore 0649

    private float _speed = 3f;
    private bool _isMoving = false;

    public void Init() {
        _isMoving = true;
    }

    private void Update() {
        if (_isMoving) {
            if (_isBackground) {
                _speed = TilemapManager.Instance.PlatformMovementSpeed / 2f;
            } else {
                _speed = TilemapManager.Instance.PlatformMovementSpeed * 2f;
            }

            transform.position += new Vector3(0f, _speed, 0f) * Time.deltaTime;
            if (transform.localPosition.y >= 12f) {
                transform.localPosition = new Vector3(transform.position.x, -12f, transform.position.z);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    private bool _isMoving = false;

    private void Start() {
        TilemapManager.instance.AddTile(this);
    }

    public void Init() {
        _isMoving = true;
    }

    private void Update() {
        if (_isMoving) {
            transform.position += new Vector3(0f, 3f, 0f) * Time.deltaTime;
        }
    }
}
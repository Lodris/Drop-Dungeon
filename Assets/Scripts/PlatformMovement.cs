using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {
    private float _speed = 3f;
    private bool _isMoving = false;

    private void Start() {
        TilemapManager.Instance.AddTile(this);
        _speed = TilemapManager.Instance.PlatformMovementSpeed;
    }

    public void Init() {
        _isMoving = true;
    }

    private void Update() {
        if (_isMoving) {
            transform.position += new Vector3(0f, _speed, 0f) * Time.deltaTime;
            if (transform.position.y > TilemapManager.Instance.transform.position.y + 7f) {
                Destroy(gameObject);
            }
        }
    }
}
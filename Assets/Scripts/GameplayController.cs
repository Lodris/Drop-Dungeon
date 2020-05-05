using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {
    public static GameplayController instance { get; private set; }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {
        PlayerManager.instance.Init();
        TilemapManager.instance.Init();
    }

    private void OnDestroy() {
        instance = null;
    }
}
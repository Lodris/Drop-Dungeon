using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour {
    public static GameplayController Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {
        PlayerManager.instance.Init();
        TilemapManager.Instance.Init();
    }

    private void OnDestroy() {
        Instance = null;
    }
}
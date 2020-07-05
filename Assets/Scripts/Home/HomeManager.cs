using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour {
    public static HomeManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    public void Play() {
        SceneManager.LoadScene("Gameplay");
    }

    private void OnDestroy() {
        Instance = null;
    }
}
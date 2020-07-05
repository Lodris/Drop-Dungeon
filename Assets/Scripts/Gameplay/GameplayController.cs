using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {
    public static GameplayController Instance { get; private set; }

#pragma warning disable 0649
    [SerializeField] private Text _metersText;
#pragma warning restore 0649

    private float _metersCounter = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    private void Start() {
        PlayerManager.Instance.Init();
        TilemapManager.Instance.Init();
        EnemiesSpawningManager.Instance.Init();
    }

    private void Update() {
        if (TilemapManager.Instance.IsStarted) {
            _metersText.text = _metersCounter.ToString("0.0");
            _metersCounter += Time.deltaTime;
        }
    }

    public void Pause() {
        Time.timeScale = 0f;
        SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
    }

    private void OnDestroy() {
        Instance = null;
    }
}
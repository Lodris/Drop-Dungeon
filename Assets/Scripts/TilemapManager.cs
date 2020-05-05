using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour {
    public static TilemapManager instance { get; private set; }

#pragma warning disable 0649
    [SerializeField] private PlatformPositioner _platformPositioner;
#pragma warning restore 0649

    private List<PlatformMovement> _platforms;
    private bool _isStarted = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }

        _platforms = new List<PlatformMovement>();
    }

    public void Init() {
        StartCoroutine(StartScrolling());
    }

    public void StartMe() {
        if (!_isStarted) {
            StopCoroutine(nameof(StartScrolling));
            _isStarted = true;
            InitAllTiles();
            _platformPositioner.Init();
        }
    }

    public void AddTile(PlatformMovement platformMovement) {
        _platforms.Add(platformMovement);
        if (_isStarted) {
            platformMovement.Init();
        }
    }

    private void InitAllTiles() {
        foreach (PlatformMovement platformMovement in _platforms) {
            platformMovement.Init();
        }
    }

    private IEnumerator StartScrolling() {
        yield return new WaitForSeconds(1f);
        StartMe();
    }

    private void OnDestroy() {
        instance = null;
    }
}
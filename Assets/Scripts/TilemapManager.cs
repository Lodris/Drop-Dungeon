using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour {
    public static TilemapManager Instance { get; private set; }

    public float PlatformMovementSpeed = 3f;
    public Transform Platforms;

#pragma warning disable 0649
    [SerializeField] private PlatformPositioner _platformPositioner;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _environment;
    [SerializeField] private List<EnvironmentMovement> _environmentElements;
#pragma warning restore 0649

    private List<PlatformMovement> _platforms;
    private bool _isStarted = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }

        _platforms = new List<PlatformMovement>();
    }

    public void Init() {
        StartCoroutine(StartScrolling());
    }

    private void Update() {
        if (_player.position.y <= transform.position.y - 2f) {
            transform.position = new Vector3(0f, _player.position.y + 2f, 0f);
            _environment.position = new Vector3(0f, _player.position.y + 2f, 0f);
            _camera.position = new Vector3(0f, _player.position.y + 2f, -10f);
        }
    }

    public void StartMe() {
        if (!_isStarted) {
            StopCoroutine(nameof(StartScrolling));
            _isStarted = true;
            InitAllTiles();
            _platformPositioner.Init();
            foreach (EnvironmentMovement environmentElement in _environmentElements) {
                environmentElement.Init();
            }
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
        Instance = null;
    }
}
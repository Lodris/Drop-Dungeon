using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour {
    public static TilemapManager Instance { get; private set; }

    public float PlatformMovementSpeed = 3f;
    public Transform Platforms;
    public bool IsStarted = false;

#pragma warning disable 0649
    [SerializeField] private PlatformPositioner _platformPositioner;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _environment;
    [SerializeField] private List<Transform> _backgrounds;
    [SerializeField] private Transform _walls;
#pragma warning restore 0649

    private List<PlatformMovement> _platforms;

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
        if (_player != null && _player.position.y <= transform.position.y - 2f) {
            transform.position = new Vector3(0f, _player.position.y + 2f, 0f);
            _environment.position = new Vector3(0f, _player.position.y + 2f, 0f);
            _camera.position = new Vector3(0f, _player.position.y + 2f, -10f);
        }
    }

    public void StartMe() {
        if (!IsStarted) {
            StopCoroutine(nameof(StartScrolling));
            IsStarted = true;
            InitAllTiles();
            _platformPositioner.Init();
            foreach (Transform background in _backgrounds) {
                background.GetComponent<EnvironmentMovement>().Init();
            }
            foreach (Transform wall in _walls) {
                wall.GetComponent<EnvironmentMovement>().Init();
            }
        }
    }

    public void AddTile(PlatformMovement platformMovement) {
        _platforms.Add(platformMovement);
        if (IsStarted) {
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
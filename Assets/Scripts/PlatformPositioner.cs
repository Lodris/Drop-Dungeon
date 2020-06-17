using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformPositioner : MonoBehaviour {
    public static PlatformPositioner Instance { get; private set; }
    public List<Transform> LastTransforms = new List<Transform>();

#pragma warning disable 0649
    [SerializeField] private Transform _bottomRow;
    [Range(0, 100)] [SerializeField] private float _platformLength;
    [Range(0, 100)] [SerializeField] private float _gapLength;
    [Range(2, 5)] [SerializeField] private float _rowGapLength = 3;
    [Range(2, 16)] [SerializeField] private int _minPlatformLength = 2;
    [Range(1, 16)] [SerializeField] private int _minGapLength = 2;
    [SerializeField] private TileSpawner[] _tileSpawners;
    [SerializeField] private List<Sprite> _tileSprites;
#pragma warning restore 0649

    private int[] _tileSpawnersValues;
    private bool _isGap = false;
    private int _currentPlatformLength = 0;
    private int _currentGapLength = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    public void Init() {
        _tileSpawnersValues = new int[_tileSpawners.Length];
        LastTransforms.Add(_bottomRow);
    }

    private void Update() {
        if (LastTransforms.Count > 0) {
            if (LastTransforms[0].position.y >= _tileSpawners[0].transform.position.y + _rowGapLength) {
                LastTransforms.Clear();
                GetLine();
                EnemiesSpawningManager.Instance.PickRandomTilesToSpawnEnemiesOn();
            }
        }
    }

    private void GetLine() {
        _tileSpawnersValues[0] = IsContinuePlatform();

        if (_tileSpawnersValues[0] == 1) {
            _tileSpawnersValues[1] = 1;
            _currentPlatformLength = 2;
            _currentGapLength = 0;
        } else {
            _tileSpawnersValues[1] = IsContinueGap();
        }

        for (int i = 2; i < _tileSpawnersValues.Length; i++) {
            DecideTile(i);
        }

        if (!_isGap) {
            int rndTileIndex = Random.Range(1, _tileSpawners.Length - 1);
            _tileSpawnersValues[rndTileIndex] = 0;
            _tileSpawnersValues[rndTileIndex + 1] = 0;
        }
        _isGap = false;
        _currentPlatformLength = 0;
        _currentGapLength = 0;

        int spriteID = 1;
        if (_tileSpawnersValues[0] == 1) {
            spriteID = 0;
            _tileSpawners[0].Spawn(_tileSprites[spriteID]);
        }
        for (int i = 1; i < _tileSpawners.Length - 1; i++) {
            if (_tileSpawnersValues[i] == 1) {
                if (_tileSpawnersValues[i - 1] == 1 && _tileSpawnersValues[i + 1] == 1) {
                    spriteID = 1;
                    _tileSpawners[i].Spawn(_tileSprites[spriteID]);
                } else if (_tileSpawnersValues[i - 1] == 1 && _tileSpawnersValues[i + 1] == 0) {
                    spriteID = 2;
                    _tileSpawners[i].Spawn( _tileSprites[spriteID]);
                } else if (_tileSpawnersValues[i - 1] == 0) {
                    spriteID = 0;
                    _tileSpawners[i].Spawn(_tileSprites[spriteID]);
                }
            }
        }

        if (_tileSpawnersValues[_tileSpawners.Length - 1] == 1) {
            if (_tileSpawnersValues[_tileSpawners.Length - 2] == 1) {
                spriteID = 2;
                _tileSpawners[_tileSpawners.Length - 1].Spawn(_tileSprites[spriteID]);
            } else {
                spriteID = 1;
                _tileSpawners[_tileSpawners.Length - 1].Spawn(_tileSprites[spriteID]);
            }
        }
    }

    private void DecideTile(int index) {
        if (_tileSpawnersValues[index - 1] == 1) {
            if (_currentPlatformLength < _minPlatformLength) {
                _tileSpawnersValues[index] = 1;
                _currentPlatformLength++;
                _currentGapLength = 0;
            } else {
                _tileSpawnersValues[index] = IsContinuePlatform();
            }
        } else {
            if (_currentGapLength < _minGapLength) {
                _tileSpawnersValues[index] = 0;
                _isGap = true;
                _currentGapLength++;
                _currentPlatformLength = 0;
            } else {
                _tileSpawnersValues[index] = IsContinueGap();
            }
        }
    }

    private int IsContinuePlatform() {
        float randomNum = Random.Range(0f, 100f);
        if (randomNum <= _platformLength) {
            _currentPlatformLength++;
            _currentGapLength = 0;
            return 1;
        }
        _isGap = true;
        _currentGapLength++;
        _currentPlatformLength = 0;
        return 0;
    }

    private int IsContinueGap() {
        float randomNum = Random.Range(0f, 100f);
        if (randomNum <= _gapLength) {
            _isGap = true;
            _currentGapLength++;
            _currentPlatformLength = 0;
            return 0;
        }
        _currentPlatformLength++;
        _currentGapLength = 0;
        return 1;
    }
}
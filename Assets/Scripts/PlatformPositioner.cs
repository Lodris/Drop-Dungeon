using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformPositioner : MonoBehaviour {
    public static Transform LastTransform;

#pragma warning disable 0649
    [SerializeField] private Transform _bottomRow;
    [Range(0, 100)] [SerializeField] private float _platformLength;
    [Range(0, 100)] [SerializeField] private float _gapLength;
    [Range(2, 8)] [SerializeField] private float _rowGapLength = 2;
    [SerializeField] private TileSpawner[] _tileSpawners;
#pragma warning restore 0649

    private int[] _tileSpawnersValues;
    private bool _isGap = false;

    public void Init() {
        _tileSpawnersValues = new int[_tileSpawners.Length];
        LastTransform = _bottomRow;
    }

    private void Update() {
        if (LastTransform != null) {
            if (LastTransform.position.y >= _tileSpawners[0].transform.position.y + _rowGapLength) {
                GetLine();
            }
        }
    }

    private void GetLine() {
        _tileSpawnersValues[0] = IsContinuePlatform();

        for (int i = 1; i < _tileSpawnersValues.Length; i++) {
            if (_tileSpawnersValues[i - 1] == 1) {
                _tileSpawnersValues[i] = IsContinuePlatform();
            } else {
                _tileSpawnersValues[i] = IsContinueGap();
                if (_tileSpawnersValues[i] == 0) {
                    _isGap = true;
                }
            }
        }

        if (!_isGap) {
            _tileSpawnersValues[_tileSpawners.Length - 1] = 0;
        }
        _isGap = false;

        for (int i = 0; i < _tileSpawners.Length; i++) {
            _tileSpawners[i].Spawn(_tileSpawnersValues[i]);
        }
    }

    private int IsContinuePlatform() {
        float randomNum = Random.Range(0f, 100f);
        if (randomNum <= _platformLength) {
            return 1;
        }
        return 0;
    }

    private int IsContinueGap() {
        float randomNum = Random.Range(0f, 100f);
        if (randomNum <= _gapLength) {
            return 0;
        }
        return 1;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawningManager : MonoBehaviour {
    public static EnemiesSpawningManager Instance { get; private set; }

#pragma warning disable 0649
    [SerializeField] private GameObject _enemyPrefab;
    [Range(1, 3)] [SerializeField] private int _maxEnemiesPerLine = 1;
#pragma warning restore 0649

    //private int _currentNumberOfEnemies = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }
    }

    public void Init() {

    }

    public void PickRandomTilesToSpawnEnemiesOn() {
        List<Transform> lastTransforms = PlatformPositioner.Instance.LastTransforms;
        int numberOfTiles = lastTransforms.Count;
        int numberOfEnemiesInCurrentLine = Random.Range(0, _maxEnemiesPerLine + 1);
        for (int i = 0; i < numberOfEnemiesInCurrentLine; i++) {
            int randomTileIndex = Random.Range(0, numberOfTiles);
            Vector3 position = lastTransforms[randomTileIndex].position;

            int maxXPosition = 0;
            for (int j = randomTileIndex; j < lastTransforms.Count; j++) {
                if (j + 1 < lastTransforms.Count) {
                    if (lastTransforms[j + 1].position.x == lastTransforms[j].position.x + 1) {
                        maxXPosition++;
                    } else {
                        break;
                    }
                }
            }

            int minXPosition = 0;
            for (int j = randomTileIndex; j >= 0; j--) {
                if (j - 1 > -1) {
                    if (lastTransforms[j - 1].position.x == lastTransforms[j].position.x - 1) {
                        minXPosition++;
                    } else {
                        break;
                    }
                }
            }

            SpawnEnemy(position, minXPosition, maxXPosition);
        }
    }

    private void SpawnEnemy(Vector3 position, int minXPosition, int maxXPosition) {
        GameObject enemy = Instantiate(_enemyPrefab);
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        float halfHeight = 0;
        halfHeight = sprite.rect.height;
        halfHeight = halfHeight * enemy.transform.lossyScale.y / sprite.pixelsPerUnit;
        halfHeight = halfHeight / 2f;
        enemy.transform.position = new Vector3(position.x, position.y + 0.5f + halfHeight, 0f);

        EnemyManager enemyManager = enemy.GetComponent<EnemyManager>();
        enemyManager.Init(minXPosition, maxXPosition);
    }
}
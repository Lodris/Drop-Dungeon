using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {
    public Sprite Sprite;

#pragma warning disable 0649
    [SerializeField] private GameObject tile;
#pragma warning restore 0649

    private SpriteRenderer spriteRenderer;

    public void Spawn(Sprite sprite) {
        Transform tileTransform = Instantiate(tile, transform.position, Quaternion.identity, TilemapManager.Instance.Platforms).transform;
        PlatformPositioner.LastTransform = tileTransform;
        spriteRenderer = tileTransform.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}
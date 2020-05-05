using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {
#pragma warning disable 0649
    [SerializeField] private GameObject tile;
#pragma warning restore 0649

    public void Spawn(int num) {
        if (num == 1) {
            PlatformPositioner.LastTransform = Instantiate(tile, transform.position, Quaternion.identity,transform.parent).transform;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour {
    private void Awake() {
        if (!GameData.SoundtrackPlayed) {
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            DontDestroyOnLoad(gameObject);
            GameData.SoundtrackPlayed = true;
        }
    }
}
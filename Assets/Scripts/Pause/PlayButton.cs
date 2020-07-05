using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : ButtonBase {
    protected override void OnClick() {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Pause");
    }
}
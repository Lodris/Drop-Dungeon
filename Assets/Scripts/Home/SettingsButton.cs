using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButton : ButtonBase {
    protected override void OnClick() {
        SceneManager.LoadSceneAsync("Options", LoadSceneMode.Additive);
    }
}
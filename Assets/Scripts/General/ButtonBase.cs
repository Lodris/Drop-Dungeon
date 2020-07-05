using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ButtonBase : MonoBehaviour {
    protected virtual void Awake() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();
}
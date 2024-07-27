using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObjectVisual : MonoBehaviour {

    [SerializeField] private Transform baseTransform;

    private IInteractable baseInteractable;

    private void Awake() {
        baseInteractable = baseTransform.GetComponent<IInteractable>();
        if (baseInteractable == null) {
            Debug.LogError("baseTransform does not have component with IInteractable");
        }
    }

    private void Start() {
        Player.Instance.OnSelectedObjectChanged += Player_OnSelectedObjectChanged;
    }

    private void Player_OnSelectedObjectChanged(object sender, Player.OnSelectedObjectChangedEventArgs e) {
        if (e.selectedObject == baseInteractable) {
            Show();
        } else {
            Hide();
        }
    }

    private void OnDestroy() {
        Player.Instance.OnSelectedObjectChanged -= Player_OnSelectedObjectChanged;
    }

    private void Show() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }
    }

    private void Hide() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }


}
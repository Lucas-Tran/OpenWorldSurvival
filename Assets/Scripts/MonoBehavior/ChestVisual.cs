using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestVisual : MonoBehaviour {

    private const string OPEN = "Open";

    [SerializeField] private Chest chest;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        chest.OnInteract += Chest_OnInteract;
    }

    private void Chest_OnInteract(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN);
    }
}
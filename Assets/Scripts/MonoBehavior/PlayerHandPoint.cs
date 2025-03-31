using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandPoint : MonoBehaviour {


    private const string ON_HIT = "OnHit";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        player.OnHit += Player_OnHit;
    }

    private void Player_OnHit(object sender, System.EventArgs e) {
        animator.SetTrigger(ON_HIT);
    }
}
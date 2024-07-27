using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour, IInteractable, IHasHealth {


    public event EventHandler<IHasHealth.OnHealthChangedEventArgs> OnHealthChanged;
    [SerializeField] private float maxHealth;
    private float health;

    private void Awake() {
        health = maxHealth;

        OnHealthChanged?.Invoke(this, new IHasHealth.OnHealthChangedEventArgs {
            healthNormalized = health / maxHealth
        });
    }

    public void Interact(Player player) {
        if (player.HasItemWithCatagory(ItemSO.ItemCatagories.Pickaxe)) {
            health -= 1;

            OnHealthChanged?.Invoke(this, new IHasHealth.OnHealthChangedEventArgs {
                healthNormalized = health / maxHealth
            });

            Player.Instance.Hit();
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }


}
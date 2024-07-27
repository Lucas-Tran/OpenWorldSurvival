using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tree : MonoBehaviour, IInteractable, IHasHealth {


    public event EventHandler<IHasHealth.OnHealthChangedEventArgs> OnHealthChanged;

    [SerializeField] private int maxHealth;
    private int health;

    private void Awake() {
        health = maxHealth;
        OnHealthChanged?.Invoke(this, new IHasHealth.OnHealthChangedEventArgs {
            healthNormalized = (float)health / maxHealth
        });
    }

    public void Interact(Player player) {
        if (player.HasItemWithCatagory(ItemSO.ItemCatagories.Axe)) {
            health -= 1;
            OnHealthChanged?.Invoke(this, new IHasHealth.OnHealthChangedEventArgs {
                healthNormalized = (float)health / maxHealth
            });
            Player.Instance.Hit();
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }


}
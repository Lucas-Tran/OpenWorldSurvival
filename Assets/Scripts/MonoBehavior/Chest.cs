using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable {

    [SerializeField] ItemSO itemSO;

    public event EventHandler OnInteract;

    public void Interact(Player player) {
        if (InventoryManager.Instance.TryAddItem(itemSO, out int itemSOIndex)) {
            OnInteract?.Invoke(this, EventArgs.Empty);

            player.SetHeldItemInventoryIndex(itemSOIndex);
        }
    }

}
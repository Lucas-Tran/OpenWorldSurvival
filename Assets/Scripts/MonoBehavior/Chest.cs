using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable {

    [SerializeField] ItemSO itemSO;

    public event EventHandler OnInteract;

    public void Interact(Player player) {
        if (!player.HasItem()) {
            OnInteract?.Invoke(this, EventArgs.Empty);

            Transform itemTransform = Instantiate(itemSO.prefab);
            itemTransform.GetComponent<Item>().SetItemParent(player);
            InventoryUI.Instance.TryAddItem(itemSO);
        }
    }

}
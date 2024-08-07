using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {


    public static InventoryUI Instance { get; private set; }

    [SerializeField] private Transform container;
    [SerializeField] private Transform inventorySlotTemplate;


    private void Start() {
        InventoryManager.Instance.OnShowInventory += InventoryManager_OnShowInventory;
        InventoryManager.Instance.OnHideInventory += InventoryManager_OnHideInventory;
        InventoryManager.Instance.OnItemAdded += InventoryManager_OnItemAdded;

        UpdateVisual();
        Hide();
    }

    private void InventoryManager_OnItemAdded(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void InventoryManager_OnHideInventory(object sender, EventArgs e) {
        Hide();
    }

    private void InventoryManager_OnShowInventory(object sender, EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == inventorySlotTemplate) continue;
            Destroy(child.gameObject);
        }

        for (int i = 0; i < InventoryManager.Instance.GetItemSOArray().Length; i++) {
            Transform inventorySlot = Instantiate(inventorySlotTemplate, container);
            inventorySlot.gameObject.SetActive(true);
            inventorySlot.GetComponent<InventorySingleUI>().SetIconItemSO(InventoryManager.Instance.GetItemSOArray()[i]);
        }
    }


}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {


    public static InventoryManager Instance { get; private set; }

    public event EventHandler OnItemAdded;
    public event EventHandler OnShowInventory;
    public event EventHandler OnHideInventory;

    private bool inInventory = false;
    private ItemSO[] itemSOArray;
    private int itemsCount;

    private void Awake() {
        Instance = this;

        int inventorySlots = 10;

        itemSOArray = new ItemSO[inventorySlots];
    }

    private void Start() {
        GameInput.Instance.OnToggleInventory += GameInput_OnToggleInventory;
    }

    private void GameInput_OnToggleInventory(object sender, EventArgs e) {
        ToggleInventory();
    }

    private void ToggleInventory() {
        inInventory = !inInventory;
        if (inInventory) {
            OnShowInventory?.Invoke(this, EventArgs.Empty);
        } else {
            OnHideInventory?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool TryAddItem(ItemSO itemSO, out int itemSOIndex) {
        if (itemsCount < itemSOArray.Length) {
            itemSOIndex = itemsCount;
            itemSOArray[itemSOIndex] = itemSO;

            itemsCount += 1;
            OnItemAdded?.Invoke(this, EventArgs.Empty);
            return true;
        } else {
            itemSOIndex = -1;
            return false;
        }
    }

    public ItemSO[] GetItemSOArray() {
        return itemSOArray;
    }


}
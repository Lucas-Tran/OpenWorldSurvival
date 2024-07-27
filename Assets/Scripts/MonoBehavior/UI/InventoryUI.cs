using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {


    public static InventoryUI Instance { get; private set; }

    [SerializeField] private Transform container;
    [SerializeField] private Transform inventorySlotTemplate;

    private bool inInventory = false;
    private ItemSO[] itemSOArray;
    private int itemsCount;

    private void Awake() {
        Instance = this;

        int inventorySlots = 10;

        itemSOArray = new ItemSO[inventorySlots];

        UpdateVisual();
    }

    private void Start() {
        GameInput.Instance.OnToggleInventory += GameInput_OnToggleInventory;

        Hide();
    }

    private void GameInput_OnToggleInventory(object sender, System.EventArgs e) {
        ToggleInventory();
    }

    private void ToggleInventory() {
        inInventory = !inInventory;
        if (inInventory) {
            Show();
        } else {
            Hide();
        }
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

        for (int i = 0; i < itemSOArray.Length; i++) {
            Transform inventorySlot = Instantiate(inventorySlotTemplate, container);
            inventorySlot.gameObject.SetActive(true);
            inventorySlot.GetComponent<InventorySingleUI>().SetIconItemSO(itemSOArray[i]);
        }
    }

    public bool TryAddItem(ItemSO itemSO) {
        if (itemsCount < itemSOArray.Length) {
            itemSOArray[itemsCount] = itemSO;
            itemsCount += 1;
            UpdateVisual();
            return true;
        } else {
            return false;
        }
    }


}
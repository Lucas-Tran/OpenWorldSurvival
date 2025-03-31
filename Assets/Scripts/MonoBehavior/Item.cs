using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable {


    [SerializeField] private ItemSO itemSO;

    private IItemParent itemParent;

    public ItemSO GetItemSO() {
        return itemSO;
    }

    public void SetItemParent(IItemParent itemParent) {
        if (itemParent.HasItem()) {
            Debug.LogError("itemParent already has an item");
        }

        this.itemParent = itemParent;

        Transform itemParentHoldPoint = itemParent.GetItemHoldPoint();

        transform.parent = itemParentHoldPoint;
        itemParent.SetItem(this);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    public IItemParent GetItemParent() {
        return itemParent;
    }

    public void DestroySelf() {
        itemParent.ClearItem();
        Destroy(gameObject);
    }

   public void Interact(Player player) {
      InventoryManager.Instance.TryAddItem(itemSO, out int itemSOIndex);
      Destroy(gameObject);
   }
}
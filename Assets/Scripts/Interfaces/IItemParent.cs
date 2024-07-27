using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemParent {

    public Transform GetItemHoldPoint();

    public Item GetItem();

    public void SetItem(Item item);

    public bool HasItem();

    public void ClearItem();

    public bool HasItemWithCatagory(ItemSO.ItemCatagories itemCatagory);

}
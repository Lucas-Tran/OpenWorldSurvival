using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject {

    public enum ItemCatagories {
        Axe,
        Pickaxe,
        Material
    }

    public Transform prefab;
    public ItemCatagories catagory;
    public Sprite iconSprite;
    public string itemName;

}
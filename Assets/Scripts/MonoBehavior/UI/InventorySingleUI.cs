using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySingleUI : MonoBehaviour {


    [SerializeField] private Image iconSprite;

    public void SetIconItemSO(ItemSO itemSO) {
        if (itemSO != null) {
            ShowIconSprite();
            iconSprite.sprite = itemSO.iconSprite;
        } else {
            HideIconSprite();
        }
    }

    private void ShowIconSprite() {
        iconSprite.gameObject.SetActive(true);
    }

    private void HideIconSprite() {
        iconSprite.gameObject.SetActive(false);
    }


}
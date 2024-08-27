using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Item {
    public enum ItemType{
        RedKey,
        BlueKey,
        YellowKey,
        GreenKey,
        Wood,

    }
public ItemType itemType;
public int amount;

public Sprite GetSprite() {
    switch (itemType) {
        default:
        case ItemType.RedKey: return ItemAssets.Instance.redKeySprite;

        case ItemType.BlueKey: return ItemAssets.Instance.bluKeySprite;

        case ItemType.YellowKey: return ItemAssets.Instance.ylwKeySprite;

        case ItemType.GreenKey: return ItemAssets.Instance.grnKeySprite;
        
        case ItemType.Wood: return ItemAssets.Instance.woodSprite;

    }
}

public string GetDesc() {
    switch (itemType) {
        default:
        case ItemType.RedKey: return ItemAssets.Instance.redKeyDesc;

        case ItemType.BlueKey: return ItemAssets.Instance.bluKeyDesc;

        case ItemType.YellowKey: return ItemAssets.Instance.ylwKeyDesc;

        case ItemType.GreenKey: return ItemAssets.Instance.grnKeyDesc;
        
        case ItemType.Wood: return ItemAssets.Instance.woodDesc;

    }
}

}

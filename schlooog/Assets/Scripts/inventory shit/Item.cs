using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Item {
    public enum ItemType{
        redKey,
        bluKey,
        ylwKey,
        grnKey,

    }
public ItemType itemType;
public int amount;

public Sprite GetSprite() {
    switch (itemType) {
        default:
        case ItemType.redKey: return ItemAssets.Instance.redKeySprite;

        case ItemType.bluKey: return ItemAssets.Instance.bluKeySprite;

        case ItemType.ylwKey: return ItemAssets.Instance.ylwKeySprite;

        case ItemType.grnKey: return ItemAssets.Instance.grnKeySprite;

    }
}

}

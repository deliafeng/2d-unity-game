using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    private List<Item> itemList;

    public Inventory() {
        itemList = new List<Item>();

        AddItem (new Item { itemType = Item.ItemType.redKey, amount = 1 });
        AddItem (new Item { itemType = Item.ItemType.bluKey, amount = 1 });
        AddItem (new Item { itemType = Item.ItemType.ylwKey, amount = 1 });
        AddItem (new Item { itemType = Item.ItemType.grnKey, amount = 1 });
    }

    public void AddItem(Item item) {
        itemList.Add(item);
    }

    public List<Item> GetItemList() {
        return itemList;
    }

}

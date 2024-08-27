using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public class Inventory {

    private List<Item> itemList;
    public event EventHandler OnItemListChanged;
    public Inventory() {
        itemList = new List<Item>();

        /*AddItem (new Item { itemType = Item.ItemType.redKey, amount = 1 });
        AddItem (new Item { itemType = Item.ItemType.bluKey, amount = 1 });
        AddItem (new Item { itemType = Item.ItemType.ylwKey, amount = 1 });
        AddItem (new Item { itemType = Item.ItemType.grnKey, amount = 1 });*/
    }

    public void AddItem(Item item) {
        
            itemList.Add(item);
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        
    }

    public List<Item> GetItemList() {
        
        return itemList;
    }

    public void RemoveItem(Item removedItem)
    {
        
        itemList.Remove(removedItem);
    }

    public void ShowDesc(Item item){

    }

}

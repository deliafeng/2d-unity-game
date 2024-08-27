using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using System;
[System.Serializable]


public class UIInventory : MonoBehaviour
{    
    private Inventory inv;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;


    private void Awake() {
    //   itemSlotContainer = transform.Find("itemSlotContainer");
    //   itemSlotTemplate = transform.Find("itemSlotTemplate");
    }


    public void SetInventory(Inventory inventory) {
        inv = inventory;
        inv.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
        
    }
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e) {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems() {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 120f;
        foreach (Item item in inv.GetItemList()) {
         
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            //On left click, show item desc
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => {
                Debug.Log("CLICK");
                itemName.gameObject.SetActive(true);
                itemName.text = item.itemType.ToString();
                itemDesc.gameObject.SetActive(true);
                itemDesc.text = item.GetDesc();
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                Debug.Log("ricklig");
            };
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y*itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("itemImage").GetComponent<Image>();
            image.sprite = item.GetSprite();

            
            
            
            x++;
            if (x>=3) {
      
                x = 0;
                y--;
            }
        }
    }

}

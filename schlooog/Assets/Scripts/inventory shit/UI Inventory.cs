using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class UIInventory : MonoBehaviour
{    
    private Inventory inv;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;




    private void Awake() {
    //   itemSlotContainer = transform.Find("itemSlotContainer");
    //   itemSlotTemplate = transform.Find("itemSlotTemplate");
    }


    public void SetInventory(Inventory inventory) {
        inv = inventory;
        RefreshInventoryItems();
        
    }

    private void RefreshInventoryItems() {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 120f;
        foreach (Item item in inv.GetItemList()) {

            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

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

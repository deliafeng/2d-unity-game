using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ItemAssets : MonoBehaviour
{

    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;

    }

    public Sprite redKeySprite;
    public Sprite bluKeySprite;
    public Sprite ylwKeySprite;
    public Sprite grnKeySprite;
    public Sprite woodSprite;

    public string redKeyDesc;
    public string bluKeyDesc;
    public string ylwKeyDesc;
    public string grnKeyDesc;
    public String woodDesc;

}

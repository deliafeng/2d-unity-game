using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}

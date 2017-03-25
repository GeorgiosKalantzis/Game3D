using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {

    public enum ITEMTYPE {KNIFE,GUN};

    public ITEMTYPE Type;

    public Sprite GUI_ICON = null;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("fasdf");
        if (!other.CompareTag("Snek"))
        {
            return; 
        }

        Inventory.AddItem(gameObject);

    }
}

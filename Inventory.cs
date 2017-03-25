using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private static Inventory ThisInstance = null;

    public static Inventory Instance
    {
        get
        {
            if (ThisInstance == null)
            {
                GameObject InventoryObject = new GameObject("Inventory");

                ThisInstance = InventoryObject.AddComponent<Inventory>();
            }

            return ThisInstance;
        }
    }

    public RectTransform ItemList = null;

    void Awake()
    {
        if (ThisInstance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        ThisInstance = this;
    } 

    public static void AddItem(GameObject GO)
    {

        foreach(Collider C in GO.GetComponents<Collider>())
        {
            C.enabled = false;
        }

        foreach ( MeshRenderer MR in GO.GetComponents<MeshRenderer>())
        {
            MR.enabled = false;
        }

        for (int i=0; i<ThisInstance.ItemList.childCount; i++)
        {
            Transform Item = ThisInstance.ItemList.GetChild(i);

            if (!Item.gameObject.activeSelf)
            {
                Item.GetComponent<Image>().sprite = GO.GetComponent<InventoryItem>().GUI_ICON;

                Item.gameObject.SetActive(true);

                return;

            }
        }
    }
}

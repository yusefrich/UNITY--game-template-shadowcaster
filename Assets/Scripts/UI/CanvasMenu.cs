using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class CanvasMenu : MonoBehaviour
{
    [Header("editing shadows")]
    public GameObject editingPanel;
    [Header("Inventory items render")]
    public List<ItemInventorySlot> items;
    public GameObject inventory;
    public GameObject itemSlotPrefab;
    private int intantiatedSlotCount = 1;
    public static CanvasMenu Instance;

    private void Start()
    {
        Instance = this;
    }

    public void SetEditingPanel(bool active)
    {
        editingPanel.SetActive(active);
    }

    private void Update()
    {
        UpdateInventoryItems();
    }

    public void UpdateInventoryItems()
    {
        //instanciate slots if necessary
        if (InventoryManager.Instance.GetItems().Count > items.Count)
        {
            for (int i = 0; i < InventoryManager.Instance.GetItems().Count; i++)
            {
                if (i >= intantiatedSlotCount)
                {
                    GameObject newSlotInstance = Instantiate(itemSlotPrefab, inventory.transform);
                    newSlotInstance.transform.localPosition = items.ElementAt(i - 1).gameObject.transform.localPosition;
                    newSlotInstance.transform.localPosition = new Vector3(
                        newSlotInstance.transform.localPosition.x + 100, 
                        newSlotInstance.transform.localPosition.y,
                        newSlotInstance.transform.localPosition.z);
                    items.Add(newSlotInstance.GetComponent<ItemInventorySlot>());
                    intantiatedSlotCount++;
                }
            }
        }
        //update items
        for (int i = 0; i < InventoryManager.Instance.GetItems().Count; i++)
        {
            items.ElementAt(i).CurrentItemSprite(InventoryManager.Instance.GetItems().ElementAt(i).artWork);
            
            if (InventoryManager.Instance.GetItems().ElementAt(i).GetHolder() !=
                null)
            {

                    switch (InventoryManager.Instance.GetItems().ElementAt(i).GetHolder().GetType())
                    {
                        case HolderType.player:
                            items.ElementAt(i).ActivateItem();
                            break;
                        case HolderType.shadow:
                            items.ElementAt(i).ActivateToShadow();
                            
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                
/*
                if (InventoryManager.Instance.GetItems().ElementAt(i).GetHolder().GetHolder().gameObject
                    .CompareTag("Player"))
                {
                }
*/
            }
            else
            {
                items.ElementAt(i).DisableItem();
            }
            
        }
    }
}

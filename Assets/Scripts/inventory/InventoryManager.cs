using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))] //only the player can manage the inventory
public class InventoryManager : MonoBehaviour
{
    List<Item> itemsList = new List<Item>();
    public Item empityItem;

    public static InventoryManager Instance;

    //input manager variables
    private bool dpadBeingUsed = false;



    /// <summary>
    /// Storing items type to search for them on UseItems() method
    /// </summary>
    public enum ItemType
    {
        ///<summary>used on for atacking</summary>
        active,

        ///<summary>gets called all the tipe if is equiped</summary>
        passive,

        ///<summary>add new shadows, collectables</summary>
        collectable,
        notUsable
    }



    //code part
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ResetItemsHolders();
        AddItemToInventory(empityItem);
    }

    public void AddItemToInventory(Item itemToAdd)
    {
        itemsList.Add(itemToAdd);
    }

    public List<Item> GetItems()
    {
        return itemsList;
    }

    /// <summary>
    /// Reset all the items holder to set a new one
    /// </summary>
    public void ResetItemsHolders()
    {
        foreach (Item item in itemsList)
        {
            item.SetHolder(null);
        }
    }

    /// <summary>
    /// reset onlu the itens holded by the plauyer
    /// </summary>
    public void ResetPlayerItemsHolders()
    {
        foreach (Item item in itemsList)
        {
            if (item.GetHolder() != null)
            {
                if (item.GetHolder().GetType() == HolderType.shadow)
                {
                    continue;
                }
            }

            item.SetHolder(null);
        }
    }

    public void ResetShadowItemsHolders()
    {
        foreach (Item item in itemsList)
        {
            if (item.GetHolder() != null)
            {
                if (item.GetHolder().GetType() == HolderType.player)
                {
                    continue;
                }
            }

            item.SetHolder(null);
        }
    }


    public void SetNewItemHolder(_ItemHolder holderToItem)
    {
        bool itemOut = false;

        foreach (Item item in itemsList)
        {
            if (item.GetHolder() == holderToItem)
            {
                item.SetHolder(null);
                itemOut = true;
                break;
            }
        }

        if (itemOut)
            return;
        
        foreach (Item item in itemsList)
        {
            if (item.GetHolder() == null)
                continue;

            if (item.GetHolder().GetType() == HolderType.player)
            {
                item.SetHolder(null);
                item.SetHolder(holderToItem);
                SelectNoneItemInInventory();
                break;
            }
        }
    }

    private void Update()
    {
        //using item 
        if (Input.GetButtonDown("Atack"))
        {
            UseItems();
        }

        //selecting item
        if (Input.GetAxis("HorizontalDpad") > 0 && !dpadBeingUsed)
        {
            print("right");
            dpadBeingUsed = true;
            //select the next item in the inventory
            NextItemInInventory();
        }
        else if (Input.GetAxis("HorizontalDpad") < 0 && !dpadBeingUsed)
        {
            print("left");
            dpadBeingUsed = true;
            //select the previous item in the 
            PreviousItemInInventory();

        }
        else if (Input.GetAxis("HorizontalDpad") == 0)
        {
            dpadBeingUsed = false;
        }
        //dropping item to shadow

    }

    public void NextItemInInventory()
    {
        int itemIndex = -1;

        foreach (Item item in itemsList)
        {
            itemIndex++;
            if (item.GetHolder() == null)
            {
                continue;
            }

            //the rest of the loop is only called in the current iteration is on the current selected item
            //and if theres space to the right of the list
            if (item.GetHolder().GetType() != HolderType.player || (itemIndex + 1 >= itemsList.Count))
            {
                continue;
            }
            int nextListvalue = GetNextItemInList(itemIndex);

            if (nextListvalue > -1)
            {
                ResetPlayerItemsHolders();
                itemsList.ElementAt(nextListvalue).SetHolder(GetComponent<Player>());
                break;

            }
        }
    }

    int GetNextItemInList(int currentValue)
    {
        if (currentValue + 1 >= itemsList.Count)
        {
            return -1;
        }

        if (itemsList.ElementAt(currentValue + 1).GetHolder() == null)
        {
            return currentValue + 1;
        }
        else
        {
            return GetNextItemInList(currentValue + 1);
        }
    }
    int GetPreviousItemInList(int currentValue)
    {
        if (currentValue - 1 < 0)
        {
            return -1;
        }

        if (itemsList.ElementAt(currentValue - 1).GetHolder() == null)
        {
            return currentValue - 1;
        }
        else
        {
            return GetPreviousItemInList(currentValue - 1);
        }
    }


    public void SelectNoneItemInInventory()
    {
        int itemIndex = -1;
        foreach (Item item in itemsList)
        {
            if (item.myType == ItemType.notUsable)
            {
                item.SetHolder(GetComponent<Player>());
            }
        }

    }
    public void PreviousItemInInventory()
    {
        int itemIndex = -1;
        foreach (Item item in itemsList)
        {
            itemIndex++;
            if (item.GetHolder() == null)
            {
                continue;
            }
            
            //the rest of the loop is only called in the current iteration is on the current selected item
            //and if theres space to the right of the list
            if (item.GetHolder().GetType() != HolderType.player || (itemIndex - 1 < 0))
            {
                continue;
            }
            
            int nextListvalue = GetPreviousItemInList(itemIndex);

            if (nextListvalue > -1)
            {
                ResetPlayerItemsHolders();
                itemsList.ElementAt(nextListvalue).SetHolder(GetComponent<Player>());
                break;
            }
        }
    }
    
    public void UseItems()
    {
        //loop the list and use all items 
        foreach (Item item in itemsList)
        {
            if (item.GetHolder() == null)
            {
                continue;
            }

            switch (item.myType)
            {
                case ItemType.active:
                    item.GetHolder().UseActiveItem(item.itemEffect);
                    break;
                case ItemType.passive:
                    item.GetHolder().UsePassiveItem();
                    break;
                case ItemType.collectable:
                    item.GetHolder().UseCollectableItem();
                    break;
                case ItemType.notUsable:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void CollideWithItem(ItemVessel itemCollided)
    {
        
        ResetItemsHolders();
        itemCollided.SetHolder(GetComponent<Player>());
        AddItemToInventory(itemCollided.GetItem());//adds to the inventory
        itemCollided.DestroyVessel();//deletes the vessel

    }

/*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ItemVessel>() != null) //item vessel 
        {
            // getting a reference to the collided item
            ItemVessel collidedItem = other.GetComponent<ItemVessel>();
            
            ResetItemsHolders();
            collidedItem.SetHolder(GetComponent<Player>());
            AddItemToInventory(collidedItem.GetItem());//adds to the inventory
            collidedItem.DestroyVessel();//deletes the vessel
        }
    }
*/
}

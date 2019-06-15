using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemVessel : MonoBehaviour
{
    public Item item;


    public Item GetItem()
    {
        return item;
    }

    public void SetHolder(_ItemHolder itemToSet)
    {
        item.SetHolder(itemToSet);
    }
    
    public void DestroyVessel()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InventoryManager>().CollideWithItem(this);
            DestroyVessel();
        }
    }
}

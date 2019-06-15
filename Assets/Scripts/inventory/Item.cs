using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    //my item values
    public new string name;
    public string description;
    public Sprite artWork;
    public Animation anim;
    public InventoryManager.ItemType myType;
    public GameObject itemEffect;

    //holder reference
    private _ItemHolder holder = null;

    public _ItemHolder GetHolder()
    {
        return holder;
    }

    public void SetHolder(_ItemHolder holderToSet)
    {
        holder = holderToSet;
        
    }
    
    
}

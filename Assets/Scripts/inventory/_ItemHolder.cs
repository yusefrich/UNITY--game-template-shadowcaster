using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HolderType
{
    player, shadow
}


public interface _ItemHolder
{
    GameObject GetHolder();
    HolderType GetType();
    void UseActiveItem(GameObject itemEffect);
    void UsePassiveItem();
    void UseCollectableItem();
}

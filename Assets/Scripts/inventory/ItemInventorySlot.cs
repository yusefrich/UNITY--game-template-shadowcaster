using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventorySlot : MonoBehaviour
{
    public GameObject activated;
    public GameObject activatedToShadow;
    public GameObject disabled;
    public Image currentItemDisplayed;

    public void ActivateItem()
    {
        activated.SetActive(true);
        activatedToShadow.SetActive(false);
        disabled.SetActive(false);
    }

    public void ActivateToShadow()
    {
        activatedToShadow.SetActive(true);

        activated.SetActive(false);
        disabled.SetActive(false);
    }
    public void DisableItem()
    {
        activatedToShadow.SetActive(false);
        activated.SetActive(false);
        disabled.SetActive(true);
    }
    public void CurrentItemSprite(Sprite objectSprite)
    {
        currentItemDisplayed.gameObject.SetActive(true);
        currentItemDisplayed.sprite = objectSprite;
        currentItemDisplayed.SetNativeSize();
    }
}

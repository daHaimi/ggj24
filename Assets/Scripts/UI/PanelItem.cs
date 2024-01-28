using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelItem : MonoBehaviour
{
    public ItemSo Item;

    public void SetItem(ItemSo item)
    {
        this.Item = item;
        GetComponent<Image>().sprite = item.Icon;
    }
}

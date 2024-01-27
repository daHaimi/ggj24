using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The controller for the Ingame UI, including
/// laugh meter, items etc.
/// WARNING: This needs to be in the scene before calling Instance!
/// </summary>
public class UiIngameController : MonoBehaviour
{
    #region singleton
    public static UiIngameController Instance;

    public UiIngameController()
    {
        Instance = this;
    }
    #endregion

    [SerializeField]
    private Slider _sliderLaughMeter;
    [SerializeField]
    private Transform _panelItems;

    void LateUpdate()
    {
        _sliderLaughMeter.value = CharController.Instance.Laughing.LaughMeter / CharController.Instance.Laughing.LaughMeterMax;
    }

    /// <summary>
    /// Holds a dictionary of all visible Items and their panels
    /// </summary>
    private Dictionary<ItemSo, PanelItem> itemDictionary = new();

    /// <summary>
    /// Add an item to the Items Panel.
    /// Won't do anything if the item is already shown.
    /// </summary>
    /// <param name="item">The item to add</param>
    public void AddItemIcon(ItemSo item)
    {
        // Return from method if item is already visible
        if (itemDictionary.ContainsKey(item))
            return;

        var panelItem = Instantiate(GlobalDataSo.Instance.PrefabPanelItem, _panelItems.transform);
        panelItem.SetItem(item);

        itemDictionary.Add(item, panelItem);
    }

    /// <summary>
    /// Remove an item from the Items Panel.
    /// Won't do anything if the item is not shown.
    /// </summary>
    /// <param name="item">The item to remove</param>
    public void RemoveItemIcon(ItemSo item)
        {
        if (itemDictionary.ContainsKey(item))
        {
            Destroy(itemDictionary[item].gameObject);
            itemDictionary.Remove(item);
        }
    }
}

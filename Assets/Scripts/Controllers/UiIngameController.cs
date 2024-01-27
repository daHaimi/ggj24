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
    private Slider _sliderItemUsage;
    [SerializeField]
    private Image _sliderItemUsageImage;
    [SerializeField]
    private Transform _panelItems;

    private bool _itemUsageShown;

    void Start()
    {
        // Scale to 0 so we can animate lazily later
        _sliderItemUsage.transform.localScale = Vector3.zero;
    }

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

    /// <summary>
    /// Show the given item usage for a specific item.
    /// </summary>
    /// <param name="item">The item to show</param>
    /// <param name="value">The value between 0 and 1 to show. Value 1 hides the slider.</param>
    public void ShowItemUsage(ItemSo item, float value)
    {
        if ((value == 0 || value == 1) && _itemUsageShown)
        {
            LeanTween.cancel(_sliderItemUsage.gameObject);
            LeanTween.scale(_sliderItemUsage.gameObject, Vector3.zero, 0.3f)
                .setEaseOutSine();
            _itemUsageShown = false;
        }
        else if (!_itemUsageShown)
        {
            LeanTween.cancel(_sliderItemUsage.gameObject);
            LeanTween.scale(_sliderItemUsage.gameObject, Vector3.one, 0.3f)
                .setEaseOutSine();
            _itemUsageShown = true;
        }

        _sliderItemUsageImage.sprite = item.Icon;
        _sliderItemUsage.value = value;
    }
}

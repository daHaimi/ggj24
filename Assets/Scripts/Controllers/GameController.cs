using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Controller for Base Game Logic
/// </summary>
public class GameController : BaseSceneConsistentController
{
    #region singleton
    private static GameController _cachedInstance;
    public static GameController Instance
    {
        get
        {
            if (_cachedInstance == null)
                _cachedInstance = Instantiate(GlobalDataSo.Instance.GameController);

            return _cachedInstance;
        }
    }
    #endregion

    #region item handling
    /// <summary>
    /// Contains all items collected by the player.
    /// </summary>
    private List<ItemSo> CollectedItems = new();

    /// <summary>
    /// Adds an item to the list of collected items
    /// and to the UI.
    /// Won't do anything if Item is already in list.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(ItemSo item)
    {
        if (CollectedItems.Contains(item))
            return;

        AudioController.Instance.PlaySound(GlobalDataSo.Instance.SfxPickupItem);

        CollectedItems.Add(item);
        UiIngameController.Instance.AddItemIcon(item);
    }

    /// <summary>
    /// Removes an item from the list of collected items,
    /// and from the UI.
    /// Won't do anything if Item isn't in list.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    public void RemoveItem(ItemSo item)
    {
        if (CollectedItems.Contains(item))
            CollectedItems.Remove(item);

        AudioController.Instance.PlaySound(GlobalDataSo.Instance.SfxDropItem);

        UiIngameController.Instance.RemoveItemIcon(item);
    }

    /// <summary>
    /// Checks if the player has an Item.
    /// </summary>
    /// <param name="item">The Item to check for.</param>
    /// <returns>True, if the player has the item.</returns>
    public bool CheckForItem(ItemSo item)
    {
        return CollectedItems.Contains(item);
    }

    /// <summary>
    /// Checks if the player has an Item by the item title.
    /// </summary>
    /// <param name="item">The Item title to check for.</param>
    /// <returns>The item, if the player has it, or null.</returns>
    public ItemSo? CheckForItemByName(string itemTitle)
    {
        return CollectedItems.FirstOrDefault(x => x.Title == itemTitle);
    }
    #endregion

}

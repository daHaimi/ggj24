using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemSo", order = 1)]
public class ItemSo : ScriptableObject
{
    public string Title;
    public Sprite Icon;
}

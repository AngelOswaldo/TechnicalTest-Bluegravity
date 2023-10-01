using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClothesData", menuName = "Custom/ClothesData")]
public class ClothesData : ScriptableObject
{
    [field: SerializeField] private List<ItemInfo> AllLockedClothes = new List<ItemInfo>();
    [field: SerializeField] public List<ItemInfo> AllUnlockedClothes { get; private set; } = new List<ItemInfo>();

    private Dictionary<string, ItemInfo> _itemDictionary = new Dictionary<string, ItemInfo>();

    private void OnEnable()
    {
        BuildItemDictionary(AllLockedClothes);
        //PrintDictionaryValues();
    }

    private void BuildItemDictionary(List<ItemInfo> itemList)
    {
        foreach (var item in itemList)
        {
            if (!string.IsNullOrEmpty(item.Code))
            {
                _itemDictionary[item.Code] = item;
            }
        }
    }

    private void PrintDictionaryValues()
    {
        foreach (var item in _itemDictionary)
        {
            Debug.Log("Item Code: " + item.Key);
        }
    }

    public bool BuyClothes(string itemCode, int playerMoney)
    {
        if (_itemDictionary.TryGetValue(itemCode, out ItemInfo item) && item.Price <= playerMoney)
        {
            UnlockItem(item);
            return true;
        }
        return false;
    }

    public void SellClothes(string itemCode)
    {
        if (_itemDictionary.TryGetValue(itemCode, out ItemInfo item))
        {
            LockItem(item);
        }
    }

    public bool IsItemUnlocked(string itemCode)
    {
        return _itemDictionary.TryGetValue(itemCode, out ItemInfo item) && AllUnlockedClothes.Contains(item);
    }

    private void UnlockItem(ItemInfo item)
    {
        if (!AllUnlockedClothes.Contains(item))
        {
            AllUnlockedClothes.Add(item);
            AllLockedClothes.Remove(item);
        }
    }

    private void LockItem(ItemInfo item)
    {
        if (!AllLockedClothes.Contains(item))
        {
            AllLockedClothes.Add(item);
            AllUnlockedClothes.Remove(item);
        }
    }
}

[System.Serializable]
public class ItemInfo
{
    public ClothingClass Class;
    public int Id;
    public int Price;
    public string Code;
    public Sprite Icon;
}

[System.Serializable]
public enum ClothingClass
{
    SHIRT, PANTS, GLASSES, HAIRCUT, ACCESSORY
}

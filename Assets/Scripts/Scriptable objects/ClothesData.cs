using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClothesData", menuName = "Custom/ClothesData")]
public class ClothesData : ScriptableObject
{
    [SerializeField] private List<ItemInfo> _originalLockedClothes = new List<ItemInfo>();
    [field: SerializeField] public List<ItemInfo> AllUnlockedClothes { get; private set; } = new List<ItemInfo>();
    private List<ItemInfo> _currentLockedClothes = new List<ItemInfo>();

    private Dictionary<string, ItemInfo> _itemDictionary = new Dictionary<string, ItemInfo>();

    private void Awake()
    {

    }

    private void OnEnable()
    {
        ResetClothesLists();
        BuildItemDictionary(_currentLockedClothes);
        PrintDictionaryValues();
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

    public int GetItemPrice(string itemCode)
    {
        if (_itemDictionary.TryGetValue(itemCode, out ItemInfo item))
        {
            return item.Price;
        }
        return 0;
    }

    private void UnlockItem(ItemInfo item)
    {
        if (!AllUnlockedClothes.Contains(item))
        {
            AllUnlockedClothes.Add(item);
            _currentLockedClothes.Remove(item);
        }
    }

    private void LockItem(ItemInfo item)
    {
        if (!_currentLockedClothes.Contains(item))
        {
            _currentLockedClothes.Add(item);
            AllUnlockedClothes.Remove(item);
        }
    }

    private void ResetClothesLists()
    {
        _currentLockedClothes = new List<ItemInfo>(_originalLockedClothes);
        AllUnlockedClothes.Clear();
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

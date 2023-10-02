using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClothesData", menuName = "Custom/ClothesData")]
public class ClothesData : ScriptableObject
{
    [SerializeField] private List<ItemInfo> _originalLockedClothes = new List<ItemInfo>();
    [field: SerializeField] public List<ItemInfo> AllUnlockedClothes { get; private set; } = new List<ItemInfo>();
    private List<ItemInfo> _currentLockedClothes = new List<ItemInfo>();

    private Dictionary<string, ItemInfo> _itemDictionary = new Dictionary<string, ItemInfo>();

    private void OnEnable()
    {
        ResetClothesLists(); // Reset the clothes lists to their original state
        BuildItemDictionary(_currentLockedClothes); // Build the item dictionary with the current locked clothes
        PrintDictionaryValues(); // Print the values of the item dictionary
    }

    /// <summary>
    /// Build the item dictionary from a list of items.
    /// </summary>
    /// <param name="itemList">The list of items to build the dictionary from.</param>
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

    /// <summary>
    /// Print the values of the item dictionary.
    /// </summary>
    private void PrintDictionaryValues()
    {
        foreach (var item in _itemDictionary)
        {
            Debug.Log("Item Code: " + item.Key);
        }
    }

    /// <summary>
    /// Attempt to buy an item with the given item code and player money.
    /// </summary>
    /// <param name="itemCode">The code of the item to buy.</param>
    /// <param name="playerMoney">The player's current money.</param>
    /// <returns>True if the item was bought successfully, false otherwise.</returns>
    public bool BuyClothes(string itemCode, int playerMoney)
    {
        if (_itemDictionary.TryGetValue(itemCode, out ItemInfo item) && item.Price <= playerMoney)
        {
            UnlockItem(item); // Unlock the item
            return true; // Return true to indicate successful purchase
        }
        return false; // Return false if the purchase was not successful
    }

    /// <summary>
    /// Sell an item with the given item code.
    /// </summary>
    /// <param name="itemCode">The code of the item to sell.</param>
    public void SellClothes(string itemCode)
    {
        if (_itemDictionary.TryGetValue(itemCode, out ItemInfo item))
        {
            LockItem(item); // Lock the item
        }
    }

    /// <summary>
    /// Check if an item with the given item code is unlocked.
    /// </summary>
    /// <param name="itemCode">The code of the item to check.</param>
    /// <returns>True if the item is unlocked, false otherwise.</returns>
    public bool IsItemUnlocked(string itemCode)
    {
        return _itemDictionary.TryGetValue(itemCode, out ItemInfo item) && AllUnlockedClothes.Contains(item);
    }

    /// <summary>
    /// Get the price of an item with the given item code.
    /// </summary>
    /// <param name="itemCode">The code of the item to get the price for.</param>
    /// <returns>The price of the item, or 0 if the item is not found.</returns>
    public int GetItemPrice(string itemCode)
    {
        if (_itemDictionary.TryGetValue(itemCode, out ItemInfo item))
        {
            return item.Price;
        }
        return 0; // Return 0 if the item is not found
    }

    /// <summary>
    /// Unlock an item and move it to the list of unlocked clothes.
    /// </summary>
    /// <param name="item">The item to unlock.</param>
    private void UnlockItem(ItemInfo item)
    {
        if (!AllUnlockedClothes.Contains(item))
        {
            AllUnlockedClothes.Add(item);
            _currentLockedClothes.Remove(item);
        }
    }

    /// <summary>
    /// Lock an item and move it to the list of locked clothes.
    /// </summary>
    /// <param name="item">The item to lock.</param>
    private void LockItem(ItemInfo item)
    {
        if (!_currentLockedClothes.Contains(item))
        {
            _currentLockedClothes.Add(item);
            AllUnlockedClothes.Remove(item);
        }
    }

    /// <summary>
    /// Reset the clothes lists to their original state.
    /// </summary>
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

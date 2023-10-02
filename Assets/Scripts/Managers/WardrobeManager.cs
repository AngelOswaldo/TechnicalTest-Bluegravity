using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeManager : MonoBehaviour
{
    [SerializeField] private GameObject _wardrobePanel;
    [SerializeField] private List<GameObject> _clothingUIObjects;
    [SerializeField] private ClothesData _clothesData;

    public static Action OpenUIWarobeEvent;  // Event to open the wardrobe UI.

    private void OnEnable()
    {
        OpenUIWarobeEvent += OpenUIWardrobe; // Subscribe to the event when this script is enabled.
    }

    private void OnDisable()
    {
        OpenUIWarobeEvent -= OpenUIWardrobe; // Unsubscribe from the event when this script is disabled.
    }

    /// <summary>
    /// Opens the wardrobe UI by loading and displaying unlocked clothing items.
    /// </summary>
    private void OpenUIWardrobe()
    {
        LoadUIClothes();
        _wardrobePanel.SetActive(true);
    }

    /// <summary>
    /// Closes the wardrobe UI, disables the chest, and enables player movement and interaction.
    /// </summary>
    public void CloseUIWardrobe()
    {
        _wardrobePanel.SetActive(false);
        Wardrobe.CloseChestEvent?.Invoke();
        PlayerController.CanPlayerWalkEvent?.Invoke(true);
        PlayerInteraction.EnableInteractionEvent?.Invoke();
    }

    /// <summary>
    /// Informs the PlayerAnimation script to wear a shirt with the given value.
    /// </summary>
    /// <param name="value">The value representing the shirt to wear.</param>
    public void WearShirt(int value)
    {
        PlayerAnimation.ChangeShirtEvent?.Invoke(value);
    }

    /// <summary>
    /// Informs the PlayerAnimation script to wear pants with the given value.
    /// </summary>
    /// <param name="value">The value representing the pants to wear.</param>
    public void WearPants(int value)
    {
        PlayerAnimation.ChangePantsEvent?.Invoke(value);
    }

    /// <summary>
    /// Informs the PlayerAnimation script to wear glasses with the given value.
    /// </summary>
    /// <param name="value">The value representing the glasses to wear.</param>
    public void WearGlasses(int value)
    {
        PlayerAnimation.ChangeGlassesEvent?.Invoke(value);
    }

    /// <summary>
    /// Informs the PlayerAnimation script to wear a haircut with the given value.
    /// </summary>
    /// <param name="value">The value representing the haircut to wear.</param>
    public void WearHaircut(int value)
    {
        PlayerAnimation.ChangeHaircutEvent?.Invoke(value);
    }

    /// <summary>
    /// Informs the PlayerAnimation script to wear an accessory with the given value.
    /// </summary>
    /// <param name="value">The value representing the accessory to wear.</param>
    public void WearAccessory(int value)
    {
        PlayerAnimation.ChangeAccessoryEvent?.Invoke(value);
    }

    /// <summary>
    /// Loads and activates the UI objects corresponding to unlocked clothing items.
    /// </summary>
    private void LoadUIClothes()
    {
        foreach (var uiObject in _clothingUIObjects)
        {
            uiObject.SetActive(false);
        }

        foreach (var item in _clothesData.AllUnlockedClothes)
        {
            ActivateUIObjectByClothingCode(item.Code);
        }
    }

    /// <summary>
    /// Activates a UI object based on its clothing code.
    /// </summary>
    /// <param name="clothingCode">The code of the clothing item to activate.</param>
    private void ActivateUIObjectByClothingCode(string clothingCode)
    {
        GameObject uiObject = _clothingUIObjects.Find(obj => obj.name == clothingCode);
        if (uiObject != null)
        {
            uiObject.SetActive(true);
        }
    }
}

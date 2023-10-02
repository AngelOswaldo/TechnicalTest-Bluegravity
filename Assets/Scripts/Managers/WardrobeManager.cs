using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeManager : MonoBehaviour
{
    [SerializeField] private GameObject _wardrobePanel;
    [SerializeField] private List<GameObject> _clothingUIObjects;
    [SerializeField] private ClothesData _clothesData;

    public static Action OpenUIWarobeEvent;

    private void OnEnable()
    {
        OpenUIWarobeEvent += OpenUIWardrobe;
    }

    private void OnDisable()
    {
        OpenUIWarobeEvent -= OpenUIWardrobe;
    }

    private void OpenUIWardrobe()
    {
        LoadUIClothes();
        _wardrobePanel.SetActive(true);
    }

    public void CloseUIWardrobe()
    {
        _wardrobePanel.SetActive(false);
        Wardrobe.CloseChestEvent?.Invoke();
        PlayerController.CanPlayerWalkEvent?.Invoke(true);
    }

    public void WearShirt(int value)
    {
        PlayerAnimation.ChangeShirtEvent?.Invoke(value);
    }

    public void WearPants(int value)
    {
        PlayerAnimation.ChangePantsEvent?.Invoke(value);
    }

    public void WearGlasses(int value)
    {
        PlayerAnimation.ChangeGlassesEvent?.Invoke(value);
    }

    public void WearHaircut(int value)
    {
        PlayerAnimation.ChangeHaircutEvent?.Invoke(value);
    }

    public void WearAccessory(int value)
    {
        PlayerAnimation.ChangeAccessoryEvent?.Invoke(value);
    }

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

    private void ActivateUIObjectByClothingCode(string clothingCode)
    {
        GameObject uiObject = _clothingUIObjects.Find(obj => obj.name == clothingCode);
        if (uiObject != null)
        {
            uiObject.SetActive(true);
        }
    }
}

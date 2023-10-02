using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private float _currentShirtID = 0;
    [SerializeField] private float _currentPantsID = 0;
    [SerializeField] private int _currentGlassesID = 0;
    [SerializeField] private int _currentHaircutID = 0;
    [SerializeField] private int _currentAccessoryID = 0;

    [Header("Glasses")]
    [SerializeField] private SpriteRenderer _glassesRenderer;
    [SerializeField] private List<Sprite> _glassesSprites;

    [Header("Haircuts")]
    [SerializeField] private SpriteRenderer _haircutRenderer;
    [SerializeField] private List<Sprite> _haircutSprites;

    [Header("Accessories")]
    [SerializeField] private SpriteRenderer _accessoryRenderer;
    [SerializeField] private List<Sprite> _accessorySprites;


    public static Action<bool> WalkEvent;
    public static Action<bool> BodyFlipEvent;

    public static Action<int> ChangeShirtEvent;
    public static Action<int> ChangePantsEvent;
    public static Action<int> ChangeGlassesEvent;
    public static Action<int> ChangeHaircutEvent;
    public static Action<int> ChangeAccessoryEvent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        WalkEvent += WalkAnimation;
        BodyFlipEvent += FlipX;

        ChangeShirtEvent += ChangeShirt;
        ChangePantsEvent += ChangePants;
        ChangeGlassesEvent += ChangeGlasses;
        ChangeAccessoryEvent += ChangeAccessory;
        ChangeHaircutEvent += ChangeHaircut;
    }

    private void OnDisable()
    {
        WalkEvent -= WalkAnimation;
        BodyFlipEvent -= FlipX;

        ChangeShirtEvent -= ChangeShirt;
        ChangePantsEvent -= ChangePants;
        ChangeGlassesEvent -= ChangeGlasses;
        ChangeAccessoryEvent -= ChangeAccessory;
        ChangeHaircutEvent -= ChangeHaircut;
    }

    private void FlipX(bool value)
    {
        if (value)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void WalkAnimation(bool value)
    {
        _animator.SetBool("isWalking", value);
    }

    private void WearShirt(bool value)
    {
        if (value)
        {
            _animator.SetLayerWeight(1, 1f);
        }
        else
        {
            _animator.SetLayerWeight(1, 0f);
        }
    }

    private void WearPants(bool value)
    {
        if (value)
        {
            _animator.SetLayerWeight(2, 1f);
        }
        else 
        { 
            _animator.SetLayerWeight(2,0f);
        }
    }

    private void ChangeShirt(int value)
    {
        WearShirt(true);

        _animator.SetFloat("shirtID", value);
        _currentShirtID = value;
    }

    private void ChangePants(int value)
    {
        WearPants(true);

        _animator.SetFloat("pantsID", value);
        _currentPantsID = value;
    }

    private void ChangeGlasses(int value)
    {
        _glassesRenderer.sprite = _glassesSprites[value];
        _currentGlassesID = value;
    }

    private void ChangeHaircut(int value)
    {
        _haircutRenderer.sprite = _haircutSprites[value];
        _currentHaircutID = value;
    }

    private void ChangeAccessory(int value)
    {
        _accessoryRenderer.sprite = _accessorySprites[value];
        _currentAccessoryID = value;
    }
}

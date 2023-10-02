using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private float _currentShirtID = 0;
    [SerializeField] private float _currentPantsID = 0;
    [SerializeField] private int _currentGlassesID = 0;
    [SerializeField] private int _currentHaircutID = 0;
    [SerializeField] private int _currentAccessoryID = 0;

    [Header("Body")]
    [SerializeField] private SpriteRenderer _bodyRenderer;

    [Header("Shirt")]
    [SerializeField] private SpriteRenderer _shirtRenderer;

    [Header("Pants")]
    [SerializeField] private SpriteRenderer _pantsRenderer;

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

        int randomSkin = UnityEngine.Random.Range(1, 4);
        if(randomSkin == 1 ) 
        {
            _bodyRenderer.color = new Color32(253,223,203, 255);
        }
        else if (randomSkin == 2)
        {
            _bodyRenderer.color = new Color32(102, 59, 60, 255);
        }
        else if (randomSkin == 3)
        {
            _bodyRenderer.color = new Color32(225, 192, 151, 255);
        }
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

        if(value == 1)
        {
            _shirtRenderer.color = new Color32(107, 161, 255, 255);
        }
        else if (value == 2)
        {
            _shirtRenderer.color = new Color32(188, 255, 170, 255);
        }
        else if (value == 3)
        {
            _shirtRenderer.color = new Color32(255, 225, 0, 255);
        }

        _currentShirtID = value;
    }

    private void ChangePants(int value)
    {
        WearPants(true);
        _animator.SetFloat("pantsID", value);

        if (value == 1)
        {
            _pantsRenderer.color = new Color32(253, 167, 255, 255);
        }
        else if (value == 2)
        {
            _pantsRenderer.color = new Color32(125, 125, 125, 255);
        }

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

        if (value == 0)
        {
            _haircutRenderer.color = new Color32(48, 45, 45, 255);
        }
        else if (value == 1)
        {
            _haircutRenderer.color = new Color32(113, 76, 49, 255);
        }
        else if (value == 2)
        {
            _haircutRenderer.color = new Color32(255, 226, 81, 255);
        }
        else if(value == 3)
        {
            _haircutRenderer.color = new Color32(153, 93, 255, 255);
        }

        _currentHaircutID = value;
    }

    private void ChangeAccessory(int value)
    {
        _accessoryRenderer.sprite = _accessorySprites[value];
        _currentAccessoryID = value;
    }
}

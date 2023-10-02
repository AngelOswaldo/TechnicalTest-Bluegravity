using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    // Serialized fields for current clothing IDs
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


    // Events for character actions
    public static Action<bool> WalkEvent;
    public static Action<bool> BodyFlipEvent;

    // Events for changing clothing items
    public static Action<int> ChangeShirtEvent;
    public static Action<int> ChangePantsEvent;
    public static Action<int> ChangeGlassesEvent;
    public static Action<int> ChangeHaircutEvent;
    public static Action<int> ChangeAccessoryEvent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        // Randomly select skin color on awake
        int randomSkin = UnityEngine.Random.Range(1, 4);
        if (randomSkin == 1)
        {
            _bodyRenderer.color = new Color32(253, 223, 203, 255);
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
        // Subscribe to animation events
        WalkEvent += WalkAnimation;
        BodyFlipEvent += FlipX;

        // Subscribe to clothing change events
        ChangeShirtEvent += ChangeShirt;
        ChangePantsEvent += ChangePants;
        ChangeGlassesEvent += ChangeGlasses;
        ChangeAccessoryEvent += ChangeAccessory;
        ChangeHaircutEvent += ChangeHaircut;
    }

    private void OnDisable()
    {
        // Unsubscribe from animation events when disabled
        WalkEvent -= WalkAnimation;
        BodyFlipEvent -= FlipX;

        // Unsubscribe from clothing change events when disabled
        ChangeShirtEvent -= ChangeShirt;
        ChangePantsEvent -= ChangePants;
        ChangeGlassesEvent -= ChangeGlasses;
        ChangeAccessoryEvent -= ChangeAccessory;
        ChangeHaircutEvent -= ChangeHaircut;
    }

    /// <summary>
    /// Flip the character horizontally.
    /// </summary>
    /// <param name="value">If true, flip the character.</param>
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

    /// <summary>
    /// Control the walking animation.
    /// </summary>
    /// <param name="value">If true, start walking animation.</param>
    private void WalkAnimation(bool value)
    {
        _animator.SetBool("isWalking", value);
    }

    /// <summary>
    /// Control the visibility of the shirt layer.
    /// </summary>
    /// <param name="value">If true, show the shirt layer.</param>
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

    /// <summary>
    /// Control the visibility of the pants layer.
    /// </summary>
    /// <param name="value">If true, show the pants layer.</param>
    private void WearPants(bool value)
    {
        if (value)
        {
            _animator.SetLayerWeight(2, 1f);
        }
        else
        {
            _animator.SetLayerWeight(2, 0f);
        }
    }

    /// <summary>
    /// Change the character's shirt appearance.
    /// </summary>
    /// <param name="value">The new shirt ID.</param>
    private void ChangeShirt(int value)
    {
        WearShirt(true);
        _animator.SetFloat("shirtID", value);

        // Set the shirt color based on the provided value
        if (value == 1)
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

    /// <summary>
    /// Change the character's pants appearance.
    /// </summary>
    /// <param name="value">The new pants ID.</param>
    private void ChangePants(int value)
    {
        WearPants(true);
        _animator.SetFloat("pantsID", value);

        // Set the pants color based on the provided value
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

    /// <summary>
    /// Change the character's glasses appearance.
    /// </summary>
    /// <param name="value">The new glasses ID.</param>
    private void ChangeGlasses(int value)
    {
        _glassesRenderer.sprite = _glassesSprites[value];
        _currentGlassesID = value;
    }

    /// <summary>
    /// Change the character's haircut appearance.
    /// </summary>
    /// <param name="value">The new haircut ID.</param>
    private void ChangeHaircut(int value)
    {
        _haircutRenderer.sprite = _haircutSprites[value];

        // Set the haircut color based on the provided value
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
        else if (value == 3)
        {
            _haircutRenderer.color = new Color32(153, 93, 255, 255);
        }

        _currentHaircutID = value;
    }

    /// <summary>
    /// Change the character's accessory appearance.
    /// </summary>
    /// <param name="value">The new accessory ID.</param>
    private void ChangeAccessory(int value)
    {
        _accessoryRenderer.sprite = _accessorySprites[value];
        _currentAccessoryID = value;
    }
}

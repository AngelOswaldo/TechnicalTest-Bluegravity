using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    /// <summary>
    /// This method is called when an interaction with the object occurs.
    /// Subclasses can override this method to define specific interactions.
    /// </summary>
    public virtual void Interact()
    {
        // Implement interaction logic here.
        // For example, you can open a dialog, pick up an item, or trigger an event.
    }
}

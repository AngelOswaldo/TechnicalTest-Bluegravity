using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private void Awake()
    {
        // Pause the game by setting the time scale to 0.
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Called when the tutorial is finished.
    /// </summary>
    public void FinishTutorial()
    {
        // Resume the game by setting the time scale to 1.
        Time.timeScale = 1f;
    }
}

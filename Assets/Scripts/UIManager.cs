using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI TotalPointText;
    public TextMeshProUGUI CurrentPointText;
    public TextMeshProUGUI StatusText;

    #region Singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        // Initialize UI with default values
        if (TotalPointText != null)
            TotalPointText.text = "Points: 0";

        if (CurrentPointText != null)
            CurrentPointText.text = "";

        if (StatusText != null)
            StatusText.text = "Pull the lever to start!";
    }

    // Updates the total points displayed in the UI.
    public void UpdateTotalPoints(int totalPoints)
    {
        if (TotalPointText != null)
            TotalPointText.text = "Points: " + totalPoints;
    }

    // Shows how many points were added/removed.
    public void ShowCurrentPoints(int amount, bool added)
    {
        if (CurrentPointText != null)
            CurrentPointText.text = added ? "+" + amount : "-" + amount;
    }

    // Updates the winning text depending on result.
    public void ShowWinningText(bool isJackpot, string iconName)
    {
        if (StatusText == null) return;

        if (isJackpot)
        {
            StatusText.text = "JACKPOT!\n" + iconName;
        }
        else if (iconName == "No match")
        {
            StatusText.text = "No match found. Try again!";
        }
        else
        {
            StatusText.text = "You won with a pair!\n" + iconName;
        }
        StartCoroutine(ClearUIAfterDelay());
    }

    // Sets a custom status text (e.g., at start or after losing).
    public void SetStatusText(string message)
    {
        if (StatusText != null)
            StatusText.text = message;
    }

    // Clears the winning text after a delay.
    private IEnumerator ClearUIAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        StatusText.text = "Pull the lever to start!";
        CurrentPointText.text = "";
    }
}
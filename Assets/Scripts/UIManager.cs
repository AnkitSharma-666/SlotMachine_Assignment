using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI TotalPointText;
    public TextMeshProUGUI CurrentPointText;
    public TextMeshProUGUI StatusText;

    public float TextClearDelay = 3f;
    public float JackpotTextClearDelay = 5f;
    public SO_UIText UIText;

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
            StatusText.text = UIText.StartTips;
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
            StatusText.text = UIText.JackpotText + iconName;
            StartCoroutine(ClearUIAfterDelay(JackpotTextClearDelay));
            return;
        }
        else if (iconName == UIText.NoPairKeyValue)
        {
            StatusText.text = UIText.NoPairText;
        }
        else
        {
            StatusText.text = UIText.PairText + iconName;
        }
        StartCoroutine(ClearUIAfterDelay(TextClearDelay));
    }

    // Sets a custom status text (e.g., at start or after losing).
    public void SetStatusText(string message)
    {
        if (StatusText != null)
            StatusText.text = message;
    }

    // Clears the winning text after a delay.
    private IEnumerator ClearUIAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        StatusText.text = UIText.StartTips;
        CurrentPointText.text = "";
    }
}
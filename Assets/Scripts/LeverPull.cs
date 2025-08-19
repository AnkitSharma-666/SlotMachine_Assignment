using UnityEngine;
using UnityEngine.UI;

// Handles lever interactions: pulling, releasing, 
// deducting points, and triggering the slot spin.
public class LeverPull : MonoBehaviour
{
    [Header("Lever Visuals")]
    public SpriteRenderer LeverReleaseSprite;
    public SpriteRenderer LeverPullSprite;

    [Header("Lever Button")]
    public Button LeverButton;

    private void Start()
    {
        OnLeverReleased();
    }

    // Pulls the lever: deducts points and starts the spin if the player can afford it.
    public void OnLeverPulled()
    {
        if (SlotGame.Instance.TotalPoints >= SlotGame.Instance.MinPointsNeeded)
        {
            SlotGame.Instance.TotalPoints -= SlotGame.Instance.MinPointsNeeded;

            UIManager.Instance.UpdateTotalPoints(SlotGame.Instance.TotalPoints); // update total points in UI
            UIManager.Instance.ShowCurrentPoints(SlotGame.Instance.MinPointsNeeded, false); // show point deduction
            UIManager.Instance.StopAllCoroutines(); // ensure no lingering UI updates

            LeverReleaseSprite.enabled = false;
            LeverPullSprite.enabled = true;

            SlotGame.Instance.SpinSlotsTrigger();
            LeverButton.interactable = false;
        }
        else
        {
            Debug.Log("Not enough points to pull the lever.");
            UIManager.Instance.SetStatusText("Not enough points to pull the lever!");
        }
    }

    // Resets lever visuals and makes it interactable again.
    public void OnLeverReleased()
    {
        LeverReleaseSprite.enabled = true;
        LeverPullSprite.enabled = false;
        LeverButton.interactable = true;
    }
}

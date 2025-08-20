using System.Collections;
using UnityEngine;

// Handles the slot machine logic: spinning reels, checking results, 
// adding points, and triggering UI updates through UIManager.

public class SlotGame : MonoBehaviour
{
    [Header("Game Settings")]
    public int TotalPoints = 0;        // Current player points
    public int MinPointsNeeded = 10;   // Minimum points required to pull lever
    public int MinRollCycles = 10;     // Minimum spins per reel
    public int MaxRollCycles = 30;     // Maximum spins per reel
    public float SpinSpeed = 0.1f;
    public float NextTurnDelay = 3f;

    [Header("Reel References")]
    public SpriteRenderer LeftRow;
    public SpriteRenderer MidRow;
    public SpriteRenderer RightRow;

    [Header("Symbols")]
    public Sprite[] ResultSprites;     // Possible reel results

    [Header("Payout Values")]
    public int TripleMatchPoints;
    public int PairWithBar;
    public int PairWithCherry;
    public int PairWithSeven;
    public int PairWithBell;

    // Internal roll cycle counters
    private int _leftRollCycle = 0;
    private int _midRollCycle = 0;
    private int _rightRollCycle = 0;

    private int _leftIndex = 0;
    private int _midIndex = 0;
    private int _rightIndex = 0;

    #region Singleton
    public static SlotGame Instance { get; private set; }
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
        // Initialize UI
        UIManager.Instance.UpdateTotalPoints(TotalPoints);
        UIManager.Instance.SetStatusText("Pull the lever to start!");
    }

    // Trigger the slot machine spin when the lever is pulled.
    // Randomizes cycle counts for each reel and starts spinning coroutine.
    public void SpinSlotsTrigger()
    {
        _leftRollCycle = Mathf.RoundToInt(Random.Range(MinRollCycles, MaxRollCycles));
        _midRollCycle = Mathf.RoundToInt(Random.Range(MinRollCycles, MaxRollCycles));
        _rightRollCycle = Mathf.RoundToInt(Random.Range(MinRollCycles, MaxRollCycles));

        StartCoroutine(SpinSlots());
    }

    // Coroutine that spins reels by changing their sprites randomly
    // until roll cycles reach zero.
    IEnumerator SpinSlots()
    {
        AudioManager.Instance.RollingSlotsAudioStart();
        UIManager.Instance.SetStatusText("Spinning...");

        int tempLeft = 0, tempMid = 0, tempRight = 0;

        while (_leftRollCycle > 0 || _midRollCycle > 0 || _rightRollCycle > 0)
        {
            if (_leftRollCycle > 0)
            {
                tempLeft = (tempLeft + 1) % ResultSprites.Length;
                LeftRow.sprite = ResultSprites[tempLeft];
                _leftRollCycle--;
            }
            if (_midRollCycle > 0)
            {
                tempMid = (tempMid + 1) % ResultSprites.Length;
                MidRow.sprite = ResultSprites[tempMid];
                _midRollCycle--;
            }
            if (_rightRollCycle > 0)
            {
                tempRight = (tempRight + 1) % ResultSprites.Length;
                RightRow.sprite = ResultSprites[tempRight];
                _rightRollCycle--;
            }
                yield return new WaitForSeconds(SpinSpeed);
        }

<<<<<<< HEAD
        // Snap to your predetermined results
        LeftRow.sprite = ResultSprites[_leftIndex];
        MidRow.sprite = ResultSprites[_midIndex];
        RightRow.sprite = ResultSprites[_rightIndex];

        AudioManager.Instance.RollingSlotAudioStop();
=======
        AudioManager.Instance.RollingSlotAudioStop(); // stop audio when rolling stop.

>>>>>>> 52ed2fe (Slot Machine code Bug Fix)
        CalculateResult();
    }

    // Calculates the result after the reels stop spinning.
    // Handles triple match (jackpot) and pair matches with payouts.
    void CalculateResult()
    {
        // Jackpot: All three match
        if (LeftRow.sprite == MidRow.sprite && MidRow.sprite == RightRow.sprite)
        {
            AddPoints(TripleMatchPoints);
            UIManager.Instance.ShowWinningText(true, LeftRow.sprite.name);
            AudioManager.Instance.JackpotAudioTrigger();
            StartCoroutine(OnFinishRolling());
            return;
        }

        // Check for pairs
        Sprite matchedSprite = null;
        if (LeftRow.sprite == MidRow.sprite) matchedSprite = LeftRow.sprite;
        else if (MidRow.sprite == RightRow.sprite) matchedSprite = MidRow.sprite;
        else if (LeftRow.sprite == RightRow.sprite) matchedSprite = LeftRow.sprite;

        if (matchedSprite != null)
        {
            int reward = 0;
            if (matchedSprite.name.Contains("Bar")) reward = PairWithBar;
            else if (matchedSprite.name.Contains("Cherry")) reward = PairWithCherry;
            else if (matchedSprite.name.Contains("Seven")) reward = PairWithSeven;
            else if (matchedSprite.name.Contains("Bell")) reward = PairWithBell;

            AddPoints(reward);
            UIManager.Instance.ShowWinningText(false, matchedSprite.name);
            AudioManager.Instance.PairWinAudioTrigger();
        }
        else
        {
            // No win, Show 0 points
            AudioManager.Instance.NoPairAudioTrigger();
            UIManager.Instance.ShowCurrentPoints(0, true);
            UIManager.Instance.ShowWinningText(false, UIManager.Instance.UIText.NoPairKeyValue); // tells ui manager to show no match
        }

        StartCoroutine(OnFinishRolling());
    }

    // Called after reels finish rolling.
    // Resets lever state via LeverPull script.
    IEnumerator OnFinishRolling()
    {
        yield return new WaitForSeconds(NextTurnDelay);
        LeverPull leverPull = GetComponent<LeverPull>();
        leverPull.OnLeverReleased();
    }

    // Adds points to total and updates UI accordingly.
    private void AddPoints(int amount)
    {
        TotalPoints += amount;
        UIManager.Instance.UpdateTotalPoints(TotalPoints);
        UIManager.Instance.ShowCurrentPoints(amount, true);
    }
}

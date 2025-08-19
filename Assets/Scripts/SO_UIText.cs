using UnityEngine;

[CreateAssetMenu(fileName = "New UI Text", menuName = "Slot Machine/UI Text", order = 1)]
public class SO_UIText : ScriptableObject
{
    public string StartTips = "Pull the lever to start!\nor press Space";
    public string JackpotText = "JACKPOT\n";
    public string PairText = "You won with a pair!\n";
    public string NoPairText = "No match found.\nTry again!";
    public string NoPairKeyValue = "No match";
}

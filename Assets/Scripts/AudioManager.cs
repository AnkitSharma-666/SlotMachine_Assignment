using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _BGM;
    [SerializeField] private AudioSource _jackpotAudio;
    [SerializeField] private AudioSource _rollingAudio;
    [SerializeField] private AudioSource _pairWinAudio;
    [SerializeField] private AudioSource _noPairAudio;

    #region Singleton
    public static AudioManager Instance { get; private set;}
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    // Stop other Audios on start except BGM
    private void Start()
    {
        StopAudios();
    }

    private void StopAudios()
    {
        _jackpotAudio.Stop();
        _rollingAudio.Stop();
        _pairWinAudio.Stop();
        _noPairAudio.Stop();
    }

    // plyas loose audio when there are no pairs
    public void NoPairAudioTrigger()
    {
        if (!_noPairAudio.isPlaying)
        {
            _noPairAudio.Play();
        }
    }

    // Plays audio when won by a pair
    public void PairWinAudioTrigger()
    {
        if (!_pairWinAudio.isPlaying) _pairWinAudio.Play();
        return;
    }

    // Play rolling audio
    public void RollingSlotsAudioStart()
    {
        StopAudios();
        if (_rollingAudio.isPlaying) { return; }
        _rollingAudio.Play();
    }

    // Stop rolling audio
    public void RollingSlotAudioStop()
    {
        if (!_rollingAudio.isPlaying) { return; }
        _rollingAudio.Stop();
    }

    // play jackpot audio
    public void JackpotAudioTrigger()
    {
        if (_jackpotAudio.isPlaying) { return; }
        _jackpotAudio.Play();
    }

    // Contol mute toggle
    public void AudioControl(bool mute)
    {
        if (!mute) 
        {
            _BGM.mute = false;
            _rollingAudio.mute = false;
            _noPairAudio.mute = false;
            _pairWinAudio.mute = false;
            _jackpotAudio.mute = false;
        }
        else
        {
            _BGM.mute = true;
            _rollingAudio.mute = true;
            _noPairAudio.mute = true;
            _pairWinAudio.mute = true;
            _jackpotAudio.mute = true;
        }
    }
}

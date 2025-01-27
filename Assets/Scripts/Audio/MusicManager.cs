using Unity.VisualScripting;
using UnityEngine;

public class MusicManager
{
    private AudioSource _musicSource;
    private float _musicVolume = 1f;
    private string _currentTrackName = "";
    
    private bool _isPlaying = true;
    public bool IsPlaying => _isPlaying;

    public MusicManager(AudioSource source) => _musicSource = source;

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.clip == clip) return;

        _musicSource.clip = clip;
        _musicSource.Play();
        _currentTrackName = clip.name;
        _isPlaying = true;
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
        _isPlaying = false;
    }

    public void ResumeMusic()
    {
        _musicSource.UnPause();
        _isPlaying = true;
    }

    public void StopMusic()
    {
        _musicSource.Stop();
        _currentTrackName = "";
        _isPlaying = false;
    }

    public void SwitchPlayingMusic()
    {
        if (_isPlaying)
            PauseMusic();
        else
            ResumeMusic();
    }

    public void SetVolume(float volume)
    {
        _musicVolume = volume;
        _musicSource.volume = volume;
    }

    public void SaveMusicState()
    {
        PlayerPrefs.SetString("CurrentTrack", _currentTrackName);
        PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
        PlayerPrefs.SetFloat("MusicTime", _musicSource.time);
        PlayerPrefs.Save();
    }

    public void LoadMusicState()
    {
        _currentTrackName = PlayerPrefs.GetString("CurrentTrack", "");
        _musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float musicTime = PlayerPrefs.GetFloat("MusicTime", 0f);

        if (!string.IsNullOrEmpty(_currentTrackName))
        {
            AudioClip clip = Resources.Load<AudioClip>($"Music/{_currentTrackName}");
            if (clip != null)
            {
                PlayMusic(clip);
                _musicSource.time = musicTime;
            }
        }

        SetVolume(_musicVolume);
    }
}
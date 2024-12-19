using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    [SerializeField] private Button muteButton;
    [SerializeField] private AudioSource backgroundMusicAudioSource;

    private bool muted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        backgroundMusicAudioSource.mute = muted; // Mute only the background music
    }

    public void OnButtonPress()
    {
        muted = !muted;
        backgroundMusicAudioSource.mute = muted; // Toggle mute for background music only
        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        soundOnIcon.enabled = !muted;
        soundOffIcon.enabled = muted;
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
           
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}

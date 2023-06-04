using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettingsButton : MonoBehaviour
{
    public void ToggleMusic()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToggleMusic();
        }
    }
    public void ToggleSoundFX()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToggleSoundFX();
        }
    }

}

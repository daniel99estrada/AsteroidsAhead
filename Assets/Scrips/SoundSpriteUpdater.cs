using UnityEngine;
using UnityEngine.UI;

public class SoundSpriteUpdater : MonoBehaviour
{
    public Sprite enabledSprite; // Reference to the sprite when sound effects are enabled
    public Sprite disabledSprite; // Reference to the sprite when sound effects are disabled
    public Image imageComponentMusic;
    public Image imageComponentSoundFX; // Reference to the image component

    public static SoundSpriteUpdater Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateSoundSprite(bool enabled)
    {
        if (enabled)
        {
            imageComponentSoundFX.sprite = enabledSprite;
        }
        else
        {
            imageComponentSoundFX.sprite = disabledSprite;
        }
    }

    public void UpdateMusicSprite(bool enabled)
    {
        if (enabled)
        {
            imageComponentMusic.sprite = enabledSprite;
        }
        else
        {
            imageComponentMusic.sprite = disabledSprite;
        }
    }
}

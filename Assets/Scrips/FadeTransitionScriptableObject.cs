using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Fade", menuName = "Scene Transition/Fade")]
public class FadeTransitionScriptableObject : AbstractSceneTransitionScriptableObject
{   
    public Color fadeToColor;

    public override IEnumerator Enter(Canvas Parent)
    {   
        float time = 0;
        Color startColor = fadeToColor;
        Color endColor = new Color(fadeToColor.r, fadeToColor.g, fadeToColor.b, 0);

        while (time < 1)
        {
            AnimatedObject.color = Color.Lerp(startColor, endColor, time);
            yield return null;
            time += Time.deltaTime / AnimationTime;
        }

        Destroy(AnimatedObject.gameObject);
    }

    public override IEnumerator Exit(Canvas Parent)
    {
        AnimatedObject = CreateImage(Parent);
        AnimatedObject.rectTransform.anchorMin = Vector2.zero;
        AnimatedObject.rectTransform.anchorMax = Vector2.one;
        AnimatedObject.rectTransform.sizeDelta = Vector2.zero;

        float time = 0;
        Color endColor = fadeToColor;
        Color startColor = new Color(fadeToColor.r, fadeToColor.g, fadeToColor.b, 0);
    
        while (time < 1)
        {
            AnimatedObject.color = Color.Lerp(startColor, endColor, time);
            yield return null;
            time += Time.deltaTime / AnimationTime;
        }
    }   
}

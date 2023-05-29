using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEnabler : MonoBehaviour
{
    private int LEVEL; 
    public LevelScriptableObject levelSO;

    private void Start()
    {
        LEVEL = levelSO.highestLevelCompleted;
        SetGrayRecursive(transform);
        
    }

    private void SetGrayRecursive(Transform parent)
    {
        // Set the color of all child objects' image components and TextMeshPro text UI to gray if the level is above LEVEL
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            int childLevel = i + 1; // increase the child level by 1

            // Only process the child if its level is less than or equal to LEVEL
            if (childLevel > LEVEL + 1)
            {   
                // Set the color of the child's image component to gray if its level is above LEVEL
                var childImage = child.GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.color = Color.gray;
                }

                // Set the color of the child's TextMeshPro text UI to gray if its level is above LEVEL
                var grandChild = child.GetChild(0);
                var textMeshPro = grandChild.GetComponent<TextMeshProUGUI>();
                if (textMeshPro != null)
                {
                    textMeshPro.color = Color.gray;
                }

                // Disable the child's Button component if its level is above LEVEL
                var button = child.GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = false;
                }
            }
        }
    }
}


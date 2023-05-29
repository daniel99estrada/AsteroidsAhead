using UnityEngine;
using System.Collections.Generic;

public class ChangeMaterials : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material[] materials;
    public List<Color> originalMaterials = new List<Color>();

    public Material onShotMaterial; // the material to use when the object is shot
    public float hitDuration = 0.1f; // how long the object should be the onShotMaterial when hit

    void Start()
    {   
        meshRenderer = GetComponent<MeshRenderer>();
        materials = meshRenderer.materials;

        // save the original materials
        for (int i = 0; i < materials.Length; i++)
        {
            Material material = materials[i];
            originalMaterials.Add(material.color);
        }

        onShotMaterial = new Material(Shader.Find("Standard"));
        // Set the Albedo color to pure white (#FFFFFF)
        onShotMaterial.color = Color.white;

        GetComponent<Health>().OnHealthChanged += ChangeAllMaterials;

    }

    void ChangeAllMaterials(float f)
    {
        // temporarily change the materials to the onShotMaterial
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = Color.white;
        }
        // restore the materials after a delay
        Invoke("RestoreOriginalMaterials", hitDuration);  
    }

    void RestoreOriginalMaterials()
    {
        // restore the original materials
        for (int i = 0; i < materials.Length; i++)
        {   
            materials[i].color = originalMaterials[i];
        }
    }
}

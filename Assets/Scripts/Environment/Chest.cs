using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestGraphics;

    private MeshRenderer meshRenderer;
    private Material chestPowerMaterial;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = chestGraphics.GetComponent<MeshRenderer>();
        chestPowerMaterial = meshRenderer.materials[2];
    }

    public void ActivateChest()
    {
        chestPowerMaterial.SetColor("_EmissionColor", Color.cyan * Mathf.LinearToGammaSpace(20f));

    }

    public void DeactivateChest()
    {
        chestPowerMaterial.SetColor("_EmissionColor", Color.black);
    }
}

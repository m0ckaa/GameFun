using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGlow : MonoBehaviour
{
    public Material normalMat;
    public Material glowMat;
    public Renderer tileRenderer;


    void Start()
    {
        tileRenderer = GetComponent<Renderer>();

        if(tileRenderer != null)
        {
            tileRenderer.material = normalMat;
        }
    }

    public void SetSelected(bool isSelected)
    {
        if(tileRenderer != null)
        {
            tileRenderer.material = isSelected ? glowMat : normalMat;
        }
    }
}

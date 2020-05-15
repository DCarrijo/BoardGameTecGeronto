using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public bool IsTileStart = false;
    public bool IsFinalTile = false;

    private Categories _tileCategorie;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        
        if (IsTileStart && IsFinalTile)
        {
            Debug.LogError("TileCannotBeFirstAndLast");
        }
    }

    public Question GetQuestion()
    {
        
    }

    public void SetTile(Categories cat, Material materialTile, Material materialGlow)
    {
        _tileCategorie = cat;
        _meshRenderer.materials = new[] {materialGlow, materialTile};
        IsTileStart = false;
        IsFinalTile = false;
    }

    public void SetFirstTile(Material materialTile, Material materialGlow)
    {
        IsTileStart = true;
        IsFinalTile = false;
        _meshRenderer.materials = new[] {materialGlow, materialTile};
    }

    public void SetLastTile(Material materialTile, Material materialGlow)
    {
        IsTileStart = false;
        IsFinalTile = true;
        _meshRenderer.materials = new[] {materialGlow, materialTile};
    }
}

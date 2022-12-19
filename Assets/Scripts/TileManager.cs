using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] Tilemap interactableMap;
    [SerializeField] Tile hiddenInteractableTile;
    [SerializeField] Tile interactedTile;
    [SerializeField] Tilemap highlightMap;
    [SerializeField] Tile hiddenHighlightTile;
    [SerializeField] Tile highlightTile;

    Vector3Int currentPos = Vector3Int.zero;

    void Start()
    {
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null && tile.name == "interactable_visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }

        foreach (var position in highlightMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = highlightMap.GetTile(position);
            if (tile != null && tile.name == "Highlighted")
            {
                highlightMap.SetTile(position, hiddenHighlightTile);
            }
        }

        foreach (var position in GameManager.instance.data.interactedTiles)
        {
            interactableMap.SetTile(position, interactedTile);
        }
    }

    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);

        if (tile != null)
        {
            if (tile.name == "interactable")
            {
                return true;
            }
        }

        return false;
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);
        GameManager.instance.data.interactedTiles.Add(position);
        GameManager.instance.Save();
    }

    public void ResetInteractedTile(Vector3Int position)
    {
        interactableMap.SetTile(position, hiddenInteractableTile);
        GameManager.instance.data.interactedTiles.Remove(position);
        GameManager.instance.Save();
    }

    public void SetHighlight(Vector3Int position, bool isOn)
    {
        if(!isOn)
        {
            highlightMap.SetTile(currentPos, hiddenHighlightTile);
            return;
        }
        highlightMap.SetTile(currentPos, hiddenHighlightTile);

        currentPos = position;
        highlightMap.SetTile(currentPos, highlightTile);
    }

    public bool IsHighlighted(Vector3Int position)
    {
        if(highlightMap.GetTile(position) == highlightTile)
        {
            return true;
        }

        return false;
    }
}

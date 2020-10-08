using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsVisited = false;
    public bool IsCovered;
    public Sprite CoveredSprite;
    public Sprite FlaggedSprite;
    public TileType TileType;
    public TileState TileState;

    private Sprite defaultSprite;


    void Start()
    {
        CoveredTile();
    }

    public void CoveredTile()
    {
        IsCovered = true;
        TileState = TileState.Normal;
        defaultSprite = GetComponent<SpriteRenderer>().sprite;

        GetComponent<SpriteRenderer>().sprite = CoveredSprite;
    }

    public void RevealedTile()
    {
        IsCovered = false;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

    public void ToggleFlagTileState()
    {
        if(IsCovered && TileState == TileState.Normal)
        {
            TileState = TileState.Flagged;
            GetComponent<SpriteRenderer>().sprite = FlaggedSprite;
        }
        else if(IsCovered && TileState == TileState.Flagged)
        {
            TileState = TileState.Normal;
            GetComponent<SpriteRenderer>().sprite = CoveredSprite;
        }
    }

}


public enum TileType
{
    None,
    Blank,
    Mine,
    Clue
}


public enum TileState
{
    None,
    Normal,
    Flagged
}

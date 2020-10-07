using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsCovered = true;
    public Sprite CoveredSprite;
    public TileType TileType;
    private Sprite defaultSprite;

    void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;

        //GetComponent<SpriteRenderer>().sprite = CoveredSprite;
    }

    public void RevealedTile()
    {
        IsCovered = false;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

}


public enum TileType
{
    None,
    Blank,
    Mine,
    Clue
}

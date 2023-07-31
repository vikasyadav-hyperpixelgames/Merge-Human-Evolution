using UnityEngine;

public class Tile : MonoBehaviour
{
    public void markAnimEnd() => GetComponentInChildren<TilePanel>().merged = true;
}

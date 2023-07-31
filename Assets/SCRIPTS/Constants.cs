using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static readonly List<long> TilePrice = new List<long> { 0, 0, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000, 10000000000, 100000000000 };
}

public enum TileObjects
{
    NULL,
    DraggableImage,
    Gift,
}

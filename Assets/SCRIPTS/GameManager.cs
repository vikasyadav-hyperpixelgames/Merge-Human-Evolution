using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static long Coins
    {
        get
        {
            if (PlayerPrefs.HasKey("Coins"))
            {
                return long.Parse(PlayerPrefs.GetString("Coins"));
            }
            else return 0;
        }
        set
        {
            PlayerPrefs.SetString("Coins", value.ToString());
        }
    }
    public static TileObjects checkTileObject(int id)
    {
        return (TileObjects)(PlayerPrefs.GetInt("TileObject" + id)); //0,1,2
    }
    public static int getTileImageLevel(int id)
    {
        return (int)PlayerPrefs.GetInt("TileImage" + id);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

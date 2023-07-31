using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilesParent : MonoBehaviour
{
    public List<TileState> tileStates = new List<TileState>(12);
    public List<TilePanel> tilePanels = new List<TilePanel>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void isDraggingStart()
    {
        for (int i = 0; i < tileStates.Count; i++)
        {
            if (tileStates[i] == TileState.Occupied)
            {
                tilePanels[i].draggableImage.GetComponent<Image>().raycastTarget = false;
            }
        }
    }
    public void isDraggingEnd()
    {
        for (int i = 0; i < tileStates.Count; i++)
        {
            if (tileStates[i] == TileState.Occupied)
            {
                tilePanels[i].draggableImage.GetComponent<Image>().raycastTarget = true;
            }
        }
    }

    public void spawnImage(int level)
    {
        int i = 0;
        while (i < tilePanels.Count)
        {
            if (tileStates[i] == TileState.Empty)
            {
                break;
            }
            i++;
        }
        if (i >= tilePanels.Count) return;
        TilePanel tilePanel = tilePanels[i];
        GameObject obj = Instantiate(PrefabReference.Instance.HumanImages[level], tilePanel.imagePar);
        DraggableImage dragImg = obj.GetComponent<DraggableImage>();
        dragImg.parTile = tilePanel;
        tilePanel.draggableImage = dragImg;
    }
}
public enum TileState
{
    Locked,
    Empty,
    Occupied
}
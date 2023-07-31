using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class TilePanel : MonoBehaviour, IDropHandler
{
    public TileObjects tileObject;
    public int TileID;
    public Animator anim;
    public Transform TileLevelPar;
    public Text TileLevelText;
    public DraggableImage draggableImage;
    public Transform imagePar, tempImagePar;
    Image shadow;
    public TilesParent tilesParent;
    public Tile ParTile;
    public GameObject LockedObject;
    private void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        tilesParent = GetComponentInParent<TilesParent>();
        TileLevelText = TileLevelPar.GetComponentInChildren<Text>();
        shadow = imagePar.GetComponent<Image>();

        if (!PlayerPrefs.HasKey("Tile" + TileID))
        {
            tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] = TileState.Locked;
        }
        else
        {
            if (GameManager.checkTileObject(TileID) == TileObjects.DraggableImage)
            {
                spawnImage((int)GameManager.getTileImageLevel(TileID));
                tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] = TileState.Occupied;
            }
            else if (GameManager.checkTileObject(TileID) == TileObjects.Gift)
            {
                // TODO gift spawn
                tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] = TileState.Occupied;
                draggableImage = null;
            }
            else
            {
                draggableImage = null;
                tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] = TileState.Empty;
            }
        }
        ParTile = transform.parent.GetComponent<Tile>();
    }

    private void Update()
    {
        if (shadow.enabled != (draggableImage != null))
        {
            shadow.enabled = (draggableImage != null);
            TileLevelPar.gameObject.SetActive((draggableImage != null));
        }
        if (draggableImage)
        {
            TileLevelText.text = draggableImage.level.ToString();
        }
        if (LockedObject.activeInHierarchy != (tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] != TileState.Locked))
        {
            LockedObject.SetActive(tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] != TileState.Locked);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableImage draggableImage = eventData.pointerDrag.GetComponent<DraggableImage>();

        if (draggableImage != null)
        {
            if (imagePar.childCount == 0 && tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] == TileState.Empty)
            {
                // If the tile is empty, change the parent of the draggable image
                draggableImage.transform.SetParent(imagePar);
                draggableImage.resetLocal(false);
                draggableImage.parTile = this;
                TileLevelPar.gameObject.SetActive(true);
                this.draggableImage = draggableImage;
                tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] = TileState.Occupied;
            }
            else if (tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] == TileState.Occupied && GameManager.checkTileObject(TileID) == TileObjects.DraggableImage)
            {
                // If the tile is occupied, merge or swap the images
                MergeOrSwapImages(this.draggableImage, draggableImage);
            }
        }
        tilesParent.isDraggingEnd();
    }

    private void MergeOrSwapImages(DraggableImage curImage, DraggableImage newImage)
    {
        if (curImage.level != newImage.level)
        {
            // Change the parent transform of the images
            curImage.transform.SetParent(newImage.parTile.imagePar);
            newImage.transform.SetParent(imagePar);

            // Set the draggableImage reference in the parent tiles correctly
            this.draggableImage = newImage;
            newImage.parTile.draggableImage = curImage;

            TilePanel tempParent = curImage.parTile;
            curImage.parTile = newImage.parTile;
            newImage.parTile = tempParent;

            // Reset the local positions of the images
            curImage.resetLocal(false);
            newImage.resetLocal(false);

            curImage.GetComponent<Image>().raycastTarget = true;
            newImage.GetComponent<Image>().raycastTarget = true;

            tilesParent.tileStates[tilesParent.tilePanels.IndexOf(curImage.parTile)] = TileState.Occupied;
            tilesParent.tileStates[tilesParent.tilePanels.IndexOf(newImage.parTile)] = TileState.Occupied;
        }
        else
        {
            StartCoroutine(merge(curImage, newImage));
        }
    }
    public bool merged = true;
    public IEnumerator merge(DraggableImage curImage, DraggableImage newImage)
    {
        merged = false;
        newImage.transform.parent = tempImagePar;
        newImage.resetLocal(false);
        anim.enabled = true;
        yield return new WaitUntil(() => merged);
        anim.enabled = false;
        GameObject obj = Instantiate(PrefabReference.Instance.HumanImages[curImage.level], imagePar);
        DraggableImage dragImg = obj.GetComponent<DraggableImage>();
        dragImg.parTile = this;
        draggableImage = dragImg;
        Destroy(curImage.gameObject);
        Destroy(newImage.gameObject);
    }
    public void spawnImage(int level)
    {
        int i = tilesParent.tilePanels.IndexOf(this);
        GameObject obj = Instantiate(PrefabReference.Instance.HumanImages[level], imagePar);
        DraggableImage dragImg = obj.GetComponent<DraggableImage>();
        dragImg.parTile = this;
        draggableImage = dragImg;
    }

    public void UnlockTile()
    {
        if (Constants.TilePrice[TileID] <= GameManager.Coins)
        {
            GameManager.Coins -= Constants.TilePrice[TileID];
            tilesParent.tileStates[tilesParent.tilePanels.IndexOf(this)] = TileState.Empty;
        }
    }
}

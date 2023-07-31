using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int level;
    public TilePanel parTile;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    public Vector3 localAnchoredPos, localRot, localScale;
    MainCanvasController mainCanvasController;
    TilesParent tilesParent;

    private void Awake()
    {
        tilesParent = GetComponentInParent<TilesParent>();
        mainCanvasController = GameObject.FindObjectOfType<MainCanvasController>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); // Get the Canvas component in the parent hierarchy
        canvasGroup = GetComponent<CanvasGroup>();
        localAnchoredPos = rectTransform.anchoredPosition;
        localRot = transform.localEulerAngles;
        localScale = transform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        tilesParent.isDraggingStart();
        canvasGroup.alpha = 0.8f; // Reduce the transparency of the image while dragging
        canvasGroup.blocksRaycasts = false; // Allow raycasts to go through the image
        originalParent = transform.parent;
        parTile.draggableImage = null;
        tilesParent.tileStates[tilesParent.tilePanels.IndexOf(parTile)] = TileState.Empty;
        transform.SetParent(mainCanvasController.transform); // Detach the image from the tile panel 
        // sar / bring in front 
    }

    public void OnDrag(
        PointerEventData eventData)
    {
        // Calculate the position in the Canvas space by dividing with the canvas scale factor
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        // tilesParent.isDraggingEnd();
        canvasGroup.alpha = 1f; // Restore the image's transparency after dragging
        canvasGroup.blocksRaycasts = true; // Enable raycasts again

        if (transform.parent == mainCanvasController.transform)
        {
            // If the image is dropped back to its original parent, reset its position
            tilesParent.isDraggingEnd();
            parTile.draggableImage = this;
            tilesParent.tileStates[tilesParent.tilePanels.IndexOf(parTile)] = TileState.Occupied;
            resetLocal(true);
        }
    }
    public void resetLocal(bool samePar = false)
    {
        RectTransform rect = GetComponent<RectTransform>();
        if (samePar) rect.SetParent(originalParent);
        rect.anchoredPosition = localAnchoredPos;
        rect.localEulerAngles = localRot;
        rect.localScale = localScale;
    }
}
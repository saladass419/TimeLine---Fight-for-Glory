using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Plane plane = new Plane(Vector3.up, -1.5f);
    private Camera _cam;
    private GameObject objectHoveredOver;
    private GameObject objectBeingDragged;
    private Vector3 worldPos;
    private bool inOnAndDrag = false;
    Board board = null;
    [SerializeField] Tile currentHighlightedTile = null;
    private Vector3 oldPosition;

    void Awake()
    {
        _cam = Camera.main;
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
    }

    public GameObject ObjectHoveredOver { get => objectHoveredOver; set => objectHoveredOver = value; }
    public GameObject ObjectBeingDragged { get => objectBeingDragged; set => objectBeingDragged = value; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ObjectHoveredOver = gameObject;
        if (ObjectHoveredOver == null) return;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ObjectHoveredOver = null;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        oldPosition = transform.position;
        inOnAndDrag = false;
        if (ObjectHoveredOver != null)
        {
            ObjectBeingDragged = ObjectHoveredOver;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (ObjectBeingDragged == null || ObjectBeingDragged.GetComponent<NewModel>().OnBoard == true) return;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray,out float dist))
        {
            worldPos = ray.GetPoint(dist);
        }
    }
    private void Update()
    {
        if (ObjectBeingDragged != null && inOnAndDrag == false)
        {
            if(gameObject.GetComponent<NewModel>().OnBoard != true)
            {
                ObjectBeingDragged.transform.position = Vector3.Lerp(ObjectBeingDragged.transform.position, worldPos, 0.05f);
            }
        }
        
        (Tile tile, float minimumDistance) = board.closestTileToObject(gameObject);

        if (currentHighlightedTile != tile && currentHighlightedTile != null)
            board.UnHighlightTile(currentHighlightedTile);

        currentHighlightedTile = tile;
        if(minimumDistance < 2.0f && gameObject.GetComponent<NewModel>().OnBoard != true)
        {
            board.GetComponent<Board>().HighlightTiles(tile, board.GetComponent<Board>().HightLightMaterial);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        (Tile tile, float minimumDistance) = board.closestTileToObject(gameObject);
        if(minimumDistance <2.0f && tile.Occupied == false)
        {
            inOnAndDrag = true;
            gameObject.transform.position = tile.transform.position;
            gameObject.GetComponent<NewModel>().Position = tile.Position;
            tile.PlaceMonster(gameObject);
            gameObject.GetComponent<NewModel>().OnBoard = true;
            tile.Occupied = true;
            board.UnHighlightTile(currentHighlightedTile);
        }
        else
        {
            inOnAndDrag = true;
            board.UnHighlightTile(currentHighlightedTile);
            transform.position = oldPosition;
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    #region Variables
    private Vector3 _dragOffset;
    private Vector3 startingPos;
    [SerializeField] private bool canDrag = true;
    [SerializeField] private float _speed = 100;
    #endregion

    void Awake()
    {
        _cam = Camera.main;
    }
    /*
    public void setDrag(bool val)
    {
        canDrag = val;
    }
    Vector3 GetMousePos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos.x);
        //Debug.Log(mousePos.y);
        //Debug.Log(mousePos.z);
        return mousePos;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startingPos = transform.position;
        _dragOffset = transform.position - GetMousePos();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            Debug.Log("Dragging");
            transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {

    }*/
    private Plane plane = new Plane(Vector3.up, -1.5f);
    private Camera _cam;
    private GameObject objectHoveredOver;
    private GameObject objectBeingDragged;
    private Vector3 worldPos;
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
        if (ObjectHoveredOver != null)
        {
            ObjectBeingDragged = ObjectHoveredOver;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (ObjectBeingDragged == null) return;
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray,out float dist))
        {
            worldPos = ray.GetPoint(dist);
        }
    }
    private void Update()
    {
        if (ObjectBeingDragged != null) ObjectBeingDragged.transform.position = Vector3.Lerp(ObjectBeingDragged.transform.position, worldPos, 0.05f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //DestroyItemBeingDragged();
    }
    public void DestroyItemBeingDragged()
    {
        if (ObjectBeingDragged != null)
            Destroy(ObjectBeingDragged);
        else ObjectBeingDragged = null;
    }
}

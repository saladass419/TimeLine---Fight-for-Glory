using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region Variables
    private Vector3 _dragOffset;
    private Camera _cam;
    private Vector3 startingPos;
    [SerializeField] private bool canDrag = true;
    [SerializeField] private float _speed = 100;
    #endregion

    void Awake()
    {
        _cam = Camera.main;
    }

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
        Debug.Log("hello");
        if (canDrag)
        {
            //Debug.Log("Dragging");
            transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
    }
}

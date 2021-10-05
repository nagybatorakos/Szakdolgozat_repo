using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public RectTransform recttransform;
    [SerializeField]private Canvas canvas;
    public CanvasGroup canvasgroup;
    public bool droppedonslot = false;
    public Vector2 lastpos;

    private void Awake()
    {
        recttransform = GetComponent<RectTransform>();
        canvasgroup = GetComponent<CanvasGroup>();
        lastpos = transform.position;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begindrag");
        //throw new System.NotImplementedException();
        canvasgroup.alpha = 0.6f;
        canvasgroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //itt atirni sima transformra?
        recttransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!droppedonslot)
        {
            transform.position = lastpos;
        }
        else
        {
            Debug.Log("aaaaa");
            droppedonslot = false;
        }
        //throw new System.NotImplementedException();
        canvasgroup.alpha = 1f;
        canvasgroup.blocksRaycasts = true;
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        recttransform = GetComponent<RectTransform>();
        canvasgroup = GetComponent<CanvasGroup>();
        lastpos = transform.position;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        
        //throw new System.NotImplementedException();
    }
}

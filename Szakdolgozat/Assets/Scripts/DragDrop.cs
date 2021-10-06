using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public RectTransform recttransform;
    [SerializeField] private Canvas canvas;
    public CanvasGroup canvasgroup;
    public bool droppedonslot = false;
    public Vector2 lastpos;
    public GameObject parent;

    private void Awake()
    {
        recttransform = GetComponent<RectTransform>();
        canvasgroup = GetComponent<CanvasGroup>();
        lastpos = recttransform.anchoredPosition;
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
        Debug.Log("ondrag");
        //Debug.Log(canvas.scaleFactor);
        //Debug.Log(eventData.delta);
        //throw new System.NotImplementedException();
        //itt atirni sima transformra?

        recttransform.anchoredPosition += eventData.delta /(canvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("dropped");
        if (!droppedonslot)
        {
            recttransform.anchoredPosition = lastpos;
        }
        else
        {

            droppedonslot = false;
        }
        //throw new System.NotImplementedException();
        canvasgroup.alpha = 1f;
        canvasgroup.blocksRaycasts = true;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointerdown");
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        recttransform = GetComponent<RectTransform>();
        canvasgroup = GetComponent<CanvasGroup>();
        lastpos = recttransform.anchoredPosition;
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

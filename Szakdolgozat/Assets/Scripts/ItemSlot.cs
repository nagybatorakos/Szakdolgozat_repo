using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rect;
    public bool hasitem = false;
    public GameObject item;
    public int quantity;
    public Inventory inv;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        inv = GameObject.Find("InvSys").GetComponent<Inventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("called");
        //throw new System.NotImplementedException();
        if (eventData.pointerDrag != null)
        {
            if (hasitem)
            {
                eventData.pointerDrag.GetComponent<DragDrop>().droppedonslot = true;
                //Debug.Log($"{eventData.pointerDrag.GetComponent<DragDrop>().parent.GetComponent<ItemSlot>()}");
                //if (eventData.pointerDrag==item)
                //{

                //}
                inv.Replace(eventData.pointerDrag.GetComponent<DragDrop>().parent.GetComponent<ItemSlot>(), this);
            }
            else
            {
                //Debug.Log($"{eventData.pointerDrag.GetComponent<DragDrop>().parent.GetComponent<ItemSlot>()}");
                eventData.pointerDrag.GetComponent<DragDrop>().droppedonslot = true;
                Debug.Log("dropped");
                inv.Move(eventData.pointerDrag.GetComponent<DragDrop>().parent.GetComponent<ItemSlot>(), this);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<DragDrop>().lastpos = GetComponent<RectTransform>().anchoredPosition;

                //eventData.pointerDrag.GetComponent<Transform>().position = GetComponent<Transform>().position;
                //Debug.Log($"{eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition} = {GetComponent<RectTransform>().anchoredPosition}");

            }
        }
        else
        {
            eventData.pointerDrag.GetComponent<DragDrop>().droppedonslot = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

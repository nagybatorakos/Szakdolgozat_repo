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
                inv.Replace(eventData.pointerDrag.transform.parent.GetComponent<ItemSlot>(), this);
            }
            else
            {
                eventData.pointerDrag.GetComponent<DragDrop>().droppedonslot = true;
                eventData.pointerDrag.GetComponent<DragDrop>().lastpos = GetComponent<Transform>().position;
                Debug.Log("dropped");
                // eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<Transform>().position = GetComponent<Transform>().position;
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

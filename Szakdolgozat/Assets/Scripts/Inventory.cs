using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //public Dictionary<GameObject, string> Slots = new Dictionary<GameObject, string>();
    //public Dictionary<string, int> NameCount = new Dictionary<string, int>();

    //public List<KeyValuePair<GameObject, int>> SlotList = new List<KeyValuePair<GameObject, int>>();

    public GameObject[] inv = new GameObject[9];

    //public ItemSlot slot1;
    //public ItemSlot slot2;
    //public ItemSlot slot3;
    //public ItemSlot slot4;
    //public ItemSlot slot5;
    //public ItemSlot slot6;
    //public ItemSlot slot7;
    //public ItemSlot slot8;
    //public ItemSlot slot9;

    // Start is called before the first frame update
    void Start()
    {
        //Slots.Add(slot1, 0);
        //Slots.Add(slot2, 0);
        //Slots.Add(slot3, 0);
        //Slots.Add(slot4, 0);
        //Slots.Add(slot5, 0);
        //Slots.Add(slot6, 0);
        //Slots.Add(slot7, 0);
        //Slots.Add(slot8, 0);
        //Slots.Add(slot9, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void AddtoSlot(GameObject slot, string id)
    //{
    //    if (Slots.ContainsKey(slot))
    //    {
    //        Slots[slot] = id;
    //    }
    //    else
    //    {
    //        Slots.Add(slot, id);
    //    }

    //}

    public void AddtoInv(GameObject item)
    {

        foreach (GameObject go in inv)
        {
            ItemSlot slot = go.GetComponent<ItemSlot>();
            string itemid = item.name.Split(' ')[0];
            string goid="";

            if (slot.item != null)
            {
                goid = slot.item.name.Split(' ')[0];

                Debug.Log($"{itemid}, {goid}");
            }

            if (!slot.hasitem)
            {
                slot.hasitem = true;
                slot.item = item;
                slot.quantity+=1;
                Snap(item, go);
                break;
            }
            else if (goid == itemid)
            {
                slot.quantity++;
                Destroy(item);
                break;
            }


        }



    }

    private void Snap(GameObject item, GameObject to)
    {
        item.transform.position = to.transform.position;
        item.transform.SetParent(to.transform);
        //item.transform.localScale = new Vector2(1.5f, 1.5f);
        item.GetComponent<SpriteRenderer>().sortingOrder = 200;
        item.SetActive(to.activeSelf);
        //item.AddComponent<DragDrop>();
    }

    public void Replace(ItemSlot from, ItemSlot to)
    {
        from.item.transform.SetParent(to.transform);
        to.item.transform.SetParent(from.transform);
        (from.item.transform.position, to.item.transform.position) = (to.item.transform.position, from.item.transform.position);
        (from.quantity, to.quantity) = (to.quantity, from.quantity);
        (from.item, to.item) = (to.item, from.item);
    }



}

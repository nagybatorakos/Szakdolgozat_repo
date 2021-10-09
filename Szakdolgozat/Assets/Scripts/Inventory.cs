using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    //public Dictionary<GameObject, string> Slots = new Dictionary<GameObject, string>();
    //public Dictionary<string, int> NameCount = new Dictionary<string, int>();

    //public List<KeyValuePair<GameObject, int>> SlotList = new List<KeyValuePair<GameObject, int>>();
    public int coins = 0;

    public GameObject[] inv = new GameObject[9];
    public GameObject Items;
    public Dictionary<string, GameObject> instpref = new Dictionary<string, GameObject>();
    public GameObject[] prefs = new GameObject[3];
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
        instpref.Add("health_potion", prefs[0]);
        instpref.Add("potion_1", prefs[1]);
        instpref.Add("potion_3", prefs[2]);

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
        string itemid = item.name.Split(' ')[0];
        item = Instantiate(instpref[itemid], instpref[itemid].transform.position, instpref[itemid].transform.rotation);

        foreach (GameObject go in inv)
        {
            ItemSlot slot = go.GetComponent<ItemSlot>();
            string goid = "";

            if (slot.item != null)
            {
                goid = slot.item.name.Split('(')[0];
                Debug.Log($"{itemid}, {goid}");
                //Debug.Log($"{itemid}, {goid}, {instpref[goid].name}");



                if (goid == instpref[itemid].name)
                {
                    slot.quantity++;
                    Destroy(item);
                    break;
                }
            }
            else if (!slot.hasitem)
            {
                slot.hasitem = true;
                slot.item = item;
                slot.quantity += 1;
                Snap(item, go);
                break;
            }

        }



    }

    private void Snap(GameObject item, GameObject to)
    {
        item.GetComponent<DragDrop>().parent = to;
        item.transform.SetParent(Items.transform);
        item.GetComponent<RectTransform>().localScale = new Vector3(.4f, .6f, 0);
        item.GetComponent<RectTransform>().anchoredPosition = to.GetComponent<RectTransform>().anchoredPosition;
        //item.transform.position = to.transform.position;
        //item.transform.localScale = new Vector2(1.5f, 1.5f);
        //item.GetComponent<SpriteRenderer>().sortingOrder = 200;
        item.SetActive(to.activeSelf);


    }

    public void Replace(ItemSlot from, ItemSlot to)
    {
        //from.item.transform.SetParent(to.transform);
        //to.item.transform.SetParent(from.transform);
        (from.quantity, to.quantity) = (to.quantity, from.quantity);
        (from.item, to.item) = (to.item, from.item);
        (from.item.GetComponent<DragDrop>().parent, to.item.GetComponent<DragDrop>().parent) = (to.item.GetComponent<DragDrop>().parent, from.item.GetComponent<DragDrop>().parent);
        (from.item.GetComponent<RectTransform>().anchoredPosition, to.item.GetComponent<RectTransform>().anchoredPosition) = (to.item.GetComponent<DragDrop>().lastpos, from.item.GetComponent<DragDrop>().lastpos);
        (to.item.GetComponent<DragDrop>().lastpos, from.item.GetComponent<DragDrop>().lastpos) = (from.item.GetComponent<DragDrop>().lastpos, to.item.GetComponent<DragDrop>().lastpos);

        Debug.Log($"{from.item.GetComponent<DragDrop>().lastpos}, {to.item.GetComponent<DragDrop>().lastpos}");
    }

    public void Move(ItemSlot from, ItemSlot to)
    {
        (from.hasitem, to.hasitem) = (to.hasitem, from.hasitem);

        (from.quantity, to.quantity) = (to.quantity, from.quantity);



        (from.item, to.item) = (null, from.item);


        to.item.GetComponent<DragDrop>().parent = to.gameObject;
        //Debug.Log($"{from.item.GetComponent<DragDrop>().parent}, {to.item.GetComponent<DragDrop>().parent}");

    }

}


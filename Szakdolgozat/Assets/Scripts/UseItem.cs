using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    public Inventory inv;
    public GameObject[] slots = new GameObject[4];
    private Color c;
    // Start is called before the first frame update
    void Start()
    {
        c = slots[0].GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        slotitems();
        whichItem();
    }

    private void slotitems()
    {
        for (int i = 0; i < 4; i++)
        {
            ItemSlot item = inv.inv[i].GetComponent<ItemSlot>();
            if (!item.hasitem)
            {
                slots[i].GetComponent<Image>().sprite = null;
                slots[i].GetComponent<Image>().color = c;
                continue;
            }
            Sprite sprite = item.item.GetComponent<Image>().sprite;
            slots[i].GetComponent<Image>().sprite = sprite;
            slots[i].GetComponent<Image>().color = Color.white;
            //slots[i].GetComponent<Image>().color.a = 1;
        }
    }


    private void whichItem()
    {
        int idx = 0;
        bool pressed = false;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            idx = 0;
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            idx = 1;
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            idx = 2;
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            idx = 3;
            pressed = true;
        }

        if (pressed)
        {
            if (inv.inv[idx].GetComponent<ItemSlot>().hasitem)
            {
                use(inv.inv[idx].GetComponent<ItemSlot>().item.name.Substring(0,6));
                used(inv.inv[idx].GetComponent<ItemSlot>());
            }
            pressed = false;
        }

    }

    private void use(string go)
    {
        Debug.Log(go);
        Player_Controller player = GameObject.Find("Main Camera").GetComponent<Camera_Controller>().player.GetComponent<Player_Controller>();
        StatSys stats = GameObject.Find("InvSys").GetComponent<StatSys>();
        switch (go)
        {
            case "poti_1":

                if (player.maxHealth - player.currentHealth <= 20)
                {
                    player.currentHealth = player.maxHealth;
                }
                else
                {
                    player.currentHealth += 20;
                }

                player.healthBar.SetHealth(player.currentHealth);

                break;

            case "poti_2":
                player.AttackDamage += stats.attack_up;
                break;

            case "poti_3":
                player.MovementSpeed += stats.movesp_up;
                player.Jumpheight += stats.jump_up;
                break;

            case "poti_4":
                player.specialdamage += stats.special_up;
                break;

        }
    }
    private void used(ItemSlot slot)
    {
        if (slot.quantity > 1)
        {
            slot.quantity -= 1;
        }
        else if (slot.quantity == 1)
        {
            Destroy(slot.item);
            slot.quantity = 0;
            slot.item = null;
            slot.hasitem = false;

        }
    }
}

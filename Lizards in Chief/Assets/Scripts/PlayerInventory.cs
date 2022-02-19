using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool showingInventory;
    public int selectedSlot = -1;

    enum InventoryItems { FOUR, SIX, EIGHT }
    [SerializeField]
    InventoryItems amount;
    GameObject inventoryCircle;
    GameObject[] inventorySlots;
    MeshRenderer[] inventoryMeshes;
    GameObject[] inventoryItems;
    Material hover;
    Material equip;
    Material defEmpty;
    Material defFilled;


    // Start is called before the first frame update
    void Start()
    {
        inventoryCircle = GetComponentInChildren<Transform>().gameObject;
        InitializeSlots();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool ShowInventory(float x, float y)
    {
        float amount = Vector3.Magnitude(new Vector3(x, y));

        if (amount < .75)
        {
            amount = 0;
            showingInventory = false;
        }
        if (amount > .75f)
        {
            showingInventory = true;
        }
        if (amount > 1)
        {
            amount = 1;
        }
        inventoryCircle.transform.localScale = Vector3.one * amount;

        if (amount > .75f)
        {
            selectedSlot = PointToItem(x, y);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StoreItem(GameObject item)
    {
        if (selectedSlot != -1 && inventoryItems[selectedSlot] == null)
        {
            inventoryItems[selectedSlot] = item;
            GameObject invItem = Instantiate(item, inventorySlots[selectedSlot].transform);
            inventoryItems[selectedSlot] = invItem;
            invItem.transform.localScale = Vector3.one * .75f;
            invItem.transform.localPosition = new Vector3(invItem.transform.localPosition.x, invItem.transform.localPosition.y, -1);
            inventoryMeshes[selectedSlot].material = defFilled;
        }
    }

    public GameObject EquipItem()
    {
        if (selectedSlot != -1 && inventoryItems[selectedSlot] != null)
        {
            GameObject item = Instantiate(inventoryItems[selectedSlot]);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y, 0);
            inventoryMeshes[selectedSlot].material = defEmpty;
            Destroy(inventoryItems[selectedSlot]);
            return item;
        }
        return null;
    }

    public GameObject SwapItems(GameObject itemFromHands)
    {
        if (selectedSlot != -1)
        {
            GameObject itemFromInv = Instantiate(inventoryItems[selectedSlot]);
            Destroy(inventoryItems[selectedSlot]);
            GameObject itemToInv = Instantiate(itemFromHands, inventorySlots[selectedSlot].transform);
            Destroy(itemFromHands);
            inventoryItems[selectedSlot] = itemToInv;
            itemToInv.transform.localScale = Vector3.one * .75f;
            itemToInv.transform.localPosition = new Vector3(itemToInv.transform.localPosition.x, itemToInv.transform.localPosition.y, -1);
            itemFromInv.transform.localScale = Vector3.one;
            itemFromInv.transform.localPosition = new Vector3(itemFromInv.transform.localPosition.x, itemFromInv.transform.localPosition.y, 0);
            return itemFromInv;
        }
        return null;
    }

    public bool ItemExistsInSlot(int slot)
    {
        if (slot != -1 && inventoryItems[slot] != null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Point to an item in the player's inventory, release to select. 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private int PointToItem(float x, float y)
    {
        //x == 1
        if (x >= .75f)
        {
            if (y <= .25f && y >= -.25f)     //(1,0)
            {   
                return HoverItem(0);
            }
            else if (amount != InventoryItems.FOUR)
            {
                if (y >= .75f)               //(1,1)
                    return HoverItem(5);
                if (amount != InventoryItems.SIX)
                {
                    if (y <= -.75f)              //(1,-1)
                        return HoverItem(7);
                }
            }
        }
        //x == -1
        if (x <= -.75f)
        {
            if (y <= .25f && y >= -.25f)     //(-1,0)
            {
                return HoverItem(2);
            }
            else if (amount != InventoryItems.FOUR)
            {
                if (y >= .75f)               //(-1,1)
                    return HoverItem(4);     
                if (amount != InventoryItems.SIX)
                {
                    if (y <= -.65f)          //(-1,-1)
                        return HoverItem(6);     
                }
            }
        }
        //x ==0
        if (x <= .25f && x >= -.25f)
        {
            if (y >= .75f)               //(0,1)
                return HoverItem(1);
            if (y <= -.65f)              //(0,-1)
                return HoverItem(3);
        }
        return selectedSlot;
    }

    private int HoverItem(int slot)
    {
        DeselectAllItems();
        if (inventorySlots[slot] != null)
        {
            inventorySlots[slot].GetComponent<MeshRenderer>().material = hover;
            return slot;
        }
        return selectedSlot;
    }

    private void DeselectAllItems()
    {
        for (int i = 0; i < inventoryMeshes.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                inventoryMeshes[i].material = defEmpty;
            }
            else
            {
                inventoryMeshes[i].material = defFilled;
            }
        }
        if (selectedSlot != -1)
        {
            inventoryMeshes[selectedSlot].material = equip;
        }
    }

    private void InitializeSlots()
    {
        GameObject Visuals = GetComponentInChildren<Transform>().gameObject;
        inventorySlots = new GameObject[8];
        inventoryMeshes = new MeshRenderer[8];
        inventoryItems = new GameObject[8];

        foreach (Transform slot in Visuals.GetComponentsInChildren<Transform>())
        {
            switch (slot.name)
            {
                case "Slot (1)":
                    inventorySlots[0] = slot.gameObject;
                    inventoryMeshes[0] = slot.GetComponent<MeshRenderer>();
                    break;
                case "Slot (2)":
                    inventorySlots[1] = slot.gameObject;
                    inventoryMeshes[1] = slot.GetComponent<MeshRenderer>();
                    break;
                case "Slot (3)":
                    inventorySlots[2] = slot.gameObject;
                    inventoryMeshes[2] = slot.GetComponent<MeshRenderer>();
                    break;
                case "Slot (4)":
                    inventorySlots[3] = slot.gameObject;
                    inventoryMeshes[3] = slot.GetComponent<MeshRenderer>();
                    break;
                case "Slot (5)":
                    inventorySlots[4] = slot.gameObject;
                    inventoryMeshes[4] = slot.GetComponent<MeshRenderer>();
                    if (amount == InventoryItems.FOUR)
                    {
                        slot.gameObject.SetActive(false);
                    }
                    break;
                case "Slot (6)":
                    inventorySlots[5] = slot.gameObject;
                    inventoryMeshes[5] = slot.GetComponent<MeshRenderer>();
                    if (amount == InventoryItems.FOUR)
                    {
                        slot.gameObject.SetActive(false);
                    }
                    break;
                case "Slot (7)":
                    inventorySlots[6] = slot.gameObject;
                    inventoryMeshes[6] = slot.GetComponent<MeshRenderer>();
                    if (amount == InventoryItems.FOUR || amount == InventoryItems.SIX)
                    {
                        slot.gameObject.SetActive(false);
                    }
                    break;
                case "Slot (8)":
                    inventorySlots[7] = slot.gameObject;
                    inventoryMeshes[7] = slot.GetComponent<MeshRenderer>();
                    if (amount == InventoryItems.FOUR || amount == InventoryItems.SIX)
                    {
                        slot.gameObject.SetActive(false);
                    }
                    break;
            }
        }
        foreach(MeshRenderer dummySlot in GetComponentsInChildren<MeshRenderer>()) {
            if (dummySlot.name == "DummySlot Hover")
            {
                hover = dummySlot.material;
            }
            if (dummySlot.name == "DummySlot Equip")
            {
                equip = dummySlot.material;
            }
            if (dummySlot.name == "DummySlot DefaultEmpty")
            {
                defEmpty = dummySlot.material;
            }
            if (dummySlot.name == "DummySlot DefaultFilled")
            {
                defFilled = dummySlot.material;
            }
        }
    }
}

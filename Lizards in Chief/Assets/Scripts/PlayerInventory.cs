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
    Material defMat;


    // Start is called before the first frame update
    void Start()
    {
        inventoryCircle = GetComponentInChildren<Transform>().gameObject;
        InitializeSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryCircle.transform.localScale.x > 0)
        {
            showingInventory = true;
        }
        else 
        { 
            showingInventory = false;
        }
    }

    public void ShowInventory(float x, float y)
    {
        float amount = Vector3.Magnitude(new Vector3(x, y));

        if (amount < .25)
        {
            amount = 0;
        }
        if (amount > 1)
        {
            amount = 1;
        }
        inventoryCircle.transform.localScale = Vector3.one * amount;

        selectedSlot = PointToItem(x, y);
    }

    public void StoreItem(GameObject item)
    {
        inventoryItems[selectedSlot] = item;
        item.GetComponent<Rigidbody2D>().simulated = false;
        GameObject invItem = Instantiate(item, inventorySlots[selectedSlot].transform);
        invItem.transform.localScale *= .25f;
    }

    /// <summary>
    /// Point to an item in the player's inventory, release to select
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private int PointToItem(float x, float y)
    {
        selectedSlot = -1;
        //x == 1
        if (x >= .75f)
        {
            if (y <= .25f && y >= -.25f)
                return HoverItem(0);
            if (y >= .75f)
                return HoverItem(5);
            if (y <= -.75f)
                return HoverItem(7);
        }
        //x == -1
        if (x <= -.75f)
        {
            if (y <= .25f && y >= -.25f)
                return HoverItem(2);
            if (y >= .75f)
                return HoverItem(4);
            if (y <= -.75f)
                return HoverItem(6);
        }
        //x ==0
        if (x <= .25f && x >= -.25f)
        {
            if (y >= .75f)
                return HoverItem(1);
            if (y <= -.75f)
                return HoverItem(3);
        }
        return -1;
    }

    private int HoverItem(int slot)
    {
        DeselectAllItems();
        if (inventorySlots[slot] != null)
        {
            inventorySlots[slot].GetComponent<MeshRenderer>().material = hover;
            return slot;
        }
        return -1;
    }

    private void DeselectAllItems()
    {
        foreach (MeshRenderer mat in inventoryMeshes)
        {
            if (mat != null)
            {
                mat.material = defMat;
            }
        }
        if (selectedSlot != -1)
        {
            inventoryMeshes[selectedSlot].material = equip;
        }
    }

    private void EquipItem(int item)
    {
        foreach (MeshRenderer mat in inventoryMeshes)
        {
            mat.material = defMat;
        }
        inventorySlots[item].GetComponent<MeshRenderer>().material = equip;
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
            if (dummySlot.name == "DummySlot Default")
            {
                defMat = dummySlot.material;
            }
        }
    }
}

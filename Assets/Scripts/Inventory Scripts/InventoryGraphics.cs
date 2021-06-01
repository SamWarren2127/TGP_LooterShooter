using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGraphics : MonoBehaviour
{

    public GameObject playerInventoryParent;

    InventorySlot[] inventoryUISlots;

    public GameObject playerInventoryUI;

    public CameraController cameraController;
    public WeaponSystem weaponSystem;

    PlayerInventory inventory;
    void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        cameraController = camera.GetComponent<CameraController>();

      
        inventoryUISlots = playerInventoryParent.GetComponentsInChildren<InventorySlot>();

        InvokeRepeating("UpdateUI", 0.5f, 0.5f);

        playerInventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            playerInventoryUI.SetActive(!playerInventoryUI.activeSelf);

            if (!playerInventoryUI.activeSelf)
            {
                  Cursor.lockState = CursorLockMode.Locked;
           
            }
        }
    }

    void UpdateUI()
    {
        if (playerInventoryUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;

            cameraController.enabled = false;

            for (int i = 0; i < inventoryUISlots.Length; i++)//loop through all UI slots
            {
                if (i < inventory.inventoryItems.Count)
                {
                    inventoryUISlots[i].AddItem(inventory.inventoryItems[i]); //add item from inventory to UI slots array;
                }
                else
                {
                    inventoryUISlots[i].RemoveItem();
                }
            }
        }
        else
        {
            cameraController.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}

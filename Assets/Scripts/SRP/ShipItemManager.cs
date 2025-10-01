using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipItemManager : MonoBehaviour
{
    [SerializeField] private Image itemImageHolder;
    [SerializeField] private ShipMovement movement;
    [SerializeField] private ShipShooting shooting;
    [SerializeField] private ShipUIManager uiManager;

    private List<Color> items = new List<Color>();
    private int activeItemIndex = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            PickUpItem(other.gameObject);
        }
    }

    private void Update()
    {
        CycleItems();
        UseItem();
    }

    private void PickUpItem(GameObject item)
    {
        Color color = item.GetComponent<Renderer>().material.color;
        Destroy(item);

        items.Add(color);
        activeItemIndex = items.Count - 1;

        itemImageHolder.color = color;
        itemImageHolder.enabled = true;
    }

    private void CycleItems()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && items.Count > 0)
        {
            activeItemIndex = (activeItemIndex + 1) % items.Count;
            itemImageHolder.color = items[activeItemIndex];
        }
    }

    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.E) && items.Count > 0 && activeItemIndex != -1)
        {
            Color current = items[activeItemIndex];

            if (current == Color.blue)
            {
                uiManager.ShowMessage("+ Move Speed");
                movement.IncreaseMoveSpeed(5);
            }
            else if (current == Color.red)
            {
                uiManager.ShowMessage("+ Fire Rate");
                shooting.IncreaseFireRate(0.1f);
            }
            else if (current == Color.green)
            {
                uiManager.ShowMessage("+ Rotation Speed");
                movement.IncreaseRotationSpeed(10);
            }

            items.RemoveAt(activeItemIndex);

            if (items.Count > 0)
            {
                activeItemIndex = Mathf.Clamp(activeItemIndex - 1, 0, items.Count - 1);
                itemImageHolder.color = items[activeItemIndex];
            }
            else
            {
                itemImageHolder.enabled = false;
                activeItemIndex = -1;
            }
        }
    }
}

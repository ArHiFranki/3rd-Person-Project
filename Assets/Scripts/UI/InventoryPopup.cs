using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private Image[] _itemIcons;
    [SerializeField] private TMP_Text[] _itemLabels;

    [SerializeField] private TMP_Text _currentItemLabel;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _useButton;

    private string _currentItem;
 
    public void Refresh()
    {
        List<string> itemList = Managers.Inventory.GetItemList();

        int lenghtItemIcons = _itemIcons.Length;
        for (int i = 0; i < lenghtItemIcons; i++)
        {
            if (i < itemList.Count)
            {
                _itemIcons[i].gameObject.SetActive(true);
                _itemLabels[i].gameObject.SetActive(true);

                string item = itemList[i];

                Sprite sprite = Resources.Load<Sprite>("Icons/" + item);
                _itemIcons[i].sprite = sprite;
                _itemIcons[i].SetNativeSize();
                _itemIcons[i].rectTransform.localScale = new Vector3(2, 2, 1);

                int count = Managers.Inventory.GetItemCount(item);
                string message = "x" + count;

                if (item == Managers.Inventory.equippedItem)
                {
                    message = "Equipped\n" + message;
                }

                _itemLabels[i].text = message;

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) =>
                {
                    OnItem(item);
                });

                EventTrigger trigger = _itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }
            else
            {
                _itemIcons[i].gameObject.SetActive(false);
                _itemLabels[i].gameObject.SetActive(false);
            }
        }

        if (!itemList.Contains(_currentItem))
        {
            _currentItem = null;
        }

        if (_currentItem == null)
        {
            _currentItemLabel.gameObject.SetActive(false);
            _equipButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
        }
        else
        {
            _currentItemLabel.gameObject.SetActive(true);
            _equipButton.gameObject.SetActive(true);

            if (_currentItem == "health")
            {
                _useButton.gameObject.SetActive(true);
            }
            else
            {
                _useButton.gameObject.SetActive(false);
            }

            _currentItemLabel.text = _currentItem + ":";
        }
    }

    public void OnItem(string item)
    {
        _currentItem = item;
        Refresh();
    }

    public void OnEquip()
    {
        Managers.Inventory.EquipItem(_currentItem);
        Refresh();
    }

    public void OnUse()
    {
        Managers.Inventory.ConsumeItem(_currentItem);
        if (_currentItem == "health")
        {
            Managers.Player.ChangeHealth(25);
        }

        Refresh();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    private void OnGUI()
    {
        int positionX = 10;
        int positionY = 10;
        int width = 100;
        int height = 30;
        int buffer = 10;

        List<string> itemList = Managers.Inventory.GetItemList();

        if (itemList.Count == 0)
        {
            GUI.Box(new Rect(positionX, positionY, width, height), "No Items");
        }

        foreach (string item in itemList)
        {
            int count = Managers.Inventory.GetItemCount(item);
            GUI.Box(new Rect(positionX, positionY, width, height), item + "(" + count + ")");
            positionX += width + buffer;
        }

        string equipped = Managers.Inventory.equippedItem;
        if (equipped != null)
        {
            positionX = Screen.width - (width + buffer);
            Texture2D image = Resources.Load("Icons/" + equipped) as Texture2D;
            GUI.Box(new Rect(positionX, positionY, width, height), new GUIContent("Equipped", image));
        }

        positionX = 10;
        positionY += height + buffer;

        foreach (string item in itemList)
        {
            if (GUI.Button(new Rect(positionX, positionY, width, height), "Equip" + item))
            {
                Managers.Inventory.EquipItem(item);
            }

            if (item == "Health")
            {
                if (GUI.Button(new Rect(positionX, positionY + height + buffer, width, height), "Use Health"))
                {
                    Managers.Inventory.ConsumeItem("Health");
                    Managers.Player.ChangeHealth(25);
                }
            }

            positionX += width + buffer;
        }
    }
}

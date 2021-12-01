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
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item);
            GUI.Box(new Rect(positionX, positionY, width, height), new GUIContent("(" + count + ")", image));
            positionX += width + buffer;
        }
    }
}

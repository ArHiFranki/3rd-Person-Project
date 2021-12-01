using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string _itemName;

    private void OnTriggerEnter(Collider other)
    {
        Managers.Inventory.AddItems(_itemName);
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthLabel;
    [SerializeField] private InventoryPopup _popup;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATE, OnHealthUpdated);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATE, OnHealthUpdated);
    }

    private void Start()
    {
        OnHealthUpdated();

        _popup.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isShowing = _popup.gameObject.activeSelf;
            _popup.gameObject.SetActive(!isShowing);
            _popup.Refresh();
        }
    }

    private void OnHealthUpdated()
    {
        string message = "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
        _healthLabel.text = message;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private GameObject _enemy;
    private float _speed;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(_enemyPrefab);
            _enemy.GetComponent<WarderingAI>().OnSpeedChanged(_speed);
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }
    }

    private void OnSpeedChanged(float value)
    {
        _speed = value;
    }
}

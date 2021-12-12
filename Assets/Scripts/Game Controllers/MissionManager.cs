using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public int currentLevel { get; private set; }
    public int maxLevel { get; private set; }

    private NetwokrService _network;

    public void Startup(NetwokrService service)
    {
        Debug.Log("Mission manager starting...");

        _network = service;

        currentLevel = 0;
        maxLevel = 1;

        status = ManagerStatus.Started;
    }

    public void GoToNext()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            string name = "Level" + currentLevel;
            Debug.Log("Loading " + name);
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.Log("Last level");
        }
    }
}

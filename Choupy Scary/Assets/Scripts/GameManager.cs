using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Attributes")]
    public bool blocPosition = false;
    [Header("Game Status")]
    public bool[] obtainedStatus = new bool[3] {false, false, false};
    public bool PouvoirBetonniere = true; // TODO : Remettre à false !!!

    public static GameManager Instance;

    public bool Interacting = false;

    public GameObject respawnLocation;
    public GameObject respawnCamera;

    public delegate void ResetAction();
    public static event ResetAction ResetPrefabs;

    public int doudouNb = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    public void ResetGame()
    {
        Interacting = false;
        blocPosition = false;
        obtainedStatus[0]=false;
        obtainedStatus[1]=false;
        obtainedStatus[2]=false;
    }

    public int RegisterNewDoudou()
    {
        return ++doudouNb;
    }

    public void TriggerPrefabsReset()
    {
        ResetPrefabs();
    }

    public void ActiveBetonniere() {
        PouvoirBetonniere = true;
    }
}

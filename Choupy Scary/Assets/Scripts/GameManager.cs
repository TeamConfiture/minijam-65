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
    public int PlayerOnTile = -1;

    public static GameManager Instance;

    public bool Interacting = false;

    public GameObject respawnLocation;
    public GameObject respawnCamera;

    public delegate void ResetAction();
    public static event ResetAction ResetPrefabs;

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

    public void ResetGame()
    {
        Interacting = false;
        blocPosition = false;
        obtainedStatus[0]=false;
        obtainedStatus[1]=false;
        obtainedStatus[2]=false;
    }

    public void GetDoudou(int doudouId) {
        Debug.Log("Obtained Doudou !");
        if (doudouId < 1 || doudouId > 3) {
            Debug.Log("Wrong ID !!!");
        } else {
            obtainedStatus[doudouId-1] = true;
        }
    }

    public bool GetDoudouValue(int doudouId) {
        if (doudouId < 1 || doudouId > 3)
            return false;
        return obtainedStatus[doudouId-1];
    }

    public void TriggerPrefabsReset()
    {
        ResetPrefabs();
    }

    public void ActiveBetonniere() {
        PouvoirBetonniere = true;
    }
}

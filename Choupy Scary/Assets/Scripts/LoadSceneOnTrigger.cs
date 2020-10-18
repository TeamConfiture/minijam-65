﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTrigger : MonoBehaviour
{
    public string Scene = "Scenes/MainScene";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GlobalDataHandler.plushNb += GameManager.Instance.doudouNb;
            SceneManager.LoadScene(Scene);
        }
    }
}

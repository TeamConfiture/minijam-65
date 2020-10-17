using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoudouScript : MonoBehaviour
{
    [Header("Attributes")]
    public int doudouId;
    public string scene;
    public GameObject fond;
    public bool transitionning = false;

    GameManager manager = null;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Manages whether the character is on the groud or not
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Destroy(gameObject);
            //Debug.Log("Pick up doudou");
            /*
                transitionning = true;
                StartCoroutine(changeScene());
                transitionning = false;
            */
        }
    }

    IEnumerator changeScene()
    {
        manager.GetDoudou(doudouId);
        //gameObject.GetComponent<ChangeAudio>().enabled = true;
        if (fond != null) {
            fond.SetActive(true);

            yield return new WaitForSeconds(4);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        if (fond != null) {
            fond.SetActive(false);
        }
    }
}
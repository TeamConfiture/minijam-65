using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    public Text endText;
    // Start is called before the first frame update
    void Start()
    {
        endText.text = "Congratulation, you saved " + GlobalDataHandler.plushNb + " plushies ! They are not going to spook anybody else !";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}

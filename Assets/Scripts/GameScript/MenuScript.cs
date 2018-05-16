using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
    public Text linha1;
    public Text linha2;
    public GameObject GControl;
    public GameObject EndMenu;
    public GameController gControlerScript;

    private void Start()
    {
        gControlerScript = GControl.GetComponent<GameController>();
    }
    void Update () {
        linha1.text = string.Format("Player {0} Wins!", gControlerScript.playerWinner);
        if (Input.GetKey(KeyCode.Escape) && gControlerScript.setPause == true)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        if (Input.GetKey(KeyCode.R) && gControlerScript.setPause == true)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
}

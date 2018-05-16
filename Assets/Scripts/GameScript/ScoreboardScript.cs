using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardScript : MonoBehaviour {
    public GameObject GControl;
    public GameController gControlScript;
    public Text Player1;
    public Text Player2;
	void Start () {
        GControl = GameObject.FindGameObjectWithTag("GameController");
        gControlScript = GControl.GetComponent<GameController>();
	}
	
	void Update () {
        Player1.text = string.Format("Player1: {0}|{1}|{2}", gControlScript.pointP1[0], gControlScript.pointP1[1], gControlScript.pointP1[2]);
        Player2.text = string.Format("Player1: {0}|{1}|{2}", gControlScript.pointP2[0], gControlScript.pointP2[1], gControlScript.pointP2[2]);
    }
}

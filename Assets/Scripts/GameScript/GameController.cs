using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Player1prefab;
    public GameObject Player2prefab;
    public GameObject Player1scene;
    public GameObject Player2scene;
    public GameObject EndMenu;
    public GameObject[] Walls;
    public bool destroyWalls = false;
    private Vector3 initialPositionP1;
    private Vector3 initialPositionP2;
    public int[] pointP1;
    public int[] pointP2;
    public int playerWinner;
    public bool setPause = false;
    private bool endGame = false;
    private int indexSomaP1;
    private int indexSomaP2;
    void Start()
    {
        pointP1 = new int[3];
        pointP2 = new int[3];
        Invoke("SpawnPlayers", 0.01f);
    }

    void Update()
    {
        if (endGame)
        {
            if (SomaPontos(pointP1) > SomaPontos(pointP2))
            {
                playerWinner = 1;
            }
            else
            {
                playerWinner = 2;
            }
            Debug.Log(playerWinner);
            setPause = true;
            EndMenu.SetActive(true);
            Time.timeScale = 0;
            endGame = false; //Para não loopar
        }
        Player1scene = GameObject.FindGameObjectWithTag("P1");
        Player2scene = GameObject.FindGameObjectWithTag("P2");
        Walls = GameObject.FindGameObjectsWithTag("Walls");
        if (destroyWalls)
        {
            foreach (GameObject Wall in Walls)
            {
                Destroy(Wall);
            }
            destroyWalls = false;
        }
    }

    public void TriggerDeath() //Bug ocorre aqui quando 2 player morrem ao mesmo tempo.
    {
        Debug.Log(SomaPontos(pointP1));
        Debug.Log(SomaPontos(pointP2));
        if (SomaPontos(pointP1) == 3 | SomaPontos(pointP2) == 3)
        {
            Destroy(Player1scene);
            Destroy(Player2scene);
            endGame = true;
        }
        else if (SomaPontos(pointP1) < 3 || SomaPontos(pointP2) < 3)
        {
            StartCoroutine(Pause(3));
            Destroy(Player1scene);
            Destroy(Player2scene);
            Invoke("SpawnPlayers", 3.0f);
        }
    }

    void SpawnPlayers()
    {
        Instantiate(Player1prefab, Player1prefab.transform.position, Quaternion.identity);
        Instantiate(Player2prefab, Player2prefab.transform.position, Quaternion.identity);
    }

    private IEnumerator Pause(int p)
    {
        Time.timeScale = 0.1f;
        float pauseEndTime = Time.realtimeSinceStartup + p;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
        destroyWalls = true;
    }

    public void AddPoint(string name)
    {
        if (name == "P1" && SomaPontos(pointP1) <= 3)
        {
            pointP2[indexSomaP2] = 1;
            indexSomaP2++;
        }
        else if (name == "P2" && SomaPontos(pointP2) <= 3)
        {
            pointP1[indexSomaP1] = 1;
            indexSomaP1++;
        }
    }
    private int SomaPontos(int[] pontosPlayer)
    {
        int soma = 0;
        foreach (int ponto in pontosPlayer)
        {
            soma += ponto;
        }
        return soma;
    }
}

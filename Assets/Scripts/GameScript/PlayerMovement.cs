using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Selecionar Input no inspector para cada player
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    private Rigidbody2D rb;
    public float speed = 32; //Velocidade Player
    public GameObject wallPrefab;
    private Collider2D wall;
    private Vector2 lastWallEnd;
    private KeyCode LastPressed;
    public GameObject GController;
    public GameController GControllerScript;
    void Start()
    {
        GController = GameObject.FindGameObjectWithTag("GameController");
        GControllerScript = GController.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.tag == "P1")
        {
            rb.velocity = Vector2.up * speed;
        }
        else if(gameObject.tag == "P2")
        {
            rb.velocity = -Vector2.up * speed;
        }
        LastPressed = upKey;
        SpawnWall();
    }

    void Update()
    {
        if (Input.GetKeyDown(upKey) && LastPressed != (downKey))
        {
            rb.velocity = Vector2.up * speed;
            SpawnWall();
            LastPressed = upKey;
        }
        else if (Input.GetKeyDown(downKey) && LastPressed != (upKey))
        {
            rb.velocity = -Vector2.up * speed;
            SpawnWall();
            LastPressed = downKey;
        }
        else if (Input.GetKeyDown(rightKey) && LastPressed != (leftKey))
        {
            rb.velocity = Vector2.right * speed;
            SpawnWall();
            LastPressed = rightKey;
        }
        else if (Input.GetKeyDown(leftKey) && LastPressed != (rightKey))
        {
            rb.velocity = -Vector2.right * speed;
            SpawnWall();
            LastPressed = leftKey;
        }

        FitColliderBetween(wall, lastWallEnd, transform.position);
    }

    void SpawnWall()
    {
        lastWallEnd = transform.position;
        GameObject g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();
    }

    void FitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * 0.5f;
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
        {
            co.transform.localScale = new Vector2(dist + 1, 1);
        }
        else
        {
            co.transform.localScale = new Vector2(1, dist + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co != wall)
        {
            Time.timeScale = 0;
            GControllerScript.AddPoint(gameObject.tag);
            GControllerScript.TriggerDeath();
        }
    }
}

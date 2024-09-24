using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private int win;
    private float movementX;
    private float movementY;

    private float elapsedTime;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText; // UI para exibir o tempo
    public float speed = 0;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.Instance;
    }

    void Start()
    {
        count = 0;
        win = 0;
        elapsedTime = 0; // Inicializa o tempo
        rb = GetComponent<Rigidbody>();
        winText.text = "Victory: " + win.ToString() + "/5";
        SetCountText();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimeText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void SetCountWin()
    {
        winText.text = "Victory: " + win.ToString() + "/5";
        if (win >= 5)
        {
            audioManager.SetFinalTime(elapsedTime);
            audioManager.SetFinalScore(count);
            SceneManager.LoadScene("WinScene");
        }
    }

    void UpdateTimeText()
    {
        timeText.text = "Time: " + elapsedTime.ToString("F2") + "s";
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        if (transform.position.y < -5)
        {
            TriggerDeath();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            audioManager.PlayPoint(audioManager.point);
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Dead"))
        {
            audioManager.PlayPoint(audioManager.hit);
            other.gameObject.SetActive(false);
            TriggerDeath();
        }
        else if (other.gameObject.CompareTag("Win"))
        {
            win = win + 1;
            count = count + 10;
            audioManager.PlayPoint(audioManager.win);
            other.gameObject.SetActive(false);
            SetCountText();
            SetCountWin();
        }
    }

    void TriggerDeath()
    {
        audioManager.PlayDeath();
        audioManager.SetFinalTime(elapsedTime);
        audioManager.SetFinalScore(count);
        SceneManager.LoadScene("GameOver");
    }
}

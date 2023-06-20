using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    
    private Rigidbody rb;
    private int pickupCount;
    private Timer timer;
    private bool gameOver = false;

    [Header("UI")]
    public GameObject inGamePanel;
    public GameObject winPanel;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in out scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pick up").Length;
        //Run the check pickups function
        CheckPickups();
        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
        //turn on our in game panel
        inGamePanel.SetActive(true);
        //turn off our win panel
        winPanel.SetActive(false);

    }
    private void Update()
    {
        timerText.text = timer.GetTime().ToString("F2"); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver == true)
        {
            return;
        }
            

        float moveHorizontal = Input.GetAxis("Horizontal");
       float moveVertical = Input.GetAxis("Vertical");
       Vector3 movement =  new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick up")
        {
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount -= 1;
            //Run the check pickups function
            CheckPickups();
        }
    }

    void CheckPickups()
    {
        //Display the amount of pickups left in our scene
        scoreText.text = "Pickups Left: " + pickupCount;

        if (pickupCount == 0)
        {
            WinGame();
           

        }
    }
    void WinGame()
    {
        //set game over to true
        gameOver = true; 
        //stop the timer
        timer.StopTimer();
        //turn on out win panel
        winPanel.SetActive(true);
        //turn off our in game panel
        inGamePanel.SetActive(false);
        //Display the timer on the win time text
        winTimeText.text = "your time was:" + timer.GetTime().ToString("F2"); 
        print("Yay! You Win, Your time was: " + timer.GetTime());

        //set the velocity of the rigidbody to zero
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }
    //Temporary
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();

    }
    }

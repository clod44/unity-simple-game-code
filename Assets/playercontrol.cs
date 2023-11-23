using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontrol : MonoBehaviour
{
    Rigidbody rb;
    public float jumpPower = 50f;
    public float movementSpeed = 100f;
    public bool isGrounded = false;
    public int score = 0;
    public Canvas gameOverUI;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * movementSpeed;
        float verticalMovement = Input.GetAxis("Vertical") * movementSpeed;

        rb.AddForce(
            new UnityEngine.Vector3(
                horizontalMovement,
                0f,
                verticalMovement
                ) * Time.deltaTime
            );

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new UnityEngine.Vector3(
                0f,
                jumpPower,
                0f
                )
            );
            isGrounded = false;
        }



        if (transform.position.y < -10f)
        {
            gameOverUI.enabled = true;
        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Level2");
        }
    }
}

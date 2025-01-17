using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Pelaajan Rigidbody-komponentti
    private Rigidbody rb;     
    // Tarkistaa, onko pelaaja maassa
    private bool isGrounded = true; 
    // Hyppyvoima
    public float jumpForce = 5f;

    // Pelaajan liikkumisnopeus
    public float speed = 10f; 

    // Alustetaan pelaajan Rigidbody-komponentti
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Päivitetään pelaajan syötteet jokaisella ruudulla
    void Update()
    {
        // Pelaajan liikkuminen WASD- tai nuolinäppäimillä
        float moveHorizontal = Input.GetAxis("Horizontal"); // Liike vasemmalle/oikealle
        float moveVertical = Input.GetAxis("Vertical");     // Liike eteen/taakse

        // Luodaan liikevektori syötteen perusteella
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Lisätään voima pelaajan Rigidbodyyn liikkumista varten
        rb.AddForce(movement * speed);

        // Tarkistetaan, painetaanko välilyöntinäppäintä hyppyä varten
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Estetään useampi hyppy ilmassa
        }
    }

    // Törmäysten tarkistus
    private void OnCollisionEnter(Collision collision)
    {
        // Jos törmätty objekti on "Ground"-tagilla, pelaaja on maassa
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Trigger-alueen tarkistus (esim. kerättävät esineet)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false); // Poista kerättävä esine
        }
    }
}

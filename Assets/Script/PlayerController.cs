using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

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

    // Laskuri, joka kertoo pelaajan saavutetut pisteet
    private int count;

    // Liikkumisen X- ja Y-komponentit
    private float movementX;
    private float movementY;

    // UI-komponentit pisteiden ja voiton näyttämiseen
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    // Alustetaan pelaajan Rigidbody-komponentti ja muut tarvittavat muuttujat
    void Start()
    {
        // Haetaan Rigidbody-komponentti
        rb = GetComponent<Rigidbody>();

        // Alustetaan laskuri nollaksi
        count = 0;

        // Päivitetään laskuri UI:hin
        SetCountText();

        // Piilotetaan voittoteksti alussa
        winTextObject.SetActive(false);
    }

    // Päivitetään pelaajan liikkeet InputSystemin avulla
    public void OnMove(InputValue movementValue)
    {
        // Muutetaan syöte 2D-vektoriksi
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Tallennetaan liikkeen X- ja Y-komponentit
        movementX = movementVector.x;
        movementY = movementVector.y;
        Debug.Log($"Movement Input: X={movementX}, Y={movementY}");

    }

    // Päivitetään fysiikkatoiminnot kiinteällä päivitystaajuudella
    private void FixedUpdate()
    {
        // Luodaan liikevektori X- ja Y-syötteiden perusteella
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Lisätään voima pelaajan Rigidbodyyn liikkumista varten
        rb.AddForce(movement * speed);
    }

    // Tarkistetaan, painetaanko välilyöntinäppäintä hyppyä varten
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Lisätään hyppyvoima ylöspäin
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Estetään useampi hyppy ilmassa
            isGrounded = false;
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

        // Jos törmätty objekti on vihollinen
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Tuhoaa pelaajan objektin
            Destroy(gameObject);

            // Näytetään "Hävisit!"-teksti
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "Hävisit!";
        }
    }

    // Trigger-alueen tarkistus (esim. kerättävät esineet)
    private void OnTriggerEnter(Collider other)
    {
        // Jos törmätty objekti on "PickUp"-tagilla, kerätään se
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Poistetaan kerättävä esine käytöstä
            other.gameObject.SetActive(false);

            // Kasvatetaan laskuria yhdellä
            count += 1;

            // Päivitetään laskurin arvo UI:hin
            SetCountText();
        }
    }

    // Päivitetään laskurin arvo UI:hin ja tarkistetaan voittoehto
    private void SetCountText()
    {
        // Päivitetään laskurin teksti
        countText.text = "Pisteet: " + count.ToString();

        // Jos pelaaja on kerännyt kaikki esineet, näytetään voitto
        if (count >= 12)
        {
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "Voitit!";
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}

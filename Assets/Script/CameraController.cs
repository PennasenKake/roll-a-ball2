using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Etäisyys kameran ja pelaajan välillä
    private Vector3 offset;

    // Start-metodi kutsutaan kerran pelin alussa
    void Start()
    {
        // Tarkistetaan, että pelaaja ei ole null
        if (player != null)
        {
            // Lasketaan alkuperäinen etäisyys kameran ja pelaajan välillä
            offset = transform.position - player.transform.position;
        }
        else
        {
            Debug.LogWarning("Player-objekti on null. Varmista, että se on asetettu Inspectorissa.");
        }
    }

    // LateUpdate-metodi kutsutaan kerran per frame kaikkien Update-funktioiden jälkeen
    void LateUpdate()
    {
        // Tarkistetaan, että pelaaja ei ole null
        if (player != null)
        {
            // Säilytetään sama etäisyys kameran ja pelaajan välillä koko pelin ajan
            transform.position = player.transform.position + offset;
        }
        else
        {
            Debug.LogWarning("Player-objekti on tuhoutunut tai null. Kamera ei voi seurata pelaajaa.");
        }
    }
}

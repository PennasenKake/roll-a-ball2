using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 public GameObject player;

 //etäisyys kameran ja pelaajan välillä
 private Vector3 offset;

 //start-metodi kutsutaan ja pelaajan välillä
 void Start()
    {
        // lasketaan alkuperäinen etäisyys kameran ja pelaajan välillä
        offset = transform.position - player.transform.position; 
    }

// late update-metodi kutsutaan kerran per frame kaikkien update-funkitoiden jälkeen
void LateUpdate()
    {
        // säilytetään sama etäisyys kameran ja pelaajan välillä koko pelinajan
        transform.position = player.transform.position + offset;  
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Pause-valikon CanvasGroup-komponentti
    public CanvasGroup canvasGroup;

    // Tallennetaan alkuperäinen Time.fixedDeltaTime
    private float originalFixedDeltaTime;

    // Alustetaan valikon komponentit
    void Start()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    // Käyttäjän peruutuskomento (Pause)
    void OnCancel()
    {
        Toggle();
    }

    // Palautetaan alkuperäinen aika, kun objekti tuhotaan
    void OnDestroy()
    {
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }

    // Vaihdetaan Pause-tila päälle/pois
    void Toggle()
    {
        if (canvasGroup.alpha < 0.5f)
        {
            // Näytetään pause-valikko ja pysäytetään peli
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0f;
        }
        else
        {
            // Piilotetaan pause-valikko ja jatketaan peliä
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedDeltaTime;
        }
    }
}

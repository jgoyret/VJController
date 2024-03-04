using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraSequence : MonoBehaviour
{
    public Button[] buttons; // Arreglo de botones para interactuar
    public float delayBetweenInteractions = 1f; // Retraso entre interacciones
    public Toggle loopToggle; // Asigna tu Toggle de UI aquí

    private bool continueSequence = false; // Controla si la secuencia sigue ejecutándose

    void Start()
    {
        // Asegúrate de suscribirte al evento del Toggle para cambiar el estado de continueSequence
        loopToggle.onValueChanged.AddListener(delegate { ToggleSequence(loopToggle.isOn); });
    }

    // Método para iniciar o detener la secuencia basado en el estado del Toggle
    void ToggleSequence(bool isOn)
    {
        continueSequence = isOn;
        if (continueSequence)
        {
            StartCoroutine(InteractWithButtonsSequence());
        }
    }

    public void adjustSwitchDelay(float newValue)
    {
        delayBetweenInteractions = newValue;
    }

    // Corrutina para interactuar con cada botón en secuencia con un retraso
    IEnumerator InteractWithButtonsSequence()
    {
        while (continueSequence)
        {
            foreach (var button in buttons)
            {
                // Verifica nuevamente por si el Toggle se desactivó durante la secuencia
                if (!continueSequence) yield break;

                // Simula la interacción con el botón aquí
                button.onClick.Invoke();

                // Espera por el retraso antes de continuar con el próximo botón
                yield return new WaitForSeconds(delayBetweenInteractions);
            }
        }
    }
}

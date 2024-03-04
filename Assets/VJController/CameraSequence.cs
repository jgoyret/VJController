using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraSequence : MonoBehaviour
{
    public Button[] buttons; // Arreglo de botones para interactuar
    public float delayBetweenInteractions = 1f; // Retraso entre interacciones
    public Toggle loopToggle; // Asigna tu Toggle de UI aqu�

    private bool continueSequence = false; // Controla si la secuencia sigue ejecut�ndose

    void Start()
    {
        // Aseg�rate de suscribirte al evento del Toggle para cambiar el estado de continueSequence
        loopToggle.onValueChanged.AddListener(delegate { ToggleSequence(loopToggle.isOn); });
    }

    // M�todo para iniciar o detener la secuencia basado en el estado del Toggle
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

    // Corrutina para interactuar con cada bot�n en secuencia con un retraso
    IEnumerator InteractWithButtonsSequence()
    {
        while (continueSequence)
        {
            foreach (var button in buttons)
            {
                // Verifica nuevamente por si el Toggle se desactiv� durante la secuencia
                if (!continueSequence) yield break;

                // Simula la interacci�n con el bot�n aqu�
                button.onClick.Invoke();

                // Espera por el retraso antes de continuar con el pr�ximo bot�n
                yield return new WaitForSeconds(delayBetweenInteractions);
            }
        }
    }
}

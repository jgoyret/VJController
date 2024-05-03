using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oscToUi : MonoBehaviour
{



    public void ControlOSC(GameObject slider_, float v)
    {
        slider_.GetComponent<Slider>().value = v;

    }
}

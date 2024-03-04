using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineasLocas : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 [] roquitas;
    public float line_ancho;
    void Start()
    {
        roquitas = new Vector3[this.GetComponent<Transform>().childCount]; 
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = roquitas.Length;
        for (int i = 0; i < roquitas.Length; i++)
        {
            roquitas[i] = this.GetComponent<Transform>().GetChild(i).GetComponent<Transform>().position;
        }
        lineRenderer.SetPositions(roquitas);
        lineRenderer.SetWidth(0, line_ancho);
    }
}

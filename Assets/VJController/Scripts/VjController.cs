using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.Rendering;

public class VjController : MonoBehaviour
{
    [Header("POINTS OF INTEREST")]
    public GameObject[] points_of_interest;

    // [SerializeField]
    private Vector3[] positionPOI;


    [Space(10)]
    [Header("MASTERS")]

    public GameObject[] masters;

    // [SerializeField]
    private CinemachineVirtualCamera[] vcameras;
    private CinemachineBrain cinemachineBrain;
    private GameObject[] cameras_object;
    private float switchDelaySequence;

    private Animator[] mastersAnimators;

    [Space(10)]
    [Header("LIGTHS")]

    public Animator ligth_animator;
    public HDAdditionalLightData[] ligths;
    public GameObject dLightOne;
    public GameObject dLightTwo;

    private Boolean dLightSwitch = false;
    private Boolean button_dLightSwitch = false;
    private string modeLigth = "default";

    // [Header("UI ELEMENTS")]
    private TMP_Dropdown[] m_Dropdown = new TMP_Dropdown[3];
    private int[] mDropdown_value = new int[3];
    private Slider[] slider_FOV = new Slider[3];
    private Slider[] speedAnimSlider = new Slider[3];
    private Slider pointsIntesitySlider;
    private Slider dlIntensitySlider;
    private Slider ligthAnimSpeed;
    private Slider timeBlendCameras;
    private TMP_Dropdown[] m_DropdownLookAt = new TMP_Dropdown[3];
    private int[] mDropdownLookAt_value = new int[3];
    private Button[] buttonsSequence = new Button[3]; 




    void Start()
    {

        //INITIAL
        cinemachineBrain = GameObject.Find("Main Camera").GetComponent<CinemachineBrain>();


        // GET POSITIONS OF POINTS OF INTEREST
        positionPOI = new Vector3[points_of_interest.Length];

        //GET CAMERAS 
        vcameras = new CinemachineVirtualCamera[masters.Length];

        for (int i = 0; i < vcameras.Length; i++)
            vcameras[i] = masters[i].transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();

        cameras_object = new GameObject[masters.Length];

        for (int i = 0; i < cameras_object.Length; i++)
            cameras_object[i] = masters[i].transform.GetChild(0).gameObject;


        // GET ANIMATORS 
        mastersAnimators = new Animator[masters.Length];

        for (int i = 0; i < mastersAnimators.Length; i++)
            mastersAnimators[i] = masters[i].GetComponent<Animator>();


        //GET UI COMPONENTS 
        for (int i = 0; i < m_Dropdown.Length; i++)
            m_Dropdown[i] = GameObject.Find("PosM" + (i + 1)).GetComponent<TMP_Dropdown>();

        for (int i = 0; i < slider_FOV.Length; i++)
            slider_FOV[i] = GameObject.Find("SliderFov" + (i + 1)).GetComponent<Slider>();

        for (int i = 0; i < speedAnimSlider.Length; i++)
            speedAnimSlider[i] = GameObject.Find("AnimSpeed" + (i + 1)).GetComponent<Slider>();

        for (int i = 0; i < m_DropdownLookAt.Length; i++)
            m_DropdownLookAt[i] = GameObject.Find("LookAtM" + (i + 1)).GetComponent<TMP_Dropdown>();

        pointsIntesitySlider = GameObject.Find("PointsIntensitySlider").GetComponent<Slider>();
        dlIntensitySlider = GameObject.Find("DLIntensitySlider").GetComponent<Slider>();

        ligthAnimSpeed = GameObject.Find("speedAnimLigth").GetComponent<Slider>();

        timeBlendCameras = GameObject.Find("timeBlendCameras").GetComponent<Slider>();

        buttonsSequence[0] = GameObject.Find("GOtoCAM1").GetComponent<Button>();
        buttonsSequence[1] = GameObject.Find("GOtoCAM2").GetComponent<Button>();
        buttonsSequence[2] = GameObject.Find("GOtoCAM3").GetComponent<Button>();    
   
    }

    void Update()
    {
        for (int i = 0; i < positionPOI.Length; i++)
            positionPOI[i] = points_of_interest[i].GetComponent<Transform>().position;


        for (int i = 0; i < vcameras.Length; i++)
            setFOVcamera(vcameras[i], slider_FOV[i].value);

        for (int i = 0; i < mastersAnimators.Length; i++)
            speedAnimator(mastersAnimators[i], speedAnimSlider[i].value);

        setLigthGralIntensity(modeLigth);
        speedAnimatorLigths();

        // UPDATE UI VALUES
        for (int i = 0; i < m_Dropdown.Length; i++)
            mDropdown_value[i] = m_Dropdown[i].value;

        for (int i = 0; i < m_Dropdown.Length; i++)
            mDropdownLookAt_value[i] = m_DropdownLookAt[i].value;

        // Camera Blend Time
        cinemachineBrain.m_DefaultBlend.m_Time = timeBlendCameras.value;

        // Camera Sequence

    }

    public void setMasterOnePosition()
    {
        switch (mDropdown_value[0])
        {
            case 0:
                masters[0].GetComponent<Transform>().position = positionPOI[0];
                break;

            case 1:
                masters[0].GetComponent<Transform>().position = positionPOI[1];
                break;

            case 2:
                masters[0].GetComponent<Transform>().position = positionPOI[2];
                break;

            case 3:
                masters[0].GetComponent<Transform>().position = positionPOI[3];
                break;

            case 4:
                masters[0].GetComponent<Transform>().position = positionPOI[4];
                break;
        }
    }

    public void setMasterTwoPosition()
    {
        switch (mDropdown_value[1])
        {
            case 0:
                masters[1].GetComponent<Transform>().position = positionPOI[0];
                break;

            case 1:
                masters[1].GetComponent<Transform>().position = positionPOI[1];
                break;

            case 2:
                masters[1].GetComponent<Transform>().position = positionPOI[2];
                break;

            case 3:
                masters[1].GetComponent<Transform>().position = positionPOI[3];
                break;

            case 4:
                masters[1].GetComponent<Transform>().position = positionPOI[4];
                break;
        }
    }

    public void setMasterThreePosition()
    {
        switch (mDropdown_value[2])
        {
            case 0:
                masters[2].GetComponent<Transform>().position = positionPOI[0];
                break;

            case 1:
                masters[2].GetComponent<Transform>().position = positionPOI[1];
                break;

            case 2:
                masters[2].GetComponent<Transform>().position = positionPOI[2];
                break;

            case 3:
                masters[2].GetComponent<Transform>().position = positionPOI[3];
                break;

            case 4:
                masters[2].GetComponent<Transform>().position = positionPOI[4];
                break;
        }
    }

    public void switchCamera(GameObject _camera)
    {
        if (_camera.activeSelf)
        {
            _camera.SetActive(false);
            _camera.SetActive(true);
        }
    }

    public void setFOVcamera(CinemachineVirtualCamera _vcam, float _value)
    {
        _vcam.m_Lens.FieldOfView = _value;
        //Debug.Log("Valor FOV: " + _value + " | " + _vcam);
    }


    public void SetCameraLookAt(int index_Cam)
    {

        switch (mDropdownLookAt_value[index_Cam])
        {
            case 0:
                vcameras[index_Cam].m_LookAt = points_of_interest[0].GetComponent<Transform>();
                break;

            case 1:
                vcameras[index_Cam].m_LookAt = points_of_interest[1].GetComponent<Transform>();
                break;

            case 2:
                vcameras[index_Cam].m_LookAt = points_of_interest[2].GetComponent<Transform>();
                break;

            case 3:
                vcameras[index_Cam].m_LookAt = points_of_interest[3].GetComponent<Transform>();
                break;

            case 4:
                vcameras[index_Cam].m_LookAt = points_of_interest[4].GetComponent<Transform>();
                break;
        }
    }


    public void speedAnimator(Animator _animator, float _value)
    {
        _animator.speed = _value;
    }

    public void changeAnimLigths(int index_anim)
    {
        ligth_animator.SetInteger("ligthAnim", index_anim);
    }

    public void setLigthGralIntensity(string _mode)
    {
        // _mode = modeLigth;
        // bool randomOnce = true;

        if (modeLigth == "default")
        {
            for (int i = 0; i < ligths.Length; i++)
                ligths[i].intensity = pointsIntesitySlider.value;

            dLightOne.GetComponent<Light>().intensity = dlIntensitySlider.value;
            dLightTwo.GetComponent<Light>().intensity = dlIntensitySlider.value * 2;
        }
        else if (modeLigth == "random")
        {
            for (int i = 0; i < ligths.Length; i++)
                ligths[i].intensity = UnityEngine.Random.Range(0f, pointsIntesitySlider.value);

            dLightOne.GetComponent<Light>().intensity = UnityEngine.Random.Range(0f, dlIntensitySlider.value);
            dLightTwo.GetComponent<Light>().intensity = UnityEngine.Random.Range(0f, dlIntensitySlider.value * 2);

        }
    }
    public void modeLigthDefault() { modeLigth = "default"; }
    public void modeLigthRandom() { modeLigth = "random"; }

    public void speedAnimatorLigths()
    {
        ligth_animator.speed = ligthAnimSpeed.value;
    }

    public void enableDLightSwitch()
    {
        dLightSwitch = !dLightSwitch;

        if (!dLightSwitch)
        {
            dLightOne.SetActive(true);
            dLightTwo.SetActive(true);
        }
        else
        {
            setDLightSwitch();
        }
        Debug.Log("enabledSwitch: " + dLightSwitch);
    }

    public void setDLightSwitch()
    {
        if (dLightSwitch)
        {
            button_dLightSwitch = !button_dLightSwitch;
            if (button_dLightSwitch)
            {
                dLightOne.SetActive(true);
                dLightTwo.SetActive(false);
            }
            else
            {
                dLightOne.SetActive(false);
                dLightTwo.SetActive(true);
            }
            Debug.Log("buttonSwitch: " + button_dLightSwitch);
        }
    }

}

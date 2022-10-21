using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ButtonAction : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    private GameObject presser;
    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    private bool isPressed=false;
    [SerializeField]
    private HueLamp _hueLamp;

    [SerializeField]
    private GameObject cube;


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!isPressed)
    //    {
    //        button.transform.localPosition = new Vector3(0, 0.003f, 0);
    //        presser = other.gameObject;
    //        OnPress.Invoke();
    //        isPressed = true;
    //    }
        
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject==presser)
    //    {
    //        button.transform.localPosition = new Vector3(0, 0.015f, 0);
    //        presser = other.gameObject;
    //        OnRelease.Invoke();
    //        isPressed = false;
    //    }
    //}

    public void CubeEvent()
    {
        cube.SetActive(false);
        _hueLamp.on = _hueLamp.on ? false : true;
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        _hueLamp.on = _hueLamp.on ? false : true;
    //    }
    //}

}

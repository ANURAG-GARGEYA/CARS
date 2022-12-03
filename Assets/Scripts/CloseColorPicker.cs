using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseColorPicker : MonoBehaviour
{
    public GameObject ColorPickerPrefabe;
    public GameObject OnButton;
    public GameObject OffButton;
    // Start is called before the first frame update

    public void ColorPickerClose()
    {
        // if (OnButton.activeSelf)
        // {



        OffButton.SetActive(false);
        OnButton.SetActive(true);
        //   }

        //   else
        //  {
        //     OffButton.SetActive(false);
        //      OnButton.SetActive(true);
        // }
       if (ColorPickerPrefabe != null)
       {
            ColorPickerPrefabe.SetActive(false);

        }
        //else
        //{
        //   TriangleColorPicker.SetActive(false);
        // }
        //OffButton.SetActive(true);
    }
}

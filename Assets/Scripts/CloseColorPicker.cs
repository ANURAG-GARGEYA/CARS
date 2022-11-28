using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseColorPicker : MonoBehaviour
{
    public GameObject TriangleColorPicker;
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
        if (TriangleColorPicker != null)
        {
            TriangleColorPicker.SetActive(false);

        }
        //else
        //{
        //   TriangleColorPicker.SetActive(false);
        // }
        //OffButton.SetActive(true);
    }
}

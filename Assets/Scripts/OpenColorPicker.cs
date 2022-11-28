using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenColorPicker : MonoBehaviour
{
    public GameObject TriangleColorPicker;
    public GameObject OnButton;
    public GameObject OffButton;
    // Start is called before the first frame update

    public void ColorPickerOpen()
    {
       // if (OnButton.activeSelf)
       // {



            OnButton.SetActive(false);
            OffButton.SetActive(true);
     //   }

     //   else
      //  {
       //     OffButton.SetActive(false);
      //      OnButton.SetActive(true);
       // }
            if (TriangleColorPicker != null)
            {
                TriangleColorPicker.SetActive(true);

            }
            //else
            //{
             //   TriangleColorPicker.SetActive(false);
           // }
            //OffButton.SetActive(true);
        }
    }



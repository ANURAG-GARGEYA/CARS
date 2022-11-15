using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListCloser : MonoBehaviour
{
    public GameObject List;
    public GameObject OpenMenu;
    public GameObject CloseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CloseList()
    {
        CloseMenu.SetActive(false);
        if (List != null)
        {
            List.SetActive(false);
        }
        OpenMenu.SetActive(true);

    }
}

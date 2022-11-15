using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListOpener : MonoBehaviour
{
    public GameObject List;
    public GameObject CloseMenu;
    public GameObject OpenMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenList()
    {
        OpenMenu.SetActive(false);
        if (List != null)
        {
            List.SetActive(true);
        }
        CloseMenu.SetActive(true);
    }
}

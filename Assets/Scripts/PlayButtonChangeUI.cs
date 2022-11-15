using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonChangeUI : MonoBehaviour
{
    public GameObject PauseButton;
     public GameObject PlayButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeImage()
    {
        PlayButton.SetActive(false);
        PauseButton.SetActive(true);

    }
}

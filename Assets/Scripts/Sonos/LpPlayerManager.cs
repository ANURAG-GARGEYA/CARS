using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LpPlayerManager : MonoBehaviour
{
    public Event OnRecordPlaced;
    [SerializeField]
    private GameObject _mediaPlayerUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Album")
        {
         //Debug.Log(other.GetComponent<AlbumInformationHolder>().AlbumName);
          Debug.Log(other.gameObject.name);
          _mediaPlayerUI.SetActive(true);
            other.gameObject.transform.parent = null;
            Destroy(other.gameObject);
        }    

    }
}


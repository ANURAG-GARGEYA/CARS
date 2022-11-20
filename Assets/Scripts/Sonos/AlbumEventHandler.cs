using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumEventHandler : MonoBehaviour
{
    public class AlbumInHand
    {
        public Transform tranform;
        public GameObject parent;
    }

    public void AlbumHeld(GameObject album)
    {
        album.GetComponent<BoxCollider>().enabled = true;
        //var x = new AlbumInHand();
        //x.parent = album.transform.parent.gameObject;
        //x.tranform.position = album.transform.position;
       // x.tranform.rotation = album.transform.rotation;

        album.transform.parent = null;

        album.transform.GetChild(0).gameObject.SetActive(false);
        album.transform.GetChild(1).gameObject.SetActive(true); 
        
    }



}

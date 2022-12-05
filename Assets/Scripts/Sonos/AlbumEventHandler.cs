using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class AlbumEventHandler : MonoBehaviour
{

    public int AlbumCount;
    public GameObject AlbumPrefab;
    public GameObject Container_Collection;
    public float GapBetweenAlbums = 0;
    public GameObject LPRecord;
    public Transform LPRecordSpawnPoint;
    public string[] AlbumIDs;

    private void Start()
    {
        for (int i = 0; i < AlbumCount; i++)
        {
            GameObject a = GameObject.Instantiate(AlbumPrefab);
            a.transform.parent = Container_Collection.transform;
            a.transform.position = Vector3.zero;
            a.transform.localPosition = new Vector3(0, GapBetweenAlbums, 0);
            GapBetweenAlbums -= 0.141f;
            a.transform.rotation = Quaternion.identity;
            a.GetComponent<AlbumInformationHolder>().LPRecordSpawnPoint = LPRecordSpawnPoint;
            a.GetComponent<AlbumInformationHolder>().AlbumID = AlbumIDs[i % AlbumIDs.Length];
        }
    }




}

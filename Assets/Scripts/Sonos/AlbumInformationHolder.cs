using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class AlbumInformationHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject _lpRecord;
    
    public string AlbumName;
    public string AlbumID;
    public SpriteRenderer AlbumArtSprite;
    [HideInInspector]
    public Transform LPRecordSpawnPoint;

    private void Start()
    {
        AlbumArtSprite = GetComponentInChildren<SpriteRenderer>();
    }
    public void SpawnLp()
    {
        GameObject a = Instantiate(_lpRecord, LPRecordSpawnPoint.position, LPRecordSpawnPoint.rotation);
        a.transform.localScale = LPRecordSpawnPoint.localScale;
    }

}

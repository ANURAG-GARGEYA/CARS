using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LpPlayerManager : MonoBehaviour
{
    public SonosController sonosController;
    public Event OnRecordPlaced;
    [SerializeField]
    private GameObject _mediaPlayerUI;
    [SerializeField]
    private GameObject _spinningRecord;

    void Start()
    {
        _mediaPlayerUI.SetActive(false);
        _spinningRecord.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Album")
        {
            Record newRecord = other.GetComponent<Record>();
            sonosController.AddAlbum(newRecord.albumID);

            _mediaPlayerUI.SetActive(true);
            _spinningRecord.SetActive(true);

            Record currentRecord = _spinningRecord.GetComponent<Record>();
            currentRecord.albumID = newRecord.albumID;
            currentRecord.albumArtSprite.sprite = newRecord.albumArtSprite.sprite;
            currentRecord.SetIsSpinning(true);

            other.gameObject.transform.parent = null;
            Destroy(other.gameObject);

        }

    }
}


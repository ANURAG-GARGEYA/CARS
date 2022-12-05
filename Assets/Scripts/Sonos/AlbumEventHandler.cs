using UnityEngine;
using UnityEngine.UI;

public class AlbumEventHandler : MonoBehaviour
{

    public int AlbumCount;
    public GameObject AlbumPrefab;
    public GameObject Container_Collection;
    public float GapBetweenAlbums = 0;
    public GameObject LPRecord;
    public Transform LPRecordSpawnPoint;
    public string[] AlbumIDs;
    public Sprite[] AlbumArts;

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
            AlbumInformationHolder aInfo = a.GetComponent<AlbumInformationHolder>();
            aInfo.LPRecordSpawnPoint = LPRecordSpawnPoint;
            aInfo.AlbumID = AlbumIDs[i % AlbumIDs.Length];
            aInfo.AlbumArtSprite.sprite = AlbumArts[i % AlbumArts.Length];
        }
    }




}

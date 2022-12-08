using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class Record : MonoBehaviour
{
    public string albumID;
    public SpriteRenderer albumArtSprite;

    private bool _isSpinning = false;


    void Update()
    {
        if (_isSpinning)
            transform.rotation *= Quaternion.Euler(0.0f, 0.0f, -0.18f);
    }

    public void SetIsSpinning(bool isSpinning)
    {
        _isSpinning = isSpinning;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField]
    RectTransform _texture;
    [SerializeField]
    GameObject _SphereTest;
    [SerializeField]
    Texture2D _refSprite;

    public void OnClickPicker()
    {
        SetColor();

    }
    
    private void SetColor()
    {
        Vector3 imagePos = _texture.position;
        float globalPosX = Input.mousePosition.x - imagePos.x;
        float globalPosY = Input.mousePosition.y - imagePos.y;

        int localPosX = (int)(globalPosX * (_refSprite.width / _texture.rect.width));
        int localPosY = (int)(globalPosY * (_refSprite.height / _texture.rect.height));

        Color c = _refSprite.GetPixel(localPosX, localPosY);
        SetActualColor(c);

    }

    void SetActualColor(Color c)
    {
        _SphereTest.GetComponent<MeshRenderer>().material.color = c;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;
using UnityEngine.UI;
using TMPro;

public class QRGenerator : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawInageReciver;
    [SerializeField]
    private TMP_InputField _textInputFeild;

    private Texture2D _storeEncodedTexture; // to render QR code for the image

    // Start is called before the first frame update
    void Start()
    {
        _storeEncodedTexture = new Texture2D(256, 256);
    }

    private Color32[] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public void OnClickEncode()
    {
        EncodeTextToQRCode();
    }

    private void EncodeTextToQRCode()
    {
        string textWrite = string.IsNullOrEmpty(_textInputFeild.text) ? "You should write something" : _textInputFeild.text;

        // will return a array of colour
        Color32[] _convertPixelToTexture = Encode(textWrite, _storeEncodedTexture.width, _storeEncodedTexture.height);
        _storeEncodedTexture.SetPixels32(_convertPixelToTexture);
        _storeEncodedTexture.Apply();

        _rawInageReciver.texture = _storeEncodedTexture;
        print(_storeEncodedTexture.ToString());

    }
}

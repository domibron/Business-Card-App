using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using TMPro;

public class QRScanner : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawImageBackGround;

    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;

    [SerializeField]
    private TextMeshProUGUI _textOut;

    [SerializeField]
    private RectTransform _scanZone;

    private bool _isCamAvalible;
    private WebCamTexture _cameraTexture;


    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
    }


    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            _isCamAvalible = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
            }
        }

        _cameraTexture.Play();
        _rawImageBackGround.texture = _cameraTexture;
        _isCamAvalible = true;
    }

    private void UpdateCameraRender()
    {
        if (_isCamAvalible == false)
        {
            return;
        }

        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = -_cameraTexture.videoRotationAngle;
        _rawImageBackGround.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void OnClickScan()
    {
        Scan();
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);
            if (result != null)
            {
                _textOut.text = result.Text;
            }
            else
            {
                _textOut.text = "failed to read qr code";
            }
        }
        catch
        {
            _textOut.text = "failed in try";
        }
    }
}

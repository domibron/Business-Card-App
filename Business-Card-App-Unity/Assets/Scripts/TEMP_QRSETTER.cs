using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEMP_QRSETTER : MonoBehaviour
{

    private Core core;

    // Start is called before the first frame update
    void Start()
    {
        core = GetComponentInParent<Core>();
    }

    // Update is called once per frame
    void Update()
    {
        if (core.QRCode != null)
        {
            GetComponent<RawImage>().texture = core.QRCode;
        }


    }
}

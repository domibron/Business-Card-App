using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreatorCard : MonoBehaviour
{
    private Core core;
    private QRGenerator qrGenerator;

    [SerializeField]
    private TMP_InputField Name_input;
    [SerializeField]
    private TMP_InputField Email_input;
    [SerializeField]
    private TMP_InputField Phone_input;
    [SerializeField]
    private TMP_InputField Website_input;

    void Awake()
    {
        core = GetComponentInParent<Core>();
        qrGenerator = GetComponent<QRGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadDetails()
    {
        if (!string.IsNullOrEmpty(core.QRText))
        {
            string[] details;
            details = core.QRText.Split("|");

            if (details.Length < 4) return;

            Name_input.text = details[0];
            Email_input.text = details[1];
            Phone_input.text = details[2];
            Website_input.text = details[3];
            //note = details[4];
        }
    }

    public void GenerateCardDetails()
    {
        string Name_input_text = string.IsNullOrEmpty(Name_input.text) ? "john doe" : Name_input.text;
        string Email_input_text = string.IsNullOrEmpty(Email_input.text) ? "johndoe@gmail.com" : Email_input.text;
        string Phone_input_text = string.IsNullOrEmpty(Phone_input.text) ? "+44 10000000000" : Phone_input.text;
        string Website_input_text = string.IsNullOrEmpty(Website_input.text) ? "https://www.johndoe.com/aboutme" : Website_input.text;

        string combination = Name_input_text + "|" + Email_input_text + "|" + Phone_input_text + "|" + Website_input_text;

        qrGenerator.GenQR(combination);
    }
}

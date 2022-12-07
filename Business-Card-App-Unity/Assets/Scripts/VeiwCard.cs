using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VeiwCard : MonoBehaviour
{
    private Core core;

    [SerializeField]
    private TMP_Text Name_text;
    [SerializeField]
    private TMP_Text Email_text;
    [SerializeField]
    private TMP_Text Phone_text;
    [SerializeField]
    private TMP_Text Website_text;

    private string nameofuser;
    private string email;
    private string phone;
    private string website;

    string compareDataString = "Run please";

    void Awake()
    {
        core = GetComponentInParent<Core>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadData());
    }

    private IEnumerator LoadData()
    {
        core.ReadFromFileQRText();
        if (string.Equals(core.QRText, compareDataString) == false)
        {
            ConvertData();

            Name_text.text = "Name: " + nameofuser;
            Email_text.text = "Email: " + email;
            Phone_text.text = "Phone: " + phone;
            Website_text.text = "Website: " + website;

        }


        yield return new WaitForSeconds(1f);
    }

    private void ConvertData()
    {
        if (string.IsNullOrEmpty(core.QRText))
        {
            nameofuser = "John Doe";
            email = "johndoe@gmail.com";
            phone = "+44 10000000000";
            website = "https://www.johndoe.com/aboutme";

            return;
        }

        compareDataString = core.QRText;
        string[] data = core.QRText.Split("|");

        nameofuser = data[0];
        email = data[1];
        phone = data[2];
        website = data[3];
    }
}

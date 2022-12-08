using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card_details : MonoBehaviour
{
    [HideInInspector]
    public string data;

    [SerializeField]
    private TMP_Text Name_text;
    [SerializeField]
    private TMP_Text Email_text;
    [SerializeField]
    private TMP_Text Phone_text;
    [SerializeField]
    private TMP_Text Website_text;

    private string nameString;
    private string email;
    private string phone;
    private string website;

    List<string> details = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(data))
            data = "john doe|johndoe@gmail.com|+44 10000000000|https://www.johndoe.com/aboutme";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setData(string _data)
    {
        data = _data;
        connvertData();

        nameString = "Name: " + details[0];
        email = "Email: " + details[1];
        phone = "Phone: " + details[2];
        website = "Website: " + details[3];

        Name_text.text = nameString;
        Email_text.text = email;
        Phone_text.text = phone;
        Website_text.text = website;
    }

    private void connvertData()
    {
        string[] arrayToConvert = data.Split("|");

        details.Clear();

        foreach (string _data in arrayToConvert)
        {
            details.Add(_data);
        }
    }
}

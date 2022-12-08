using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavedCards : MonoBehaviour
{
    private Core core;

    [SerializeField]
    GameObject PopUp;
    [SerializeField]
    GameObject AddButton;
    [SerializeField]
    GameObject CancelButton;
    [SerializeField]
    GameObject OkButton;

    [SerializeField]
    TMP_Text Name_text;
    [SerializeField]
    TMP_Text Email_text;
    [SerializeField]
    TMP_Text Phone_text;
    [SerializeField]
    TMP_Text Website_text;
    [SerializeField]
    TMP_Text Result_text;

    bool LackOfData;

    List<string> dataList = new List<string>();

    void Awake()
    {
        core = GetComponentInParent<Core>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AddCardPopUpRun());


    }

    private IEnumerator AddCardPopUpRun()
    {
        if (gameObject.activeSelf && !string.IsNullOrEmpty(core.scanText))
        {
            ConvertScanData();
            SetUpPopUp();
            core.scanText = "";
        }

        yield return new WaitForSeconds(1f);
    }

    private void SetUpPopUp()
    {
        if (LackOfData)
        {
            Result_text.text = "<color=red>Card Scan Failed";

            AddButton.SetActive(false);
            CancelButton.SetActive(false);
            OkButton.SetActive(true);
        }
        else
        {
            Result_text.text = "<color=green>Card Scan Sucsessful!";

            AddButton.SetActive(true);
            CancelButton.SetActive(true);
            OkButton.SetActive(false);

            Name_text.text = "Name: " + dataList[0];
            Email_text.text = "Email: " + dataList[1];
            Phone_text.text = "Phone: " + dataList[2];
            Website_text.text = "Website: " + dataList[3];
        }
        PopUp.SetActive(true);
    }

    private void ConvertScanData()
    {
        string[] scanData = core.scanText.Split("|");

        if (scanData.Length < 4) LackOfData = true;
        else LackOfData = false;

        dataList.Clear();

        foreach (string _data in scanData)
        {
            dataList.Add(_data);
        }
    }
}

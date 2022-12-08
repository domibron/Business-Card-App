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

    [SerializeField]
    private Transform childToParentCard;

    bool LackOfData;

    private string holdData;

    List<string> dataList = new List<string>();

    List<GameObject> Cards = new List<GameObject>();

    private string longData;

    void Awake()
    {
        core = GetComponentInParent<Core>();
    }

    // Start is called before the first frame update 🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈
    void Start()
    {
        PopUp.SetActive(false);

        // childToParentCard = transform.Find("Container (image)");

        if (string.IsNullOrEmpty(core.savedCards)) return;

        longData = core.savedCards;
        string[] CardsToCardData = longData.Split("🐈");

        for (int i = 0; i < CardsToCardData.Length; i++)
        {
            createCard(CardsToCardData[i], Cards.Count);
        }

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AddCardPopUpRun());


    }

    private IEnumerator SaveCards()
    {
        yield return new WaitForSeconds(5f);
        if (Cards.Count <= 0) yield return new WaitForSeconds(1f);

        string _data = Cards[0].GetComponent<Card_details>().data;

        if (Cards.Count <= 1) yield return new WaitForSeconds(1f);

        for (int i = 1; i < Cards.Count; i++)
        {
            _data = _data + "🐈" + Cards[i].GetComponent<Card_details>().data;
        }
    }

    public void CancelCard()
    {
        PopUp.SetActive(false);
        holdData = "";
    }

    public void AddCard()
    {
        createCard(holdData, Cards.Count);
        if (string.IsNullOrEmpty(longData)) longData = holdData;
        else longData = longData + "🐈" + holdData;

        core.GenericSave("savedcards", longData);
        CancelCard();
    }

    private void createCard(string detailForCard, int id)
    {
        //GameObject CardItem = Resources.Load("/Card_Item") as GameObject;

        GameObject go = Instantiate(Resources.Load("Card_Item", typeof(GameObject)), childToParentCard) as GameObject;
        go.name = id.ToString();
        //sgo.transform.SetParent(childToParentCard);

        Card_details cd = go.GetComponent<Card_details>();
        cd.setData(detailForCard);

        Cards.Add(go);

    }

    private IEnumerator AddCardPopUpRun()
    {
        if (gameObject.activeSelf && !string.IsNullOrEmpty(core.scanText))
        {
            holdData = core.scanText;
            ConvertScanData(core.scanText);
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

    private void ConvertScanData(string ToConvert)
    {
        string[] scanData = ToConvert.Split("|");

        if (scanData.Length < 4) LackOfData = true;
        else LackOfData = false;

        dataList.Clear();

        foreach (string _data in scanData)
        {
            dataList.Add(_data);
        }
    }
}

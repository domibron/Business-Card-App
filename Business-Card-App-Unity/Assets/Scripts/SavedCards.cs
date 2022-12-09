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
    GameObject CardMenu;

    private GameObject SelectedCard;
    private int SelectedCardID;

    [SerializeField]
    private Transform childToParentCard;

    [SerializeField]
    private TMP_Text Name_text_SC;
    [SerializeField]
    private TMP_Text Email_text_SC;
    [SerializeField]
    private TMP_Text Phone_text_SC;
    [SerializeField]
    private TMP_Text Website_text_SC;

    bool LackOfData;

    private string holdData;
    private string holdString;

    List<string> dataList = new List<string>();

    List<GameObject> Cards = new List<GameObject>();

    private string longData;

    void Awake()
    {
        core = GetComponentInParent<Core>();

    }

    // S🐈🐈🐈🐈🐈🐈🐈🐈🐈🐈
    void Start()
    {
        PopUp.SetActive(false);
        CardMenu.SetActive(false);

        // childToParentCard = transform.Find("Container (image)");

        GenerateCardList();
    }

    private void GenerateCardList(bool reRun = false)
    {
        Cards.Clear();

        if (!reRun) core.GenericRead("savedcards");
        else core.savedCards = core.GenericRead("savedcards");

        if (!string.IsNullOrEmpty(core.savedCards))
        {
            longData = core.savedCards;
            string[] CardsToCardData = longData.Split("#");

            for (int i = 0; i < CardsToCardData.Length; i++)
            {
                createCard(CardsToCardData[i], Cards.Count);
            }
        }

        GetComponentInChildren<Scrollbar>().value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AddCardPopUpRun());


    }

    public void UseCardMenu(GameObject go)
    {
        SelectedCardID = Cards.IndexOf(go);
        SelectedCard = go;

        Card_details cd = go.GetComponent<Card_details>();

        Name_text_SC.text = cd.details[0];
        Email_text_SC.text = cd.details[1];
        Phone_text_SC.text = cd.details[2];
        Website_text_SC.text = cd.details[3];

        CardMenu.SetActive(true);
    }

    public void DeleteCard()
    {
        Cards.Remove(SelectedCard);
        Destroy(SelectedCard);

        holdString = string.Empty;

        if (Cards.Count >= 1) holdString = Cards[0].GetComponent<Card_details>().data;

        if (Cards.Count > 1)
        {
            for (int i = 1; i < Cards.Count; i++)
            {
                holdString = holdString + "#" + Cards[i].GetComponent<Card_details>().data;
            }
        }

        if (string.IsNullOrEmpty(holdString)) PlayerPrefs.DeleteKey("savedcards");
        else core.GenericSave("savedcards", holdString);

        foreach (GameObject go in Cards)
        {
            Destroy(go);
        }

        SelectedCard = null;
        SelectedCardID = -1;
        CardMenu.SetActive(false);

        GenerateCardList(true);
    }

    // private IEnumerator SaveCards()
    // {
    //     yield return new WaitForSeconds(5f);
    //     if (Cards.Count <= 0) yield return new WaitForSeconds(1f);

    //     string _data = Cards[0].GetComponent<Card_details>().data;

    //     if (Cards.Count <= 1) yield return new WaitForSeconds(1f);

    //     for (int i = 1; i < Cards.Count; i++)
    //     {
    //         _data = _data + "#" + Cards[i].GetComponent<Card_details>().data;
    //     }

    //     core.GenericSave("savedcards", _data);
    // }

    public void CancelCard()
    {
        PopUp.SetActive(false);
        holdData = "";
    }

    public void AddCard()
    {
        createCard(holdData, Cards.Count);
        if (string.IsNullOrEmpty(longData)) longData = holdData;
        else longData = longData + "#" + holdData;

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

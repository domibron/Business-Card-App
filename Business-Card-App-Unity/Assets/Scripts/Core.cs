using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core _core;

    public Texture2D QRCode;

    public string QRText;

    public string scanText;

    public string savedCards;

    [SerializeField] Menu[] menus;

    // Start is called before the first frame update
    void Awake()
    {
        if (_core != null && _core != this)
        {
            Destroy(this);
        }
        else
        {
            _core = this;
        }
    }

    void Start()
    {
        OpenMenu("main menu");
        // if (Save_Manager.instance.hasLoaded)
        // {
        //     if (Save_Manager.instance.saveData.QRCODE.Length > 0)
        //     {
        //         ReadFromFile();
        //     }
        // }

        if (PlayerPrefs.HasKey("qr")) ReadFromFileQR();
        if (PlayerPrefs.HasKey("qrtext")) ReadFromFileQRText();
        if (PlayerPrefs.HasKey("savedcards")) savedCards = GenericRead("savedcards");
    }


    public void GenericSave(string key, string data)
    {
        print($"saving {key} with {data}");
        PlayerPrefs.SetString(key, data);
        PlayerPrefs.Save();
    }

    public string GenericRead(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void WriteToFileQRText(string text)
    {
        print(text);
        PlayerPrefs.SetString("qrtext", text);
        PlayerPrefs.Save();
    }

    public void ReadFromFileQRText()
    {
        string tempstring = PlayerPrefs.GetString("qrtext");
        QRText = tempstring;
    }


    // == texture save and load

    public void WriteToFileQR(Texture2D texture)
    {
        byte[] bytes;
        bytes = texture.EncodeToPNG();

        var str = bytes[0].ToString();

        for (int i = 1; i < bytes.Length; i++)
        {
            str = str + "|" + bytes[i].ToString();
        }

        print(str);
        PlayerPrefs.SetString("qr", str);
        PlayerPrefs.Save();
        // Save_Manager.instance.saveData.QRCODE = str;
        //Save_Manager.instance.Save();
    }

    public void ReadFromFileQR()
    {
        string tempstring = PlayerPrefs.GetString("qr");
        //string tempstring = Save_Manager.instance.saveData.QRCODE;

        string[] allNums;

        allNums = tempstring.Split("|");

        byte[] bytes = new byte[allNums.Length];

        for (int i = 0; i < allNums.Length; i++)
        {
            bytes[i] = byte.Parse(allNums[i]);
            print(bytes[i]);
        }

        Texture2D texture = new Texture2D(256, 256);
        texture.LoadImage(bytes);

        QRCode = texture;
    }


    //SAVE TEXTURE
    Texture2D save_s01_texture;
    void SaveToFile(Texture2D texture, string filename)
    {
        byte[] bytes;
        bytes = texture.EncodeToPNG();

        System.IO.FileStream fileSave;
        fileSave = new FileStream(Application.dataPath + "/Save/" + filename, FileMode.Create);

        System.IO.BinaryWriter binary;
        binary = new BinaryWriter(fileSave);
        binary.Write(bytes);
        fileSave.Close();
    }
    //LOAD TEXTURE
    // string filename = "s01_texture.png";
    Texture2D loaded_s01_texture;
    void LoadFromFile(Texture2D texture, string filename)
    {
        System.IO.FileStream fileLoad;
        fileLoad = new FileStream(Application.dataPath + "/Save/" + filename, FileMode.Open, FileAccess.Read, FileShare.None);
        //loaded_s01_texture = fileLoad.ReadByte();
        fileLoad.Close();
    }



    // ========= menu

    public bool ReturnIsOpenMenuName(string name)
    {
        foreach (Menu _menu in menus)
        {
            if (_menu.menuName == name && _menu.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
}

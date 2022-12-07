using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCard : MonoBehaviour
{
    [SerializeField]
    private GameObject FrontOfCard;
    [SerializeField]
    private GameObject BackOfCard;

    // Start is called before the first frame update
    void Start()
    {
        FrontOfCard.SetActive(true);
        BackOfCard.SetActive(false);
    }

    public void FlipTheCard()
    {
        FrontOfCard.SetActive(BackOfCard.activeSelf);
        BackOfCard.SetActive(!FrontOfCard.activeSelf);
    }
}

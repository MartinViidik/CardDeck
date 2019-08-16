using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInspection : MonoBehaviour{

    public GameObject stats;

    public GameObject card;

    public Text name;
    public Text health;
    public Text attack;
    public Text cost;
    public Text type;
    public Text theme;

    public bool inspecting;
    public Sprite cardImage;

    private Dropdown filterDropdown;
    private Dropdown sortDropdown;
    private Button addCards;

    private void Awake(){
        stats.SetActive(false);
        filterDropdown = GameObject.Find("DropdownFilter").GetComponent<Dropdown>();
        sortDropdown = GameObject.Find("DropdownTheme").GetComponent<Dropdown>();
        addCards = GameObject.Find("AddCard").GetComponent<Button>();
    }

    public void GetCard(GameObject newCard){
        DisplayStats(newCard);
        stats.SetActive(true);
    }

    void DisplayStats(GameObject newCard){
        card.GetComponent<Image>().sprite = newCard.GetComponent<Image>().sprite;
        name.text = newCard.GetComponent<Card>().name;
        type.text = newCard.GetComponent<Card>().type;
        theme.text = newCard.GetComponent<Card>().theme;
        health.text = newCard.GetComponent<Card>().health.ToString();
        cost.text = newCard.GetComponent<Card>().cost.ToString();
        attack.text = newCard.GetComponent<Card>().attack.ToString();
    }


}

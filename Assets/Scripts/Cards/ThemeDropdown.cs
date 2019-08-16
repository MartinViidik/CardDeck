using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeDropdown : MonoBehaviour
{
    private CardHandler cardHandler;
    private Dropdown dropdown;

    private void Awake()
    {
        cardHandler = GameObject.Find("CardHandler").GetComponent<CardHandler>();
        dropdown = GetComponent<Dropdown>();

        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    void DropdownValueChanged(Dropdown change){
        if (change.value == 0){
            cardHandler.revealCards();
        }
        if (change.value == 1){
            cardHandler.filterCards("scifi");
        }
        if (change.value == 2){
            cardHandler.filterCards("adventure");
        }
        if (change.value == 3){
            cardHandler.filterCards("fantasy");
        }
        if (change.value == 4){
            cardHandler.filterCards("mystical");
        }
        if (change.value == 5){
            cardHandler.filterCards("neutral");
        }
        cardHandler.SortCards();
    }

    public string selectedTheme(){
        int index = dropdown.value;
        string theme = dropdown.options[index].text;
        return theme;
    }
}

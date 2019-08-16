using UnityEngine;
using UnityEngine.UI;

public class FilterDropdown : MonoBehaviour{
    private CardHandler cardHandler;
    private Dropdown dropdown;

    private void Awake(){
        cardHandler = GameObject.Find("CardHandler").GetComponent<CardHandler>();
        dropdown = GetComponent<Dropdown>();

        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
    }

    void DropdownValueChanged(Dropdown change){
        cardHandler.SortCards();
    }

    //Gets dropdowns selected value and returns it, used to select what theme to filter by
    public string selectedTheme(){
        int index = dropdown.value;
        string theme = dropdown.options[index].text;
        return theme;
    }
}
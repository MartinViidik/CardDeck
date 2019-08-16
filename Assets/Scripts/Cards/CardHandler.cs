using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class CardHandler : MonoBehaviour{

    public GameObject[] cards;
    private Transform collection;

    public Dropdown themeDropdown;
    public Dropdown sortDropdown;

    public GameObject blankCard;

    int amountOfCards;

    private void Start(){
        if(cards.Length == 0){
            GetCards();
        }
        if (collection == null){
            collection = GameObject.Find("CardCollection").transform;
            amountOfCards = collection.childCount;
        }

    }

    //Loads all of the card gameobjects from the resources folder and assigns them to an array.
    private void GetCards(){
        object[] loadedCards = Resources.LoadAll("Objects/", typeof(GameObject));
        cards = new GameObject[loadedCards.Length];
        for (int x = 0; x < loadedCards.Length; x++){
            cards[x] = (GameObject)loadedCards[x];
        }
    }

    //Replaces the dummy blank cards that are initially set up in the scene with real cards
    public void GenerateDeck(){
        for(int i = 0; i < amountOfCards; i++){
            GameObject oldCard = collection.GetChild(i).gameObject;
            Instantiate(GetRandomCard(), oldCard.transform).transform.SetParent(collection, false);
            Destroy(oldCard);
        }
        SortCards();
    }

    public GameObject GetRandomCard(){
        int index = Random.Range(0, cards.Length);
        GameObject card = cards[index];
        return card;
    }

    public void SortCards(){
        var children = collection.GetComponentsInChildren<Transform>(true).ToList();
        children.Remove(collection.transform);
        children.Sort(Compare);
        for (int i = 0; i < children.Count; i++)
            StartCoroutine(setIndex(children[i], i));
    }


    public int Compare(Transform lhs, Transform rhs){
        Card Card1 = lhs.GetComponent<Card>();
        Card Card2 = rhs.GetComponent<Card>();

        int index = sortDropdown.GetComponent<Dropdown>().value;
        string modifier = sortDropdown.GetComponent<Dropdown>().options[index].text;

        var test = Card1.gameObject.activeInHierarchy.CompareTo(Card2.gameObject.activeInHierarchy);

        if(modifier == "attack"){
            if (Card1.attack < Card2.attack) return -1;
            if (Card1.attack > Card2.attack) return 1;
        }
        if(modifier == "health"){
            if (Card1.health < Card2.health) return -1;
            if (Card1.health > Card2.health) return 1;
        }
        if (modifier == "cost"){
            if (Card1.cost < Card2.cost) return -1;
            if (Card1.cost > Card2.cost) return 1;
        }
        return 0;
    }

    public void filterCards(string modifier){
        for (int i = 0; i < collection.childCount; i++){
            GameObject card = collection.GetChild(i).gameObject;
            if (card.GetComponent<Card>().theme != modifier){
                card.GetComponent<Card>().hideCard();
                StartCoroutine(moveCard(card));
            } else {
                if(card.GetComponent<Card>().hidden && themeDropdown.GetComponent<Dropdown>().value > 0) card.GetComponent<Card>().revealCard();
            }
        }
    }

    public void revealCards(){
        for (int i = 0; i < collection.childCount; i++){
            GameObject card = collection.GetChild(i).gameObject;
            if (card.GetComponent<Card>().hidden){
                card.GetComponent<Card>().revealCard();
            }
        }
    }

    public void shuffleCards(){
        for(int i = 0; i < collection.childCount; i++){
            int rnd = Random.Range(0, collection.childCount);
            collection.GetChild(i).SetSiblingIndex(rnd);
        }
    }


    //Small delay before moving cards around, to help avoid a visual where the cards pop into place during transition animation 
    IEnumerator moveCard(GameObject card){
        yield return new WaitForSeconds(0.22f);
        if (card != null){
            card.transform.SetAsFirstSibling();
        }
    }

    IEnumerator setIndex(Transform card, int index){
        yield return new WaitForSeconds(0.22f);
        if (card != null){
            card.transform.SetAsFirstSibling();
            card.SetSiblingIndex(index);
        }
    }

}

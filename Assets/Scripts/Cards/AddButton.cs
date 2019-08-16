using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour{
    public CardHandler cardHandler;

    private void Awake(){
        cardHandler = cardHandler.GetComponent<CardHandler>();
    }

    public void AddNewCard(){
        cardHandler.GenerateDeck();
    }
}

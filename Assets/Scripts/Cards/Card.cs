using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler{

    [Header("Card attributes")]
    public int health;
    public int attack;
    public int cost;
    public string type;
    public string theme;
    public string name;

    [Header("Technical")]
    public Sprite blankCard;
    public bool hidden;

    Sprite initialSprite;
    Image image;
    private CardInspection zoomView;

    private int initialHealth;
    private int initialAttack;
    private int initialCost;


    void Awake(){
        getStats();
        zoomView = GameObject.Find("ZoomedView").GetComponent<CardInspection>();
        if (!zoomView.inspecting){
            image = GetComponent<Image>();
            initialSprite = GetComponent<Image>().sprite;
            revealCard();
        } else {
            transform.DOScale(1, 0.15f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(!zoomView.inspecting)transform.DOScale(1.1f, 0.15f);
    }

    public void OnPointerExit(PointerEventData eventData){
        if(!zoomView.inspecting)transform.DOScale(1, 0.15f);
    }

    public void OnPointerDown(PointerEventData eventData){
        if(!zoomView.inspecting)transform.DOScale(0.9f, 0.15f);
    }

    public void OnPointerUp(PointerEventData eventData){
        if(!zoomView.inspecting)transform.DOScale(1.1f, 0.15f);
    }

    public void OnPointerClick(PointerEventData eventData){
        if(type != "none" && !hidden && !zoomView.inspecting){
            transform.DOScale(1, 0.15f);
            zoomView.inspecting = true;
            zoomView.GetCard(gameObject);
            zoomView.GetComponent<CardInspection>().transform.DOScale(1, 0.15f);
        } else {
            zoomView.inspecting = false;
            zoomView.GetComponent<CardInspection>().transform.DOScale(0, 0.15f);
        }
    }

    //Achieving the "animated" card effect, through the use of coroutines and DOTween

    public void hideCard(){
        StartCoroutine(ToFront());
        setBlank();
    }

    public void revealCard(){
        if (!zoomView.inspecting){
            StartCoroutine(ToBack());
            restoreStats();
        }
    }

    IEnumerator ToFront(){
        transform.DORotate(new Vector3(0, 90, 0), 0.2f);
        for (float i = 0.2f; i >= 0; i -= Time.deltaTime)
            yield return 0;
        image.sprite = blankCard;
        transform.DORotate(new Vector3(0, 0, 0), 0.2f);
    }

    IEnumerator ToBack(){
        transform.DORotate(new Vector3(0, 90, 0), 0.2f);
        for (float i = 0.2f; i >= 0; i -= Time.deltaTime)
            yield return 0;
        image.sprite = initialSprite;
        transform.DORotate(new Vector3(0, 0, 0), 0.2f);
    }

    //This section here is to avoid situations where if a card is hidden, it might have higher values than the cards currently being displayed

    public void setBlank(){
        attack = -1;
        cost = -1;
        health = -1;
        hidden = true;
    }

    void getStats(){
        initialAttack = attack;
        initialCost = cost;
        initialHealth = health;
    }

    void restoreStats(){
        attack = initialAttack;
        cost = initialCost;
        health = initialHealth;
        hidden = false;
    }
}

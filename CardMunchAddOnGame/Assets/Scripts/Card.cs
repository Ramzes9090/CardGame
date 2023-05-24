using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    private GameObject cardPlayed;
    private GameObject compareOne;
    private GameObject compareTwo;


    public int handIndex;
    public int cardLevel;

    private SpriteRenderer rend;
    private GameManager gm;
    private CardLevel cl;


    public bool enemyHandCard = false;


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        cl = GetComponent<CardLevel>();
        cardPlayed = GameObject.Find("PositionPlayedCard");
        compareOne = GameObject.Find("PositionCompareOne");
        compareTwo = GameObject.Find("PositionCompareTwo");
        cardLevel = cl.Level;
    }

    public void OnMouseDown()
    {
        if (hasBeenPlayed == false && gm.playedCardNumber == 0 && gm.playerHand.Count == 2 && enemyHandCard == false)
        {

            gm.availableCardSlotsPlayer[handIndex] = true;
            handIndex = 0;
            this.transform.position = cardPlayed.transform.position;
            this.transform.rotation = cardPlayed.transform.rotation;
            hasBeenPlayed = true;
            gm.playerHand.Remove(this);
            MoveToPlayedCard();
            gm.cardRenderLayer++;
            rend.sortingOrder = gm.cardRenderLayer;

            gm.playedCardNumber++;
        }
    }
    public void EnemyPlay()
    {
        if (hasBeenPlayed == false)
        {

            gm.availableCardSlotsEnemy[handIndex] = true;
            handIndex = 0;
            this.transform.position = cardPlayed.transform.position;
            this.transform.rotation = cardPlayed.transform.rotation;

            hasBeenPlayed = true;
            MoveToPlayedCard();
            gm.cardRenderLayer++;
            rend.sortingOrder = gm.cardRenderLayer;
        }
    }
    void MoveToPlayedCard()
    {
        enemyHandCard = false;
        gm.playedCard.Add(this);
    }

    public void ToCompareCardEnemy()
    {
        if (hasBeenPlayed == false)
        {
            enemyHandCard = false;

            this.transform.position = compareTwo.transform.position;
            this.transform.rotation = cardPlayed.transform.rotation;
            hasBeenPlayed = true;
            gm.availableCardSlotsEnemy[handIndex] = true;
            gm.playedCard.Add(this);

        }
    }
    public void ToCompareCardPlayer()
    {
        if (hasBeenPlayed == false)
        {

            this.transform.position = compareOne.transform.position;
            this.transform.rotation = cardPlayed.transform.rotation;
            hasBeenPlayed = true;
            gm.availableCardSlotsPlayer[handIndex] = true;
            gm.playedCard.Add(this);
        }
    }
}  

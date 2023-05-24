using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card> ();
    public List<Card> playedCard = new List<Card> ();
    public List<Card> enemyHand = new List<Card> ();
    public List<Card> playerHand = new List<Card> ();
    public Sprite[] spriteArray = new Sprite[2];

    public Transform[] cardSlotsPlayer;
    public Transform[] cardSlotsEnemy;

    public bool[] availableCardSlotsPlayer;
    public bool[] availableCardSlotsEnemy;

    public Text deckSizeText;
    public Text playedCardText;
    public int cardRenderLayer;
    public Sprite newSprite;

    private GameObject btnDraw;
    private GameObject btnEndTour;

    public int playedCardNumber = 0;
    public bool btnDrawActive = true;

    // method start when you start the game, create the free slot to use for player and enemy

    public void Start()
    {
        for (int i = 0; i< availableCardSlotsPlayer.Length; i++)
        {
            availableCardSlotsEnemy[i] = true;
            availableCardSlotsPlayer[i] = true;
        }
        cardRenderLayer = 0;
        btnDraw = GameObject.Find("DrawCard");
        btnEndTour = GameObject.Find("EndTourBtn");
    }

    // When you press the button to Draw Card, method draw new card to your hand

    public void DrawCard()
    {
        if (deck.Count >= 1) 
        {
            Card randCard = deck[Random.Range(0,deck.Count)];
             
            for (int i = 0;i<availableCardSlotsPlayer.Length; i++) 
            {
                if (availableCardSlotsPlayer[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlotsPlayer[i].position;
                    randCard.transform.rotation = cardSlotsPlayer[i].rotation;        // change rotation of your card on hand
                    randCard.hasBeenPlayed = false;
                    
                    availableCardSlotsPlayer[i] = false;
                    playerHand.Add(randCard);
                    deck.Remove(randCard);
                    return;
                }
            } 
        }
    }
    
    // this method is activated when you press button End Tour, this is logic of play your enemy

    public void EndTour()
    {
        for (int j = 0; j < availableCardSlotsEnemy.Length; j++)
        {
            //Debug.Log("Wolny slot: " + j + " " + availableCardSlotsEnemy[j]);
            playedCardNumber = 0;
            Invoke("EnemyTour", 1f);
            
        }
        Debug.Log("Enemy Tour ... ");
        Invoke("EnemyPlayCard", 3f);
    }

    // method to choese with card is played from enemy

    public void EnemyPlayCard()
    {
        int Randnumber = Random.Range(0, enemyHand.Count);
        Card cardToPlay = enemyHand[Randnumber];

        cardToPlay.gameObject.GetComponent<SpriteRenderer>().sprite = spriteArray[Randnumber];

        cardToPlay.EnemyPlay();
        enemyHand.Remove(cardToPlay);
        btnDrawActive = true;
    }

    // method draw new card to enemy hand

    public void EnemyTour()
    {
        if (deck.Count >= 1)
        {

            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlotsEnemy.Length; i++)
            {
                if (availableCardSlotsEnemy[i] == true)
                {
                    spriteArray[i] = randCard.gameObject.GetComponent<SpriteRenderer>().sprite; 
                    randCard.gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardSlotsEnemy[i].position;
                    randCard.transform.rotation = cardSlotsPlayer[i].rotation;        // change rotation of your card on hand
                    
                    randCard.hasBeenPlayed = false;
                    randCard.enemyHandCard = true;

                    availableCardSlotsEnemy[i] = false;
                    enemyHand.Add(randCard);
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    // When you press the button to shuffle all played card to deck

    public void Shuffle()
    {
        if(playedCard.Count >= 1) 
        {
            foreach(Card card in playedCard) 
            {
                card.hasBeenPlayed = false;
                card.handIndex = 0;
                deck.Add(card);
                card.gameObject.SetActive(false);
                
            }
            playedCard.Clear();
            cardRenderLayer = 0;
        }     
    }

    // all the time cheaking how many card is on board or inside of the deck

    private void Update()
    {
        deckSizeText.text = deck.Count.ToString();      
        playedCardText.text = playedCard.Count.ToString();
        
        if(playerHand.Count == 2)
        {
            btnDraw.SetActive(false);
            btnDrawActive = false;
        }
        else if(btnDrawActive == true)
        {
            btnDraw.SetActive(true);            
        }
        
        if(playedCardNumber == 0)
        {
            btnEndTour.SetActive(false);
        }
        else if(playedCardNumber == 1)
        {
            btnEndTour.SetActive(true);
        }
    }
}

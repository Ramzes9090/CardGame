using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private GameManager gm;
    

    private int CardPlayedToEndGame = 0;
    private bool stratCompare = true;

    public int enemyScore;
    public int playerScore;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>(); 
        CardPlayedToEndGame = gm.deck.Count;
    }

    private void Update()
    {
        if (gm.deck.Count == 0 && gm.playedCard.Count == CardPlayedToEndGame - 2 && stratCompare == true)
        {
            stratCompare = false;
            Debug.Log("End Game!");
            Invoke("CompareCards", 1f);

            Invoke("Winner", 2f);
        }
    }

    private void CompareCards()
    {
        
        Debug.Log("Compare cards!");

        EnemyPlayCard();
        PlayerPlayCard();

    }
    public void EnemyPlayCard()
    {
        Card cardToCompare = gm.enemyHand[0];
        cardToCompare.gameObject.GetComponent<SpriteRenderer>().sprite = gm.spriteArray[0];
        enemyScore = cardToCompare.cardLevel;
        cardToCompare.ToCompareCardEnemy();
        gm.enemyHand.Remove(cardToCompare);
    }
    public void PlayerPlayCard()
    {
        Card cardToCompare = gm.playerHand[0];
        playerScore = cardToCompare.cardLevel;
        cardToCompare.ToCompareCardPlayer();
        gm.playerHand.Remove(cardToCompare);
    }
    public void Winner()
    {
        if(playerScore > enemyScore)
        {
            Debug.Log("Player Win!!!");
        }
        else if(playerScore < enemyScore) 
        {
            Debug.Log("Enemy Win!!!");
        }
        else
        {
            Debug.Log("Draw!!!");
        }

        playerScore = 0;
        enemyScore = 0;
        stratCompare = true;
    }
}

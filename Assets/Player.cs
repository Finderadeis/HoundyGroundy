using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int playerID;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject turnIndicator;
    [SerializeField] TextMeshProUGUI bridgeCount;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI instructions;
    [SerializeField] TextMeshProUGUI ap;
    [SerializeField] Button endTurnButton;
    List<Card> cardsOnHand = new List<Card>();
    public int currentAP = 4;
    public int currentScore = 0;
    public int currentBridgeCount = 0;

    private void Start()
    {
        bridgeCount.text = currentBridgeCount.ToString();
        score.text = currentScore.ToString();
    }


    public void ReceiveCard(GameObject card){
        GameObject newCard = Instantiate(card, hand.transform);
        newCard.GetComponent<Card>().ownerID = playerID;
        newCard.GetComponent<Card>().originalParent = hand.transform;
        cardsOnHand.Add(newCard.GetComponent<Card>());
    }

    public void StartTurn(){
        MakeCardsGrabbable();
        instructions.text = "Plättchen legen";
        turnIndicator.SetActive(true);
        currentAP = 4;
        ap.text = "AP: " + currentAP;
    }

    public void StartMovePhase(){
        LockCardInteraction();
        instructions.text = "Plättchen bewegen oder drehen";
        endTurnButton.interactable = true;
    }
    

    public void EndTurn(){
        LockCardInteraction();
        turnIndicator.SetActive(false);
        instructions.text = "";
        endTurnButton.interactable = false;
    }

    public void RemoveAP(int amount){
        currentAP -= amount;
        ap.text = "AP: " + currentAP;
    }
    public void LockCardInteraction(){
        foreach (Card card in cardsOnHand)
        {
            card.MakeInactive();
        }
    }
    public void MakeCardsGrabbable(){
        foreach (Card card in cardsOnHand)
        {
            card.MakeGrabbable();
        }
    }

    public void EndGame(){
        while (hand.transform.childCount > 0) {
            DestroyImmediate(hand.transform.GetChild(0).gameObject);
        }
        turnIndicator.SetActive(false);
        currentScore = 0;
        currentBridgeCount = 0;
    }

    public void AddBridge(int points)
    {
        currentBridgeCount++;
        currentScore += points;
        bridgeCount.text = currentBridgeCount.ToString();
        score.text = currentScore.ToString();
    }
}

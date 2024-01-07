using System.Collections.Generic;

public class M_Player
{
    int playerID;
    List<M_Card> cardsOnHand = new List<M_Card>();
    int currentAP;
    int currentScore;
    int currentBridgeCount;

    public M_Player(int ID)
    {
        playerID = ID;
        currentAP = 4;
        currentScore = 0;
        currentBridgeCount = 0;
    }

    public void AddCard(M_Card m_Card)
    {
        M_Card newCard = new M_Card(m_Card.colorTopLeft, m_Card.colorTopRight, m_Card.colorBottomLeft, m_Card.colorBottomRight);
        newCard.ownerID = playerID;
        cardsOnHand.Add(newCard);
    }

    internal void StartTurn()
    {
        currentAP = 4;
    }

    internal void PlaceCard(M_Card m_Card, M_Field target)
    {
        cardsOnHand.Remove(m_Card);
        M_Game.Instance.PlaceCard(m_Card, target);
    }

    internal void GoToMovePhase()
    {
        // for now, nothing needs to be done here
    }

    internal void EndTurn()
    {
        // for now, nothing to be done here
    }

    internal void RemoveAP(int amount)
    {
        currentAP -= amount;
    }

    internal void EndGame()
    {
        cardsOnHand.Clear();
        currentScore = 0;
        currentBridgeCount = 0;
    }

    internal void AddBridge(int points)
    {
        currentBridgeCount++;
        currentScore += points;
    }
}

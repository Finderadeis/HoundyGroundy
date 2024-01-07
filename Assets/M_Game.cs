using System;
using System.Collections.Generic;

public class M_Game
{
    public static M_Game Instance;

    M_Board board;
    M_Player[] players;
    int activePlayer;
    
    M_Card[] cardVariations;
    List<M_Card> currentlyAvailableCards;

    bool isRunning;
    int currentBridges;
    enum GameState
    {
        PLACE, MOVE, DRAW, COUNT
    }
    GameState currentGameState;
    M_Card grabbedCard;
    bool cardGrabbed;
    List<M_Card> cooldownStack;
    List<M_Card> coolDownStackInQueue;
    List<M_Card> coolDownStackOutQueue;

    public void Initialize()
    {
        Instance = new M_Game();
        board = new M_Board();
        players = new M_Player[2];
        players[0] = new M_Player(0);
        players[1] = new M_Player(1);
        activePlayer = 0;
        cardVariations = new M_Card[6];
        cardVariations[0] = new M_Card(0,1,1,0);
        cardVariations[1] = new M_Card(1,0,1,0);
        cardVariations[2] = new M_Card(1,0,0,0);
        cardVariations[3] = new M_Card(0,1,1,1);
        cardVariations[4] = new M_Card(0,0,0,0);
        cardVariations[5] = new M_Card(1,1,1,1);
        isRunning = false;
        currentBridges = 0;
        currentGameState = GameState.PLACE;
        grabbedCard = null;
        cardGrabbed = false;
        cooldownStack = new List<M_Card>();
        coolDownStackInQueue = new List<M_Card>();
        coolDownStackOutQueue = new List<M_Card>();
    }

    public void StartGame()
    {
        int startingPlayer = UnityEngine.Random.Range(0, 2);
        activePlayer = startingPlayer;
        currentBridges = 0;
        grabbedCard = null;
        cardGrabbed = false;
        cooldownStack = new List<M_Card>();
        coolDownStackInQueue = new List<M_Card>();
        coolDownStackOutQueue = new List<M_Card>();
        board.Initialize(4);
        InitializeDrawPool();
        isRunning = true;
        currentGameState = GameState.DRAW;

        for (int i = 0; i < 3; i++)
        {
            DealCard(0);
            DealCard(1);
        }

        GoToPlacePhase();
    }

    private void GoToPlacePhase()
    {
        currentGameState = GameState.PLACE;
        board.WaitForPlace();
        players[activePlayer].StartTurn();
    }

    public void GoToMovePhase()
    {
        currentGameState = GameState.MOVE;
        board.WaitForMove();
        players[activePlayer].GoToMovePhase();
    }

    public void GoToDrawPhase()
    {
        currentGameState = GameState.DRAW;
        DealCard(activePlayer);
        GoToCountPhase();
    }

    private void GoToCountPhase()
    {
        currentGameState = GameState.COUNT;
        int bridgesP1 = board.FindBridges(0);
        int bridgesP2 = board.FindBridges(1);
        currentBridges += bridgesP1 + bridgesP2;
        activePlayer = activePlayer == 0 ? 1 : 0;
        ProcessCooldowns();
        GoToPlacePhase();
    }

    private void ProcessCooldowns()
    {
        foreach (M_Card card in cooldownStack)
        {
            card.reduceCooldown();
        }
        foreach (M_Card card in coolDownStackInQueue)
        {
            cooldownStack.Add(card);
        }
        coolDownStackInQueue.Clear();
        foreach (M_Card card in coolDownStackOutQueue)
        {
            cooldownStack.Remove(card);
        }
        coolDownStackOutQueue.Clear();
    }

    private void InitializeDrawPool()
    {
        for (int i = 0; i < 2; i++) // 2Corner
        {
            currentlyAvailableCards.Add(cardVariations[0]);
        }
        for (int i = 0; i < 10; i++) // 2Straight
        {
            currentlyAvailableCards.Add(cardVariations[1]);
        }
        for (int i = 0; i < 5; i++) // 3Black
        {
            currentlyAvailableCards.Add(cardVariations[2]);
        }
        for (int i = 0; i < 5; i++) // 3White
        {
            currentlyAvailableCards.Add(cardVariations[3]);
        }
        for (int i = 0; i < 5; i++) // 4Black
        {
            currentlyAvailableCards.Add(cardVariations[4]);
        }
        for (int i = 0; i < 5; i++) // 4White
        {
            currentlyAvailableCards.Add(cardVariations[5]);
        }
        ShuffleCards(currentlyAvailableCards);
    }

    private void ShuffleCards(List<M_Card> cards)
    {
        System.Random _random = new System.Random();
        int amount = cards.Count;
        M_Card tmp;
        for(int i = 0; i < amount; i++)
        {
            int randomIndex = _random.Next(amount);
            tmp = cards[randomIndex];
            cards[randomIndex] = cards[i];
            cards[i] = tmp;
        }
    }

    void DealCard(int player)
    {
        int randomCard = UnityEngine.Random.Range(0, currentlyAvailableCards.Count);
        players[player].AddCard(currentlyAvailableCards[randomCard]);
        currentlyAvailableCards.Remove(currentlyAvailableCards[randomCard]);
    }

    void PutCardBackToDeck(int ID)
    {
        currentlyAvailableCards.Add(cardVariations[ID]);
        ShuffleCards(currentlyAvailableCards);
    }

    internal void AddCardToCooldownStack(M_Card newCard)
    {
        coolDownStackInQueue.Add(newCard);
    }

    internal void RemoveCardFromCooldownStack(M_Card m_Card)
    {
        coolDownStackOutQueue.Add(m_Card);
    }

    internal void PlaceCard(M_Card m_Card, M_Field target)
    {
        board.PlaceCard(m_Card, target);
        GoToMovePhase();
    }

    internal void MoveCard(M_Field source, M_Field target)
    {
        board.MoveCard(source, target);
        GoToDrawPhase();
    }

    internal void TurnCard(M_Card m_Card)
    {
        m_Card.TurnCard();
    }
}

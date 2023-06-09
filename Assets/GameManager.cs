using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] int gridSize = 4;
    [SerializeField] int numberToDrawOnStart = 3;
    [SerializeField] int maxCardsOnHand = 3;
    [SerializeField] List<GameObject> availableCardVariations = new List<GameObject>();
    public List<GameObject> currentlyAvailableCards = new List<GameObject>();
    [SerializeField] GameObject emptyCard;
    public PlayGrid playGrid;
    GameTimer gameTimer;

    [SerializeField] List<Player> players = new List<Player>();
    [SerializeField] TextMeshProUGUI bridgeCount;
    public int currentBridges = 0;
    bool gameRunning = false;
    float timeRunning = 0.0f;
    public enum GameState
    {
        PLACE, MOVE, DRAW, COUNT
    }
    public GameState currentGameState;
    public GameObject grabbedCard;
    public bool cardGrabbed;
    public GameObject hoveredCard;

    public int activePlayer = 0;
    int activePlayerAP = 4;
    List<Card> cooldownStack = new List<Card>();
    List<Card> coolDownStackInQueue = new List<Card>();
    List<Card> coolDownStackOutQueue = new List<Card>();
    
    private void Awake() {
        instance = this;
    }

    private void Start() {
        playGrid = FindObjectOfType<PlayGrid>();
        gameTimer = FindObjectOfType<GameTimer>();
    }

    private void Update() {
        if(gameRunning){
            timeRunning += Time.deltaTime;
            gameTimer.UpdateTimer(timeRunning);
        }
    }

    public void StartGame(){
        int startingPlayer = Random.Range(0, 2);
        activePlayer = startingPlayer;
        currentBridges = 0;
        bridgeCount.text = currentBridges.ToString();

        playGrid.Initialize(emptyCard, gridSize);
        InitializeDrawPool();
        gameRunning = true;
        timeRunning = 0f;

        for (int i = 0; i < numberToDrawOnStart; i++)
        {
            DealCard(0);
            DealCard(1);
        }

        GoToPlacePhase();
    }

    public void GoToMovePhase(){
        currentGameState = GameState.MOVE;
        playGrid.MakeCardsGrabbable();
        players[activePlayer].StartMovePhase();
    }

    public void GoToDrawPhase(){
        currentGameState = GameState.DRAW;
        players[activePlayer].EndTurn();
        DealCard(activePlayer);
        GoToCountPhase();
    }

    public void GoToCountPhase(){
        currentGameState = GameState.COUNT;
        playGrid.ReBuildColorMatrix();
        if (playGrid.SearchBridges())
        {
            ProcessBridgeFound();
        }
        activePlayer = activePlayer == 0 ? 1 : 0;
        ProcessCoolDowns();
        GoToPlacePhase();
    }
    public void GoToPlacePhase(){
        currentGameState = GameState.PLACE;
        playGrid.makeCardsInactive();
        players[activePlayer].StartTurn();
    }

    void DealCard(int playerID){
        int rndIndex = Random.Range(0,currentlyAvailableCards.Count);
        players[playerID].ReceiveCard(currentlyAvailableCards[rndIndex]);
        currentlyAvailableCards.Remove(currentlyAvailableCards[rndIndex]);

    }

    public void AddCardToCoolDownStack(Card card){
        coolDownStackInQueue.Add(card);
    }
    public void RemoveCardFromCoolDownStack(Card card){
        coolDownStackOutQueue.Add(card);
    }

    void ProcessCoolDowns(){
        foreach (Card card in cooldownStack)
        {
            card.ReduceCooldown();
        }
        foreach (Card card in coolDownStackInQueue)
        {
            cooldownStack.Add(card);
        }
        coolDownStackInQueue.Clear();
        foreach (Card card in coolDownStackOutQueue)
        {
            cooldownStack.Remove(card);
        }
        coolDownStackOutQueue.Clear();
    }


    public void InitializeDrawPool(){
        for (int i = 0; i < 2; i++) // 2Corner
        {
            currentlyAvailableCards.Add(availableCardVariations[0]);
        }
        for (int i = 0; i < 10; i++) // 2Straight
        {
            currentlyAvailableCards.Add(availableCardVariations[1]);
        }
        for (int i = 0; i < 5; i++) // 3Black
        {
            currentlyAvailableCards.Add(availableCardVariations[2]);
        }
        for (int i = 0; i < 5; i++) // 3White
        {
            currentlyAvailableCards.Add(availableCardVariations[3]);
        }
        for (int i = 0; i < 5; i++) // 4Black
        {
            currentlyAvailableCards.Add(availableCardVariations[4]);
        }
        for (int i = 0; i < 5; i++) // 4White
        {
            currentlyAvailableCards.Add(availableCardVariations[5]);
        }
        ShuffleCards(currentlyAvailableCards);
    }

    void ShuffleCards(List<GameObject> cards){ //FisherYates
        System.Random _random = new System.Random ();
        int amount = cards.Count;
        GameObject tmp;
        for (int i = 0; i < amount; i++)
        {
            int r = i + (int)(_random.NextDouble() * (amount - i));
            tmp = cards[r];
            cards[r] = cards[i];
            cards[i] = tmp;
        }
    }

    void PutCardBackToDeck(int ID){
        currentlyAvailableCards.Add(availableCardVariations[ID]);
        ShuffleCards(currentlyAvailableCards);
    }

    public void EndGame(){
        gameRunning = false;
        playGrid.Reset();
        players[0].EndGame();
        players[1].EndGame();
        currentlyAvailableCards.Clear();
    }

    public void ProcessCardGrab(GameObject card){
        grabbedCard = card;
        cardGrabbed = true;
        playGrid.MakeCardsPlacable(card.GetComponent<Card>());
    }
    public void ProcessCardDrop(){
        if(hoveredCard == null){
            grabbedCard.GetComponent<Card>().PutBack();
            playGrid.makeCardsInactive();
        }
        if(hoveredCard.GetComponent<Card>().cardID == 0){
            hoveredCard.GetComponent<Card>().ChangeCard(grabbedCard.GetComponent<Card>());
            if(grabbedCard.GetComponent<Card>().ownerID == 0){
                grabbedCard.GetComponent<Card>().PutBack();
                grabbedCard.GetComponent<Card>().MakeEmpty();
            }
            else{
                Destroy(grabbedCard);
            }
            playGrid.ReBuildColorMatrix();

            if (playGrid.SearchBridges())
            {
                ProcessBridgeFound();
            }

            switch (currentGameState)
            {
                case GameState.PLACE:
                    GoToMovePhase();
                    break;
                case GameState.MOVE:
                    players[activePlayer].RemoveAP(4);
                    if(players[activePlayer].currentAP < 2){
                        GoToDrawPhase();
                    }
                    break;
                default:
                    break;
            }
        }
        else{
            grabbedCard.GetComponent<Card>().PutBack();
        }
        cardGrabbed = false;
    }

    public void ProcessBridgeFound()
    {
        int bridgeOwner = playGrid.bridgeOwner;
        int bridgePoints = playGrid.bridgePoints;
        StartCoroutine(playGrid.MarkMatchingCards());
        players[bridgeOwner-1].AddBridge(bridgePoints);
        currentBridges++;
        bridgeCount.text = currentBridges.ToString();
    }
}

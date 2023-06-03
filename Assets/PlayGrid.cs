using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : MonoBehaviour
{
    List<Card> cardsOnGrid = new List<Card>();
    public GridCell[,] gridValues;
    int gridSize;


    public void Initialize(GameObject emptyCard, int gridSize){
        this.gridSize = gridSize;
        for (int i = 0; i < gridSize*gridSize; i++)
        {
            GameObject newCard = Instantiate(emptyCard, this.transform);
            cardsOnGrid.Add(newCard.GetComponent<Card>());
            newCard.GetComponent<Card>().ownerID = 0;
            newCard.GetComponent<Card>().originalParent = this.transform;
        }

        ReBuildColorMatrix();
    }

    public void MakeCardsPlacable(){
        foreach (Card card in cardsOnGrid)
        {
            card.MakePlacable();
        }
    }
    public void MakeCardsPlacable(Card excludedCard){
        foreach (Card card in cardsOnGrid)
        {
            if(card != excludedCard){
                card.MakePlacable();
            }
        }
    }
    public void MakeCardsGrabbable(){
        foreach (Card card in cardsOnGrid)
        {
            card.MakeGrabbable();
        }
    }
    public void makeCardsInactive(){
        foreach (Card card in cardsOnGrid)
        {
            card.MakeInactive();
        }
    }

    void PrintMatrix(){
        Debug.Log("----------------------------------");
        string output = "\n";
        for (int row = 0; row < gridSize*2; row++)
        {
            for (int col = 0; col < gridSize*2; col++)
            {
                output += (gridValues[row,col].value + "  ");
            }
            output += "\n"; 
        }
        Debug.Log(output);
    }

    public void ReBuildColorMatrix(){
        gridValues = new GridCell[gridSize*2,gridSize*2];

        for (int row = 0; row < gridSize*2; row++)
        {
            for (int col = 0; col < gridSize*2; col++)
            {
                switch(row){
                    case 0:
                        switch(col){
                            case 0: gridValues[row,col] = new GridCell(cardsOnGrid[0].colorTopLeft, 0); break;
                            case 1: gridValues[row,col] = new GridCell(cardsOnGrid[0].colorTopRight, 0); break;

                            case 2: gridValues[row,col] = new GridCell(cardsOnGrid[1].colorTopLeft, 1); break;
                            case 3: gridValues[row,col] = new GridCell(cardsOnGrid[1].colorTopRight, 1); break;

                            case 4: gridValues[row,col] = new GridCell(cardsOnGrid[2].colorTopLeft, 2); break;
                            case 5: gridValues[row,col] = new GridCell(cardsOnGrid[2].colorTopRight, 2); break;

                            case 6: gridValues[row,col] = new GridCell(cardsOnGrid[3].colorTopLeft, 3); break;
                            case 7: gridValues[row,col] = new GridCell(cardsOnGrid[3].colorTopRight, 3); break;
                        }
                        break;
                    case 1:
                        switch(col){
                            case 0: gridValues[row,col] = new GridCell(cardsOnGrid[0].colorBottomLeft, 0); break;
                            case 1: gridValues[row,col] = new GridCell(cardsOnGrid[0].colorBottomRight, 0); break;

                            case 2: gridValues[row,col] = new GridCell(cardsOnGrid[1].colorBottomLeft, 1); break;
                            case 3: gridValues[row,col] = new GridCell(cardsOnGrid[1].colorBottomRight, 1); break;

                            case 4: gridValues[row,col] = new GridCell(cardsOnGrid[2].colorBottomLeft, 2); break;
                            case 5: gridValues[row,col] = new GridCell(cardsOnGrid[2].colorBottomRight, 2); break;

                            case 6: gridValues[row,col] = new GridCell(cardsOnGrid[3].colorBottomLeft, 3); break;
                            case 7: gridValues[row,col] = new GridCell(cardsOnGrid[3].colorBottomRight, 3); break;
                        }
                        break;
                    case 2:
                        switch(col){
                            case 0: gridValues[row,col] = new GridCell(cardsOnGrid[4].colorTopLeft, 4); break;
                            case 1: gridValues[row,col] = new GridCell(cardsOnGrid[4].colorTopRight, 4); break;

                            case 2: gridValues[row,col] = new GridCell(cardsOnGrid[5].colorTopLeft, 5); break;
                            case 3: gridValues[row,col] = new GridCell(cardsOnGrid[5].colorTopRight, 5); break;

                            case 4: gridValues[row,col] = new GridCell(cardsOnGrid[6].colorTopLeft, 6); break;
                            case 5: gridValues[row,col] = new GridCell(cardsOnGrid[6].colorTopRight, 6); break;

                            case 6: gridValues[row,col] = new GridCell(cardsOnGrid[7].colorTopLeft, 7); break;
                            case 7: gridValues[row,col] = new GridCell(cardsOnGrid[7].colorTopRight, 7); break;
                        }
                        break;
                    case 3:
                        switch(col){
                            case 0: gridValues[row,col] = new GridCell(cardsOnGrid[4].colorBottomLeft, 4); break;
                            case 1: gridValues[row,col] = new GridCell(cardsOnGrid[4].colorBottomRight, 4); break;

                            case 2: gridValues[row,col] = new GridCell(cardsOnGrid[5].colorBottomLeft, 5); break;
                            case 3: gridValues[row,col] = new GridCell(cardsOnGrid[5].colorBottomRight, 5); break;

                            case 4: gridValues[row,col] = new GridCell(cardsOnGrid[6].colorBottomLeft, 6); break;
                            case 5: gridValues[row,col] = new GridCell(cardsOnGrid[6].colorBottomRight, 6); break;

                            case 6: gridValues[row,col] = new GridCell(cardsOnGrid[7].colorBottomLeft, 7); break;
                            case 7: gridValues[row,col] = new GridCell(cardsOnGrid[7].colorBottomRight, 7); break;
                        }
                        break;
                    case 4:
                        switch(col){
                            case 0: gridValues[row,col] = new GridCell(cardsOnGrid[8].colorTopLeft, 8); break;
                            case 1: gridValues[row,col] = new GridCell(cardsOnGrid[8].colorTopRight, 8); break;

                            case 2: gridValues[row,col] = new GridCell(cardsOnGrid[9].colorTopLeft, 9); break;
                            case 3: gridValues[row,col] = new GridCell(cardsOnGrid[9].colorTopRight, 9); break;

                            case 4: gridValues[row,col] = new GridCell(cardsOnGrid[10].colorTopLeft, 10); break;
                            case 5: gridValues[row,col] = new GridCell(cardsOnGrid[10].colorTopRight, 10); break;

                            case 6: gridValues[row,col] = new GridCell(cardsOnGrid[11].colorTopLeft, 11); break;
                            case 7: gridValues[row,col] = new GridCell(cardsOnGrid[11].colorTopRight, 11); break;
                        }
                        break;
                    case 5:
                        switch(col){
                            case 0: gridValues[row,col] = new GridCell(cardsOnGrid[8].colorBottomLeft, 8); break;
                            case 1: gridValues[row,col] = new GridCell(cardsOnGrid[8].colorBottomRight, 8); break;

                            case 2: gridValues[row,col] = new GridCell(cardsOnGrid[9].colorBottomLeft, 9); break;
                            case 3: gridValues[row,col] = new GridCell(cardsOnGrid[9].colorBottomRight, 9); break;

                            case 4: gridValues[row,col] = new GridCell(cardsOnGrid[10].colorBottomLeft, 10); break;
                            case 5: gridValues[row,col] = new GridCell(cardsOnGrid[10].colorBottomRight, 10); break;

                            case 6: gridValues[row,col] = new GridCell(cardsOnGrid[11].colorBottomLeft, 11); break;
                            case 7: gridValues[row,col] = new GridCell(cardsOnGrid[11].colorBottomRight, 11); break;
                        }
                        break;
                    case 6:
                        switch(col){
                            case 0: gridValues[row,col] = new GridCell(cardsOnGrid[12].colorTopLeft, 12); break;
                            case 1: gridValues[row,col] = new GridCell(cardsOnGrid[12].colorTopRight, 12); break;

                            case 2: gridValues[row,col] = new GridCell(cardsOnGrid[13].colorTopLeft, 13); break;
                            case 3: gridValues[row,col] = new GridCell(cardsOnGrid[13].colorTopRight, 13); break;

                            case 4: gridValues[row,col] = new GridCell(cardsOnGrid[14].colorTopLeft, 14); break;
                            case 5: gridValues[row,col] = new GridCell(cardsOnGrid[14].colorTopRight, 14); break;

                            case 6: gridValues[row,col] = new GridCell(cardsOnGrid[15].colorTopLeft, 15); break;
                            case 7: gridValues[row,col] = new GridCell(cardsOnGrid[15].colorTopRight, 15); break;
                        }
                        break;
                    case 7:
                        switch(col){
                            case 0: gridValues[row,col] = new GridCell(cardsOnGrid[12].colorBottomLeft, 12); break;
                            case 1: gridValues[row,col] = new GridCell(cardsOnGrid[12].colorBottomRight, 12); break;

                            case 2: gridValues[row,col] = new GridCell(cardsOnGrid[13].colorBottomLeft, 13); break;
                            case 3: gridValues[row,col] = new GridCell(cardsOnGrid[13].colorBottomRight, 13); break;

                            case 4: gridValues[row,col] = new GridCell(cardsOnGrid[14].colorBottomLeft, 14); break;
                            case 5: gridValues[row,col] = new GridCell(cardsOnGrid[14].colorBottomRight, 14); break;

                            case 6: gridValues[row,col] = new GridCell(cardsOnGrid[15].colorBottomLeft, 15); break;
                            case 7: gridValues[row,col] = new GridCell(cardsOnGrid[15].colorBottomRight, 15); break;
                        }
                        break;
                    default:
                        break;
                    
                }
            }
        }
        PrintMatrix();
    }

    public void Reset(){
        foreach(Card card in GetComponentsInChildren<Card>()){
            Destroy(card.gameObject);
            cardsOnGrid.Clear();
        }
    }

    List<Vector2Int> visitedCells = new List<Vector2Int>();
    public List<GridCell> matchingCells = new List<GridCell>();
    public List<Card> matchingCards = new List<Card> ();
    bool bridgeFound;
    public int bridgeOwner;
    public int bridgePoints;
    int matrixSize;

    public bool SearchBridges(){
        visitedCells.Clear();
        matchingCells.Clear();
        bridgeFound = false;
        matrixSize = gridSize*2;

        for (int i = 0; i < gridSize*2; i++)
        {
            FindBridgesDown(1, new Vector2Int(0,i));
            if (bridgeFound){
                bridgeOwner = 1;
                break;
            }
            visitedCells.Clear();
            matchingCells.Clear();
            FindBridgesSide(1, new Vector2Int(0,i));
            if (bridgeFound){
                bridgeOwner = 1;
                break;
            }
            visitedCells.Clear();
            matchingCells.Clear();
            FindBridgesDown(2, new Vector2Int(0,i));
            if (bridgeFound){
                bridgeOwner=2;
                break;
            }
            visitedCells.Clear();
            matchingCells.Clear();
            FindBridgesSide(2, new Vector2Int(0,i));
            if (bridgeFound){
                bridgeOwner = 2;
                break;
            }
            visitedCells.Clear();
            matchingCells.Clear();
        }

        if (bridgeFound) {
            foreach (GridCell cell in matchingCells)
            {
                if (!matchingCards.Contains(cardsOnGrid[cell.owner]))
                {
                    matchingCards.Add(cardsOnGrid[cell.owner]);

                }
            }
            bridgePoints = matchingCells.Count;
        }
        return bridgeFound;
    }

    void FindBridgesDown(int color, Vector2Int pos){
        visitedCells.Add(pos);

        if (gridValues[pos.x, pos.y].value == color){
            matchingCells.Add(gridValues[pos.x,pos.y]);
            if(pos.x == matrixSize-1){
                bridgeFound = true;
                bridgeOwner = color;
            }

            Vector2Int upNeighbor = new Vector2Int(pos.x-1, pos.y);
            if(upNeighbor.x >= 0 && !visitedCells.Contains(upNeighbor)){
                FindBridgesDown(color, upNeighbor);
            }
            Vector2Int rightNeighbor = new Vector2Int(pos.x, pos.y+1);
            if(rightNeighbor.y <= matrixSize-1 && !visitedCells.Contains(rightNeighbor)){
                FindBridgesDown(color,rightNeighbor);
            }
            Vector2Int downNeighbor = new Vector2Int(pos.x+1, pos.y);
            if(downNeighbor.x <= matrixSize-1 && !visitedCells.Contains(downNeighbor)){
                FindBridgesDown(color,downNeighbor);
            }
            Vector2Int leftNeighbor = new Vector2Int(pos.x, pos.y-1);
            if(leftNeighbor.y >= 0 && !visitedCells.Contains(leftNeighbor)){
                FindBridgesDown(color,leftNeighbor);
            }
        }
    }
    void FindBridgesSide(int color, Vector2Int pos){
        visitedCells.Add(pos);

        if (gridValues[pos.x, pos.y].value == color){
            matchingCells.Add(gridValues[pos.x,pos.y]);
            if(pos.y== matrixSize-1){
                bridgeFound = true;
                bridgeOwner = color;
            }

            Vector2Int upNeighbor = new Vector2Int(pos.x-1, pos.y);
            if(upNeighbor.x >= 0 && !visitedCells.Contains(upNeighbor)){
                FindBridgesSide(color, upNeighbor);
            }
            Vector2Int rightNeighbor = new Vector2Int(pos.x, pos.y+1);
            if(rightNeighbor.y <= matrixSize-1 && !visitedCells.Contains(rightNeighbor)){
                FindBridgesSide(color,rightNeighbor);
            }
            Vector2Int downNeighbor = new Vector2Int(pos.x+1, pos.y);
            if(downNeighbor.x <= matrixSize-1 && !visitedCells.Contains(downNeighbor)){
                FindBridgesSide(color,downNeighbor);
            }
            Vector2Int leftNeighbor = new Vector2Int(pos.x, pos.y-1);
            if(leftNeighbor.y >= 0 && !visitedCells.Contains(leftNeighbor)){
                FindBridgesSide(color,leftNeighbor);
            }
        }
    }

    public IEnumerator MarkMatchingCards()
    {
        foreach(Card card in matchingCards)
        {
            card.MarkBridge();
        }
        yield return new WaitForSeconds(1.5f);
        foreach (Card card in matchingCards)
        {
            card.MakeEmpty();
        }
        matchingCards.Clear();
        matchingCells.Clear();
    }

}

public class GridCell
{
    public int value;
    public int owner;

    public GridCell(int value, int owner){
        this.value = value;
        this.owner = owner;
    }
}
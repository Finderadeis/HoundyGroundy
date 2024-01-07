using System;
using System.Collections.Generic;

public class M_Board
{
    int gridSize;
    M_Field[,] fields;
    M_GridCell[,] gridValues;
    List<M_Field> bridgeFields;

    internal void Initialize(int size)
    {
        gridSize = size;
        fields = new M_Field[size, size];

        RefreshGridValues();
    }

    internal void Reset()
    {
        foreach(M_Field field in fields)
        {
            field.occupyingCard = null;
        }
        fields = new M_Field[4, 4];
        RefreshGridValues();
    }

    private void RefreshGridValues()
    {
        gridValues = new M_GridCell[gridSize * 2, gridSize * 2];

        for (int row = 0; row < gridSize * 2; row++)
        {
            for (int col = 0; col < gridSize * 2; col++)
            {
                switch (row)
                {
                    case 0:
                        switch (col)
                        {
                            case 0: gridValues[row, col] = new M_GridCell(fields[0, 0].GetColorTopLeft(), fields[0, 0].GetOwnerID()); break;
                            case 1: gridValues[row, col] = new M_GridCell(fields[0, 0].GetColorTopRight(), fields[0, 0].GetOwnerID()); break;

                            case 2: gridValues[row, col] = new M_GridCell(fields[0, 1].GetColorTopLeft(), fields[0, 1].GetOwnerID()); break;
                            case 3: gridValues[row, col] = new M_GridCell(fields[0, 1].GetColorTopRight(), fields[0, 1].GetOwnerID()); break;

                            case 4: gridValues[row, col] = new M_GridCell(fields[0, 2].GetColorTopLeft(), fields[0, 2].GetOwnerID()); break;
                            case 5: gridValues[row, col] = new M_GridCell(fields[0, 2].GetColorTopRight(), fields[0, 2].GetOwnerID()); break;

                            case 6: gridValues[row, col] = new M_GridCell(fields[0, 3].GetColorTopLeft(), fields[0, 3].GetOwnerID()); break;
                            case 7: gridValues[row, col] = new M_GridCell(fields[0, 3].GetColorTopRight(), fields[0, 3].GetOwnerID()); break;
                        }
                        break;
                    case 1:
                        switch (col)
                        {
                            case 0: gridValues[row, col] = new M_GridCell(fields[0, 0].GetColorBottomLeft(), fields[0, 0].GetOwnerID()); break;
                            case 1: gridValues[row, col] = new M_GridCell(fields[0, 0].GetColorBottomRight(), fields[0, 0].GetOwnerID()); break;

                            case 2: gridValues[row, col] = new M_GridCell(fields[0, 1].GetColorBottomLeft(), fields[0, 1].GetOwnerID()); break;
                            case 3: gridValues[row, col] = new M_GridCell(fields[0, 1].GetColorBottomRight(), fields[0, 1].GetOwnerID()); break;

                            case 4: gridValues[row, col] = new M_GridCell(fields[0, 2].GetColorBottomLeft(), fields[0, 2].GetOwnerID()); break;
                            case 5: gridValues[row, col] = new M_GridCell(fields[0, 2].GetColorBottomRight(), fields[0, 2].GetOwnerID()); break;

                            case 6: gridValues[row, col] = new M_GridCell(fields[0, 3].GetColorBottomLeft(), fields[0, 3].GetOwnerID()); break;
                            case 7: gridValues[row, col] = new M_GridCell(fields[0, 3].GetColorBottomRight(), fields[0, 3].GetOwnerID()); break;
                        }
                        break;
                    case 2:
                        switch (col)
                        {
                            case 0: gridValues[row, col] = new M_GridCell(fields[1, 0].GetColorTopLeft(), fields[1, 0].GetOwnerID()); break;
                            case 1: gridValues[row, col] = new M_GridCell(fields[1, 0].GetColorTopRight(), fields[1, 0].GetOwnerID()); break;

                            case 2: gridValues[row, col] = new M_GridCell(fields[1, 1].GetColorTopLeft(), fields[1, 1].GetOwnerID()); break;
                            case 3: gridValues[row, col] = new M_GridCell(fields[1, 1].GetColorTopRight(), fields[1, 1].GetOwnerID()); break;

                            case 4: gridValues[row, col] = new M_GridCell(fields[1, 2].GetColorTopLeft(), fields[1, 2].GetOwnerID()); break;
                            case 5: gridValues[row, col] = new M_GridCell(fields[1, 2].GetColorTopRight(), fields[1, 2].GetOwnerID()); break;

                            case 6: gridValues[row, col] = new M_GridCell(fields[1, 3].GetColorTopLeft(), fields[1, 3].GetOwnerID()); break;
                            case 7: gridValues[row, col] = new M_GridCell(fields[1, 3].GetColorTopRight(), fields[1, 3].GetOwnerID()); break;
                        }
                        break;
                    case 3:
                        switch (col)
                        {
                            case 0: gridValues[row, col] = new M_GridCell(fields[1, 0].GetColorBottomLeft(), fields[1, 0].GetOwnerID()); break;
                            case 1: gridValues[row, col] = new M_GridCell(fields[1, 0].GetColorBottomRight(), fields[1, 0].GetOwnerID()); break;
                                                                                 
                            case 2: gridValues[row, col] = new M_GridCell(fields[1, 1].GetColorBottomLeft(), fields[1, 1].GetOwnerID()); break;
                            case 3: gridValues[row, col] = new M_GridCell(fields[1, 1].GetColorBottomRight(), fields[1, 1].GetOwnerID()); break;
                                                                                 
                            case 4: gridValues[row, col] = new M_GridCell(fields[1, 2].GetColorBottomLeft(), fields[1, 2].GetOwnerID()); break;
                            case 5: gridValues[row, col] = new M_GridCell(fields[1, 2].GetColorBottomRight(), fields[1, 2].GetOwnerID()); break;
                                                                                 
                            case 6: gridValues[row, col] = new M_GridCell(fields[1, 3].GetColorBottomLeft(), fields[1, 3].GetOwnerID()); break;
                            case 7: gridValues[row, col] = new M_GridCell(fields[1, 3].GetColorBottomRight(), fields[1, 3].GetOwnerID()); break;
                        }
                        break;
                    case 4:
                        switch (col)
                        {
                            case 0: gridValues[row, col] = new M_GridCell(fields[2, 0].GetColorTopLeft(), fields[2, 0].GetOwnerID()); break;
                            case 1: gridValues[row, col] = new M_GridCell(fields[2, 0].GetColorTopRight(), fields[2, 0].GetOwnerID()); break;

                            case 2: gridValues[row, col] = new M_GridCell(fields[2, 1].GetColorTopLeft(), fields[2, 1].GetOwnerID()); break;
                            case 3: gridValues[row, col] = new M_GridCell(fields[2, 1].GetColorTopRight(), fields[2, 1].GetOwnerID()); break;

                            case 4: gridValues[row, col] = new M_GridCell(fields[2, 2].GetColorTopLeft(), fields[2, 2].GetOwnerID()); break;
                            case 5: gridValues[row, col] = new M_GridCell(fields[2, 2].GetColorTopRight(), fields[2, 2].GetOwnerID()); break;

                            case 6: gridValues[row, col] = new M_GridCell(fields[2, 3].GetColorTopLeft(), fields[2, 3].GetOwnerID()); break;
                            case 7: gridValues[row, col] = new M_GridCell(fields[2, 3].GetColorTopRight(), fields[2, 3].GetOwnerID()); break;
                        }
                        break;
                    case 5:
                        switch (col)
                        {
                            case 0: gridValues[row, col] = new M_GridCell(fields[2, 0].GetColorBottomLeft(), fields[2, 0].GetOwnerID()); break;
                            case 1: gridValues[row, col] = new M_GridCell(fields[2, 0].GetColorBottomRight(), fields[2, 0].GetOwnerID()); break;

                            case 2: gridValues[row, col] = new M_GridCell(fields[2, 1].GetColorBottomLeft(), fields[2, 1].GetOwnerID()); break;
                            case 3: gridValues[row, col] = new M_GridCell(fields[2, 1].GetColorBottomRight(), fields[2, 1].GetOwnerID()); break;

                            case 4: gridValues[row, col] = new M_GridCell(fields[2, 2].GetColorBottomLeft(), fields[2, 2].GetOwnerID()); break;
                            case 5: gridValues[row, col] = new M_GridCell(fields[2, 2].GetColorBottomRight(), fields[2, 2].GetOwnerID()); break;

                            case 6: gridValues[row, col] = new M_GridCell(fields[2, 3].GetColorBottomLeft(), fields[2, 3].GetOwnerID()); break;
                            case 7: gridValues[row, col] = new M_GridCell(fields[2, 3].GetColorBottomRight(), fields[2, 3].GetOwnerID()); break;
                        }
                        break;
                    case 6:
                        switch (col)
                        {
                            case 0: gridValues[row, col] = new M_GridCell(fields[3, 0].GetColorTopLeft(), fields[3, 0].GetOwnerID()); break;
                            case 1: gridValues[row, col] = new M_GridCell(fields[3, 0].GetColorTopRight(), fields[3, 0].GetOwnerID()); break;

                            case 2: gridValues[row, col] = new M_GridCell(fields[3, 1].GetColorTopLeft(), fields[3, 1].GetOwnerID()); break;
                            case 3: gridValues[row, col] = new M_GridCell(fields[3, 1].GetColorTopRight(), fields[3, 1].GetOwnerID()); break;

                            case 4: gridValues[row, col] = new M_GridCell(fields[3, 2].GetColorTopLeft(), fields[3, 2].GetOwnerID()); break;
                            case 5: gridValues[row, col] = new M_GridCell(fields[3, 2].GetColorTopRight(), fields[3, 2].GetOwnerID()); break;

                            case 6: gridValues[row, col] = new M_GridCell(fields[3, 3].GetColorTopLeft(), fields[3, 3].GetOwnerID()); break;
                            case 7: gridValues[row, col] = new M_GridCell(fields[3, 3].GetColorTopRight(), fields[3, 3].GetOwnerID()); break;
                        }
                        break;
                    case 7:
                        switch (col)
                        {
                            case 0: gridValues[row, col] = new M_GridCell(fields[3, 0].GetColorBottomLeft(), fields[3, 0].GetOwnerID()); break;
                            case 1: gridValues[row, col] = new M_GridCell(fields[3, 0].GetColorBottomRight(), fields[3, 0].GetOwnerID()); break;

                            case 2: gridValues[row, col] = new M_GridCell(fields[3, 1].GetColorBottomLeft(), fields[3, 1].GetOwnerID()); break;
                            case 3: gridValues[row, col] = new M_GridCell(fields[3, 1].GetColorBottomRight(), fields[3, 1].GetOwnerID()); break;

                            case 4: gridValues[row, col] = new M_GridCell(fields[3, 2].GetColorBottomLeft(), fields[3, 2].GetOwnerID()); break;
                            case 5: gridValues[row, col] = new M_GridCell(fields[3, 2].GetColorBottomRight(), fields[3, 2].GetOwnerID()); break;

                            case 6: gridValues[row, col] = new M_GridCell(fields[3, 3].GetColorBottomLeft(), fields[3, 3].GetOwnerID()); break;
                            case 7: gridValues[row, col] = new M_GridCell(fields[3, 3].GetColorBottomRight(), fields[3, 3].GetOwnerID()); break;
                        }
                        break;
                    default:
                        break;

                }
            }
        }
    }

    internal int FindBridges(int player)
    {
        int bridgesFound = FindBridgesRecursive(0, 0, player);
        return bridgesFound;
    }

    private int FindBridgesRecursive(int row, int col, int value)
    {
        // recursive function to find paths in gridValues that are bridges, meaning they connect two sides of the board with a path of same values
        // returns the number of bridges found
        int bridgesFound = 0;
        if (gridValues[row, col].value == value)
        {
            gridValues[row, col].value = -1;
            if (row == 0 || row == gridSize * 2 - 1 || col == 0 || col == gridSize * 2 - 1)
            {
                bridgesFound++;
                bridgeFields.Add(fields[row / 2, col / 2]);
            }
            if (row > 0)
            {
                bridgesFound += FindBridgesRecursive(row - 1, col, value);
            }
            if (row < gridSize * 2 - 1)
            {
                bridgesFound += FindBridgesRecursive(row + 1, col, value);
            }
            if (col > 0)
            {
                bridgesFound += FindBridgesRecursive(row, col - 1, value);
            }
            if (col < gridSize * 2 - 1)
            {
                bridgesFound += FindBridgesRecursive(row, col + 1, value);
            }
        }
        return bridgesFound;
    }

    public List<M_Field> GetBridgeFields()
    {
        return bridgeFields;
    }

    internal void WaitForMove()
    {
        // for now, nothing to do here
    }

    internal void WaitForPlace()
    {
        // for now, nothing to do here
    }

    internal void PlaceCard(M_Card m_Card, M_Field target)
    {
        target.occupyingCard = m_Card;
    }

    internal void MoveCard(M_Field source, M_Field target)
    {
        target.occupyingCard = source.occupyingCard;
        target.occupyingCard.StartCooldown();
        source.occupyingCard = null;
    }
}
using System;

public class M_Card
{
    internal int ownerID;
    public int colorTopLeft;
    public int colorTopRight;
    public int colorBottomLeft;
    public int colorBottomRight;
    int cooldown;
    enum CardState
    {
        INACTIVE, ONCOOLDOWN, GRABBABLE, PLACABLE
    }
    CardState currentCardState;

    public M_Card(int tl, int tr, int bl, int br)
    {
        colorTopLeft = tl;
        colorTopRight = tr;
        colorBottomLeft = bl;
        colorBottomRight = br;
        currentCardState = CardState.INACTIVE;
    }

    public void MakeInactive()
    {
        if (currentCardState == CardState.ONCOOLDOWN)
        {
            return;
        }
        currentCardState = CardState.INACTIVE;
    }
    public void MakeGrabbable()
    {
        if (currentCardState == CardState.ONCOOLDOWN)
        {
            return;
        }
        currentCardState = CardState.GRABBABLE;
    }
    public void MakePlacable()
    {
        if (currentCardState == CardState.ONCOOLDOWN)
        {
            return;
        }
        currentCardState = CardState.PLACABLE;
    }

    public void TurnCard()
    {
        int tmp = colorTopLeft;

        colorTopLeft = colorBottomLeft;
        colorBottomLeft = colorBottomRight;
        colorBottomRight = colorTopRight;
        colorTopRight = tmp;
    }

    public void StartCooldown()
    {
        cooldown = 2;
        currentCardState = CardState.ONCOOLDOWN;
        M_Game.Instance.AddCardToCooldownStack(this);
    }

    internal void reduceCooldown()
    {
        cooldown -= 1;
        if(cooldown == 0)
        {
            currentCardState = CardState.INACTIVE;
            M_Game.Instance.RemoveCardFromCooldownStack(this);
        }
    }
}
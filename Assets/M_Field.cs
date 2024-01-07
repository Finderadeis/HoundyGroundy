public class M_Field
{
    internal M_Card occupyingCard;

    public M_Field()
    {
        occupyingCard = null;
    }

    public int GetOwnerID()
    {
        if (occupyingCard == null)
        {
            return -1;
        }
        return occupyingCard.ownerID;
    }

    public int GetColorTopLeft()
    {
        if (occupyingCard == null)
        {
            return -1;
        }
        return occupyingCard.colorTopLeft;
    }
    public int GetColorTopRight()
    {
        if (occupyingCard == null)
        {
            return -1;
        }
        return occupyingCard.colorTopRight;
    }
    public int GetColorBottomLeft()
    {
        if (occupyingCard == null)
        {
            return -1;
        }
        return occupyingCard.colorBottomLeft;
    }
    public int GetColorBottomRight()
    {
        if (occupyingCard == null)
        {
            return -1;
        }
        return occupyingCard.colorBottomRight;
    }
}
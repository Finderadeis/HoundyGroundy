using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Color hoverColorGrabbable;
    [SerializeField] Color hoverColorPlacable;
    [SerializeField] Color hoverColorUnplacable;
    [SerializeField] Color dragColor;
    [SerializeField] Sprite emptyCardImage;
    Sprite currentCardImage;

    public int colorTopLeft, colorTopRight, colorBottomLeft, colorBottomRight;
    public int cardID;
    public int ownerID;
    int owningPlayer = 0;
    int coolDown = 0;
    public Transform originalParent;
    Vector3 originalPosition;
    RectTransform rect;
    Image img;
    enum CardState
    {
        INACTIVE, ONCOOLDOWN, GRABBABLE, PLACABLE
    }
    CardState currentCardState;


    private void Awake() {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>(); 
        currentCardImage = img.sprite;
        currentCardState = CardState.INACTIVE;
    }

    public void MakeEmpty(){
        colorTopLeft = 0;
        colorTopRight = 0;
        colorBottomLeft = 0;
        colorBottomRight = 0;
        ownerID = 0;
        coolDown = 0;
        currentCardImage = emptyCardImage;
        img.sprite = currentCardImage;
        cardID = 0;
    }

    public void ChangeCard(Card newCard){
        cardID = newCard.cardID;
        colorTopLeft = newCard.colorTopLeft;
        colorTopRight = newCard.colorTopRight;
        colorBottomLeft = newCard.colorBottomLeft;
        colorBottomRight = newCard.colorBottomRight;
        currentCardImage = newCard.currentCardImage;
        img.sprite = currentCardImage;
        coolDown = 2;
    }

    public void PutBack(){
        transform.localPosition = originalPosition;
    }
    public void MakeInactive(){
        currentCardState = CardState.INACTIVE;
    }
    public void MakeGrabbable(){
        currentCardState = CardState.GRABBABLE;
    }
    public void MakePlacable(){
        currentCardState = CardState.PLACABLE;
    }

    void TurnCard(){
        int tmp = colorTopLeft;
        colorTopLeft = colorTopRight;
        colorTopRight = colorBottomRight;
        colorBottomRight = colorBottomLeft;
        colorBottomLeft = tmp;
        rect.Rotate(new Vector3(0,0,-90));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (currentCardState)
        {
            case CardState.GRABBABLE:
                img.color = hoverColorGrabbable;
                break;
            case CardState.PLACABLE:
                img.color = hoverColorPlacable;
                GameManager.instance.hoveredCard = this.gameObject;
                break;
            default:
                return;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.color = Color.white;
        GameManager.instance.hoveredCard = null;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(currentCardState == CardState.GRABBABLE){
            transform.position = Input.mousePosition;
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(currentCardState == CardState.GRABBABLE){
            GameManager.instance.ProcessCardGrab(this.gameObject);
            img.color = dragColor;
            img.raycastTarget = false;
            originalPosition = transform.localPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(currentCardState == CardState.GRABBABLE){
            img.color = Color.white;
            img.raycastTarget = true;
            GameManager.instance.ProcessCardDrop();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(currentCardState == CardState.GRABBABLE && GameManager.instance.currentGameState == GameManager.GameState.MOVE){
            TurnCard();
        }
    }
}

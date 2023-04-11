using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Color hoverColorGrabbable;
    [SerializeField] Color hoverColorPlacable;
    [SerializeField] Color hoverColorUnplacable;
    [SerializeField] Color dragColor;
    [SerializeField] Color cooldownColor;
    [SerializeField] Sprite emptyCardImage;
    [SerializeField] TextMeshProUGUI cooldownDisplay;
    Sprite currentCardImage;

    public int colorTopLeft, colorTopRight, colorBottomLeft, colorBottomRight;
    [SerializeField] TextMeshProUGUI tL, tR, bL, bR;
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
    int rotationValue = 0;


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
        tL.text = colorTopLeft.ToString();
        tR.text = colorTopRight.ToString();
        bL.text = colorBottomLeft.ToString();
        bR.text = colorBottomRight.ToString();
        ownerID = 0;
        coolDown = 0;
        currentCardImage = emptyCardImage;
        img.sprite = currentCardImage;
        cardID = 0;
        cooldownDisplay.gameObject.SetActive(false);
        currentCardState = CardState.INACTIVE;
        img.color = Color.white;
        GameManager.instance.RemoveCardFromCoolDownStack(this);
    }

    public void ChangeCard(Card newCard){
        cardID = newCard.cardID;
        colorTopLeft = newCard.colorTopLeft;
        colorTopRight = newCard.colorTopRight;
        colorBottomLeft = newCard.colorBottomLeft;
        colorBottomRight = newCard.colorBottomRight;
        tL.text = colorTopLeft.ToString();
        tR.text = colorTopRight.ToString();
        bL.text = colorBottomLeft.ToString();
        bR.text = colorBottomRight.ToString();
        currentCardImage = newCard.currentCardImage;
        img.sprite = currentCardImage;
        coolDown = 2;
        rotationValue = newCard.rotationValue;
        rect.Rotate(new Vector3(0,0,rotationValue));
        GameManager.instance.playGrid.ReBuildColorMatrix();
        StartCooldown();
    }

    public void PutBack(){
        transform.localPosition = originalPosition;
    }
    public void MakeInactive(){
        if(currentCardState == CardState.ONCOOLDOWN){
            return;
        }
        currentCardState = CardState.INACTIVE;
    }
    public void MakeGrabbable(){
        if(currentCardState == CardState.ONCOOLDOWN){
            return;
        }
        currentCardState = CardState.GRABBABLE;
    }
    public void MakePlacable(){
        if(currentCardState == CardState.ONCOOLDOWN){
            return;
        }
        currentCardState = CardState.PLACABLE;
    }

    void TurnCard(){
        int tmp = colorTopLeft;

        colorTopLeft = colorBottomLeft;
        colorBottomLeft = colorBottomRight;
        colorBottomRight = colorTopRight;
        colorTopRight = tmp;

        tL.text = "tl "+colorTopLeft.ToString();
        tR.text = "tr " + colorTopRight.ToString();
        bL.text = "bl " + colorBottomLeft.ToString();
        bR.text = "br " + colorBottomRight.ToString();
        rect.Rotate(new Vector3(0,0,-90));
        rotationValue -= 90;
        GameManager.instance.playGrid.ReBuildColorMatrix();
        if(GameManager.instance.currentGameState == GameManager.GameState.MOVE)
        {
            StartCooldown();
        }
    }

    public void StartCooldown(){
        coolDown = 2;
        cooldownDisplay.gameObject.SetActive(true);
        cooldownDisplay.text = coolDown.ToString();
        currentCardState = CardState.ONCOOLDOWN;
        img.color = cooldownColor;
        GameManager.instance.AddCardToCoolDownStack(this);
    }

    public void ReduceCooldown(){
        coolDown -= 1;

        if(coolDown==0){
            cooldownDisplay.gameObject.SetActive(false);
            currentCardState = CardState.INACTIVE;
            img.color = Color.white;
            GameManager.instance.RemoveCardFromCoolDownStack(this);
        }
        else{
            cooldownDisplay.text = coolDown.ToString();
        }
    }

    public void MarkBridge()
    {
        img.color = Color.red;
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
        if(currentCardState == CardState.ONCOOLDOWN){
            return;
        }
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
        switch (GameManager.instance.currentGameState)
        {
            case GameManager.GameState.PLACE:
            case GameManager.GameState.MOVE:
                if(currentCardState == CardState.GRABBABLE){
                    TurnCard();
                }
                break;
            default:
                break;
        }
        
    }
}

using UnityEngine.EventSystems;
using UnityEngine;

public class UIEventTap : MonoBehaviour, IPointerClickHandler ,IPointerUpHandler
{
    PlayerController playerController;
    Vector2 distance;
    private bool isClick;
    public bool isMainWeeapon;
    RectTransform position;
    private void Awake() {
        playerController = FindObjectOfType<PlayerController>();
        position = GetComponent<RectTransform>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isClick = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if(isClick)
        {
            distance = Camera.main.ScreenToWorldPoint(eventData.position) - position.position;
            Debug.Log(distance);
            if(Mathf.Abs(distance.x) + Mathf.Abs(distance.y) > 1)
            {
                playerController.RemoveWeapon(isMainWeeapon,playerController.mainWeapon.typeCartridges);
            }   
        }
    isClick = true;
    }
}

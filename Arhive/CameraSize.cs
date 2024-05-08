using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Arhive {

public class CameraSize : MonoBehaviour {
 public Player player;
   public Button button;
 public Camera cam;
 float size;
 public enum CameraSizeButon{X1,X2,X4,X8,X15};
 public CameraSizeButon cameraSizeButon;
 public PlayerController playerC;
 public bool acceptedButton;
 public int idButton;
 
 void Start() {
   switch(cameraSizeButon)
   {
     case CameraSizeButon.X1:
     size = player.x1;
     break;
     case CameraSizeButon.X2:
     size = player.x2;
     break;
     case CameraSizeButon.X4:
     size = player.x4;
     break;
     case CameraSizeButon.X8:
     size = player.x8;
     break;
     case CameraSizeButon.X15:
     size = player.x15;
     break;
   }
 }
  void Update() {
 }
  public void onClick()
 {
   Debug.Log("Клик");
   if(playerC.currentCamSize < size)
                {
                  playerC.checkMin = false;
                  playerC.checkMax = true;
                }
                else
                {
                playerC.checkMin = true;
                playerC.checkMax = false;
                }
                playerC.SizeCam = size;
  playerC.SizeCam = size;
  acceptedButton = true;
  player.ButtonSize(idButton);
  ButtonSize();
  if(acceptedButton)
  {
    RectTransform rButton = button.GetComponent<RectTransform>();
    rButton.localScale = new Vector3(1,1,1);
  }
 }
 public void ButtonSize()
 {
   if(acceptedButton)
  {
    RectTransform rButton = button.GetComponent<RectTransform>();
    rButton.localScale = new Vector3(1,1,1);
  }
  else if(!acceptedButton)
  {
    RectTransform rButton = button.GetComponent<RectTransform>();
    rButton.localScale = new Vector3(0.4721853f,0.4721853f,1);
  }
 }
}
}
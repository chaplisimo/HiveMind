using UnityEngine;

public class AntWorkerController : MonoBehaviour {
  
  delegate void AntCommand();
  public static AntCommand OnAntCommand;
  
  void Update(){
    if(Input.GetButton("Fire2")){
      if(OnAntCommand != null){
        OnAntCommand();
      }
    }
  }
}

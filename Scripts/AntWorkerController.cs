using UnityEngine;

public class AntWorkerController : MonoBehaviour {
  
  public delegate void AntCommand();
  public static event AntCommand OnAntCommand;
  
  void Update(){
    if(Input.GetButton("Fire2")){
      if(OnAntCommand != null){
        OnAntCommand();
      }
    }
  }
}

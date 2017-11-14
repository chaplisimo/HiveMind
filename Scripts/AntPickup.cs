using UnityEngine;

public class AntPickup : MonoBehaviour {
  
  public List<GameObject> inventory;
  
  void OnTriggerEnter(Collider other){
    if(other.CompareTag("Pickable")){
      //Direct Pickup
      inventory.Add(other.gameObject);
      other.transform.parent = gameObject.transform;
      other.gameObject.SetActive(false);
    }
  }
}

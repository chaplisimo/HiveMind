using UnityEngine;
using System.Collections.Generic;

public class AntPickup : MonoBehaviour {
  
  public List<GameObject> inventory;
  
  void OnTriggerEnter(Collider other){
    if(other.CompareTag("Pickable")){
      //Direct Pickup
      inventory.Add(other.gameObject);
      other.transform.parent = gameObject.transform;
      other.gameObject.SetActive(false);
    }else 
      if(other.CompareTag("Queen")){
		  if(inventory.Count > 0){
				other.gameObject.GetComponent<QueenController>().FeedResources(inventory.Count);
        
				foreach(GameObject gO in inventory){
				  inventory.Remove(gO);
				  Destroy(gO); 
				}
			}
      }
  }
}

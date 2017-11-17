using UnityEngine;
using System.Collections.Generic;

public class AntPickup : MonoBehaviour {
  
	public int resources = 0;
  
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Resource")){
			//Direct Pickup
			ResourceScript rs = other.GetComponent<ResourceScript>();
			resources += rs.qty;
			rs.Die();
		}else if(other.CompareTag("Queen")){
			if(resources > 0){
				other.gameObject.GetComponent<QueenController>().FeedResources(resources);
				resources = 0;
			}
		}
  }
}

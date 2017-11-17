using UnityEngine;

public class ResourceScript : MonoBehaviour {
	
	public int qty = 5;
	
	public void Die(){
		Destroy(gameObject);
	}
}

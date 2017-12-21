using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class AntMovement : MonoBehaviour {
	
	Rigidbody rb;
	
	public float speed = 4;
	public float turningRate = 30;
	
	
	bool canClimb = false;
	
	void Start(){
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate(){
		
		Vector3 newPosition;	
	
	#if UNITY_ANDROID
    
		if(Input.touchCount > 0){
			Touch touch = Input.GetTouch(0);
			
			if(touch.phase == TouchPhase.Began){
				//TODO RayCast to check if touch is in head
				
			}else if(touch.phase == TouchPhase.Moved){
				//TODO RayCast to check if touch is in head
			}else if(touch.phase == TouchPhase.Ended){
				rb.speed = 0;
			}
		}
	#else
		
		float hMove = Input.GetAxisRaw("Horizontal");
		float vMove = Input.GetAxisRaw("Vertical");
	
		newPosition = new Vector3(0f,vMove,hMove);
		newPosition *= speed * Time.deltaTime;
		
		if(newPosition.magnitude > 0){
			Quaternion rotation = Quaternion.FromToRotation(Vector3.forward,newPosition.normalized);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * turningRate);
		}
		
	#endif
		
		rb.MovePosition(newPosition + rb.position);
		//rb.MoveRotation(Quaternion.Euler(vectorRotation));		
	}
	
	void OnTriggerEnter(Collider other){
		
		/*if(other.CompareTag("Anthive")){
			canClimb = true;
		}*/
	}
	
	 void OnTriggerExit(Collider other){
		
		/*if(other.CompareTag("Anthive")){
			canClimb = false;
		}*/
	}
}

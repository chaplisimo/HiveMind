using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LiceMovement : MonoBehaviour {
	
	Rigidbody rb;
	
	public float speed = 4;
	public float turningRate = 30;
	
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
	
		newPosition = new Vector3(hMove,0f,vMove);
		newPosition *= speed * Time.deltaTime;
		
		
		Quaternion targetRotation = transform.rotation;
		Quaternion newRotation = transform.rotation;
		
		if(newPosition.magnitude != 0){
			 targetRotation = Quaternion.LookRotation(newPosition);
			 newRotation = Quaternion.RotateTowards(transform.rotation,targetRotation, turningRate * Time.deltaTime);
		}
	#endif
		
		rb.MovePosition(transform.position + newPosition);
			rb.MoveRotation(newRotation);		
	}
}

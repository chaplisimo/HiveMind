using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class AntClimbWall : MonoBehaviour {
	
	/*Rigidbody rb;
	
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
	
		newPosition = new Vector3(0f,vMove,-hMove);
		newPosition *= speed * Time.deltaTime;
		
		
		Quaternion targetRotation = transform.rotation;
		Quaternion newRotation = transform.rotation;
		
		if(newPosition.magnitude != 0){
			 targetRotation = Quaternion.LookRotation(newPosition,-Vector3.forward);
			 newRotation = Quaternion.RotateTowards(transform.rotation,targetRotation, turningRate * Time.deltaTime);
			 if(isRotating){
				newRotation *= CalculateWallRotation(vMove);
			 }
		}
	#endif
		
		rb.MovePosition(transform.position + newPosition);
		rb.MoveRotation(newRotation);		
	}
	
	void OnCollisionEnter(Collision other){
		if(other.gameObject.CompareTag("Terrain")){
			isRotating = true;
		}
	}
	
	void OnCollisionExit(Collision other){
		if(other.gameObject.CompareTag("Terrain")){
			isRotating = false;
		}
	}
	
	//Revisar si se rota desde la rotacion anterior o la nueva
	Quaternion CalculateWallRotation(float vMove){
		Vector3 vectorRotation = transform.rotation.eulerAngles;
		float targetAngle =  vMove < 0?  90*upRotation?1:-1 : 0;
		if((vectorRotation.x - targetAngle) < 0.1){
			isRotating = false;
		}
		vectorRotation.x = targetAngle;
		Quaternion newRotation = Quaternion.RotateTowards(transform.rotation,vectorRotation,turningRate *Time.deltaTime);
		
		return newRotation;
	}*/
	
}

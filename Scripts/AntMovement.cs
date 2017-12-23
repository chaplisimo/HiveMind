using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class AntMovement : MonoBehaviour {
	
	Rigidbody rb;
	
	public float speed = 4;
	public float turningRate = 30;
	
	
	bool canClimb = false;
	
	Vector3 targetPosition;
	//MIGRAR por otra cosa tal vez
	MapPathfinder mapPathfinder;
	public float timeBetweenHold = 1f;
	float holdTick = 0f;
	
	void Start(){
		rb = GetComponent<Rigidbody>();
		//SINGLETON TAL VEZ O SCRIPTABLE OBJECTS
		mapPathfinder = GameObject.FindWithTag("Respawn").GetComponent<MapPathfinder>();
	}
	
	void Update(){
	
	#if UNITY_ANDROID
    
		if(Input.touchCount > 0){
			Touch touch = Input.GetTouch(0);
			
			if(touch.phase == TouchPhase.Began){
				//TODO RayCast to check if touch is in head
				mapPathfinder.SetTargetPosition();
				
			}else if(touch.phase == TouchPhase.Moved){
				//TODO RayCast to check if touch is in head
			}else if(touch.phase == TouchPhase.Ended){
				rb.speed = 0;
			}
		}
	#else
		
		/*float hMove = Input.GetAxisRaw("Horizontal");
		float vMove = Input.GetAxisRaw("Vertical");*/
		if(Input.GetMouseButtonDown(0)){
			mapPathfinder.SetTargetPosition(GetMapPointFromMouse(Input.mousePosition));
			Debug.Log("Touch :"+mapPathfinder.GetTargetPosition());
		}else if(Input.GetMouseButton(0)){
			Vector2 oldTargetPosition = mapPathfinder.GetTargetPosition();
			mapPathfinder.SetTargetPosition(GetMapPointFromMouse(Input.mousePosition));
			if(oldTargetPosition != mapPathfinder.GetTargetPosition()){
				Debug.Log("Holding :"+mapPathfinder.GetTargetPosition());
				holdTick = 0f;
			}else{
				holdTick += Time.deltaTime;
				if(holdTick >= timeBetweenHold){
					holdTick -= timeBetweenHold;
					Debug.Log("Holding :"+mapPathfinder.GetTargetPosition());
				}
			}
		}else if(Input.GetMouseButtonUp(0)){
			Debug.Log("Released at :"+mapPathfinder.GetTargetPosition());
			holdTick = 0f;
		}
		
	#endif
	}
	
	void FixedUpdate(){
		Vector3 newPosition = Vector3.zero;
		targetPosition = GetMapPointFromMouse(Input.mousePosition);
		if(targetPosition != transform.position){
			newPosition = (targetPosition - transform.position).normalized * speed * Time.deltaTime;
			
			Quaternion rotation = Quaternion.FromToRotation(Vector3.forward,newPosition.normalized);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * turningRate);
			
		}
		rb.MovePosition(newPosition + rb.position);
	}
	
	public Vector3 GetMapPointFromMouse(Vector3 mousePosition){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		// Create a particle if hit
		if (Physics.Raycast(ray,out hit, LayerMask.GetMask("Anthive"))){
			return hit.point;
		}else{
			return transform.position;
		}
		
	}
}

using UnityEngine;

public class MapPathfinder : MonoBehaviour {
	
	//Migrar a ScriptableObject
	MapGenerator generatorScript;
	
	Vector2 targetPosition;
	
	void Start(){
		generatorScript = GetComponent<MapGenerator>();
	}
	
	public void SetTargetPosition(Vector3 worldPosition){
		Vector3 localTouch = transform.InverseTransformPoint(worldPosition);
			
		int x = Mathf.FloorToInt(localTouch.x / (generatorScript.resolution));
		int y = Mathf.FloorToInt(localTouch.y / (generatorScript.resolution));
		
		targetPosition = new Vector2(x,y);
	}
	
	public Vector2 GetTargetPosition(){
		return targetPosition;
	}
}
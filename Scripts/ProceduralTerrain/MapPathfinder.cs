using UnityEngine;

public class MapPathfinder : MonoBehaviour {
	
	//Migrar a ScriptableObject
	MapGenerator generatorScript;
	
	void Start(){
		generatorScript = GetComponent<MapGenerator>();
	}
	
	public Vector2 GetTargetPosition(Vector3 mousePosition){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePosition);
		// Create a particle if hit
		if (Physics.Raycast(ray,out hit, LayerMask.GetMask("Anthive"))){
			Debug.Log(hit.point);
			Vector3 localTouch = transform.InverseTransformPoint(hit.point);
			Debug.Log(localTouch);
			
			int x = Mathf.RoundToInt(localTouch.x / (generatorScript.resolution));
			int y = Mathf.RoundToInt(localTouch.y / (generatorScript.resolution));
			
			return new Vector2(x,y);
		}else{
			return new Vector2(-1,-1);
		}
	}
}
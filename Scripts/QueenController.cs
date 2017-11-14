using UnityEngine;

public class QueenController : MonoBehaviour {
  
  public GameObject antPrefab;
  public float breedTime = 2;
  
  void Start(){
    StartCoroutine(BreedAnts());
  }
  
  IEnumerator BreedAnts(){
    GameObject newAnt = GameObject.Instantiate(antPrefab,transform.postion, transform.rotation);
    yield return WaitForSeconds(breedTime);
  }
}

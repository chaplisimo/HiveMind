using UnityEngine;

public class QueenController : MonoBehaviour {
  
  public GameObject antPrefab;
  public int antResourceCost = 2;
  
  public float breedTime = 2;
  
  public int resourcesLeft = 5;
  
  public bool isBreeding = true;
  
  void Start(){
    StartCoroutine(BreedAnts());
  }
  
  IEnumerator BreedAnts(){
    if(resourcesLeft >= antResourcesCost){
      resources -= antResourcesCost;
      GameObject newAnt = GameObject.Instantiate(antPrefab,transform.postion, transform.rotation);
      yield return WaitForSeconds(breedTime);
    } else {
      StopCoroutine(BreedAnts);
      isBreeding = false;
      return null;
    }
  }
  
  void FeedResources(int resources){
    resourcesLeft += resources;
    if(resourcesLeft >= antResourcesCost && !isBreeding){
      isBreeding = true;
      StartCoroutine(BreedAnts());
    }
  }
}

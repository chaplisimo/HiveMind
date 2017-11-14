using UnityEngine;
using System.Collections;

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
    if(resourcesLeft >= antResourceCost){
      resourcesLeft -= antResourceCost;
      GameObject newAnt = GameObject.Instantiate(antPrefab,transform.position, transform.rotation);
      yield return new WaitForSeconds(breedTime);
    } else {
	  isBreeding = false;
      StopCoroutine("BreedAnts");
    }
  }
  
  public void FeedResources(int resources){
    resourcesLeft += resources;
    if(resourcesLeft >= antResourceCost && !isBreeding){
      isBreeding = true;
      StartCoroutine(BreedAnts());
    }
  }
}

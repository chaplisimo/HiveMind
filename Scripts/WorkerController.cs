using UnityEngine;
using UnityEngine.AI;

public class WorkerController : MonoBehaviour {
  
  UnityEngine.AI.NavMeshAgent navMeshAgent;
  Transform player;
  
  void Awake(){
    navMeshAgent = GetComponent<NavMeshAgent>();
    player = GameObject.FindWithTag("Player").transform;
  }
  
  void Start(){
    AntWorkerController.OnAntCommand += GotoCommander;
  }
  
  void GotoCommander(){
    navMeshAgent.SetDestination(player.position);
  }
}

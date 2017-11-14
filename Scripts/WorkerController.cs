using UnityEngine;

public class WorkerController : MonoBehaviour {
  
  NavMeshAgent navMeshAgent;
  Transform player;
  
  void Awake(){
    navMeshAgent = GetComponent<NavMeshAgent>();
    player = FindWithTag("Player").transform;
  }
  
  void Start(){
    AntWorkerController.OnAntCommand += GotoCommander;
  }
  
  void GotoCommander(){
    navMeshAgent.SetDestination(player.position);
  }
}

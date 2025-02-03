using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{

    public CinemachineCamera cinemachineCamera;
    public Transform player;
    private NavMeshAgent agent;
    void Start()
    {
        if (player != null){
            agent = player.GetComponent<NavMeshAgent>();
        }
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            GoToPosition();
        }
    }

    public void GoToPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            {
                if (agent != null)
                    agent.SetDestination(navHit.position);
            }
        }
    }


}

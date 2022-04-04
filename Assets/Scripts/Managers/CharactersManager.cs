using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    #region singleton

    public static CharactersManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    [SerializeField] private WanderingHuman [] NPCs;

    [SerializeField] private Transform[] destination;

    [SerializeField] private float runSpeed, walkSpeed, checkDistanceDelay, stoppingDistance;

    private float currentDelay = 0;

    private bool keepAssigningNewTargets = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(!keepAssigningNewTargets)
            return;
        
        if (currentDelay > checkDistanceDelay)
            CheckDistanceAndAssignNewDestinations();
        else
            currentDelay += Time.deltaTime;
    }

    void Init()
    {
        foreach (var NPCs in NPCs)
        {
            if (NPCs.gameObject.activeSelf)
            {
                NPCs.Walk(walkSpeed);
                NPCs.agent.SetDestination(destination[Random.Range(0, destination.Length)].position);
            }
        }
    }

    void CheckDistanceAndAssignNewDestinations()
    {
        foreach (var NPCs in NPCs)
        {
            if (NPCs.gameObject.activeSelf)
                if (!NPCs.died)
                {
                    if (NPCs.agent.remainingDistance < stoppingDistance)
                        NPCs.agent.SetDestination(destination[Random.Range(0, destination.Length)].position);
                }
        }
    }

    public void RunAgentsRun()
    {
        foreach (var NPC in NPCs)
        {
            if (NPC.gameObject.activeSelf)
                if (!NPC.died)
                {
                    NPC.Run(runSpeed);
                    NPC.agent.SetDestination(destination[Random.Range(0, destination.Length)].position);
                }
        }
    }

    public void StopAssigningNewTargets()
    {
        keepAssigningNewTargets = false;
    }
}

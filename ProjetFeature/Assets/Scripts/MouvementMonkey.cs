using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouvementMonkey : MonoBehaviour
{
    [SerializeField]
    public Transform JoueurDest;
    public NavMeshAgent agent;


    private bool activePet = true;
    [SerializeField]
    private float cdActivePet = 10.0f;

    private float nbrRessources = 0;


    public bool startRandomMonkey = false;
    public bool targetsetMonkey = false;
    private int X;
    private bool checkcoroutine;


    private Coroutine myCoroutine;



    public GameObject[] Ressources;

    public void Start()
    {
        Ressources = GameObject.FindGameObjectsWithTag("Ressources");
        //Debug.Log(Ressources.Length);
        agent.SetDestination(JoueurDest.transform.position);
    }




    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (activePet))
        {
            activePet = false;
            LaunchSearch();
        }

        if (!activePet)
        {
            StartCoroutine(CouldownActivePet(cdActivePet));
        }

        if (!targetsetMonkey)
        {
            agent.SetDestination(JoueurDest.transform.position);
        }


        if ((agent.transform.position.x == Ressources[X].transform.position.x) && (agent.transform.position.z == Ressources[X].transform.position.z))
        {
            checkcoroutine = true;
        }
    }

    public void LaunchSearch()
    {
        targetsetMonkey = true;
        RandomizeRessource();

        if (myCoroutine == null)
        {
            myCoroutine = StartCoroutine(CouldownMonkey());
        }
    }

    private void RandomizeRessource()
    {
        
        X = AddRandomRessources();
        startRandomMonkey = false;

        agent.SetDestination(Ressources[X].transform.position);
    }

    
    IEnumerator CouldownMonkey()
    {
        while ((nbrRessources <= 2) && (Ressources.Length > 0))
        {
            yield return new WaitUntil(()=> checkcoroutine);
            checkcoroutine = false;
            //Destroy(Ressources[X]);
            RandomizeRessource();
            nbrRessources++;
        }

        targetsetMonkey = false;
        nbrRessources = 0;
    }

    private int AddRandomRessources()
    {
        var X = Random.Range(0, Ressources.Length);
        //Debug.Log(X);
        return X;
    }
    IEnumerator CouldownActivePet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        activePet = true;
    }
}


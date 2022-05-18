using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouvementDog : MonoBehaviour
{
    [SerializeField]
    public Transform JoueurDest;
    public NavMeshAgent agentDog;

    private bool activePet = true;
    [SerializeField]
    private float cdActivePet = 10.0f;

    public bool targetsetDog = false;
    private float cooldown = 5.0f;
    private int X;
    private bool checkcoroutine;

    private Coroutine myCoroutine;

    public GameObject[] Tresors;

    public void Start()
    {
        Tresors = GameObject.FindGameObjectsWithTag("Tresor");
        agentDog.SetDestination(JoueurDest.transform.position);
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

        if (!targetsetDog)
        {
            agentDog.SetDestination(JoueurDest.transform.position);
        }

        if ((agentDog.transform.position.x == Tresors[X].transform.position.x) && (agentDog.transform.position.z == Tresors[X].transform.position.z))
        {
            checkcoroutine = true;
        }

    }

    IEnumerator Couldown(float waitTime)
    {
      
        yield return new WaitUntil(() => checkcoroutine);
        Debug.Log("Whouaf Whouaf");
        yield return new WaitForSeconds(waitTime);
        checkcoroutine = false;
        targetsetDog = false;

    }



    public void LaunchSearch()
    {
        targetsetDog = true;
        RandomizeRessource();

        if (myCoroutine == null)
        {
            myCoroutine = StartCoroutine(Couldown(cooldown));
        }
    }

    private void RandomizeRessource()
    {
        X = AddRandomtresor();
        agentDog.SetDestination(Tresors[X].transform.position);
    }


    private int AddRandomtresor()
    {
        var X = Random.Range(0, Tresors.Length);
        return X;
    }
    IEnumerator CouldownActivePet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        activePet = true;
    }
}


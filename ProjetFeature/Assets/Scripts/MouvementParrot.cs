using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouvementParrot : MonoBehaviour
{
    [SerializeField]
    public Transform JoueurDest;
    public NavMeshAgent agentParrot;


   
    public bool targetsetParrot = false;
    private int X = 0;
    private bool checkcoroutine;
    private int nbrEnemies = 0 ;

    private bool activePet = true;
    [SerializeField]
    private float cdActivePet = 10.0f;



    private Coroutine myCoroutine;



    public GameObject[] Enemies;

    public void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemies");
        
        agentParrot.SetDestination(JoueurDest.transform.position);
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
    

        if (!targetsetParrot)
        {
            agentParrot.SetDestination(JoueurDest.transform.position);
        }


        if ((agentParrot.transform.position.x == Enemies[X].transform.position.x) && (agentParrot.transform.position.z == Enemies[X].transform.position.z))
        { 
            nbrEnemies++;
          
            if (nbrEnemies == Enemies.Length)
            {
                
                RetourPLayer();
               
            } 
            else if (X < Enemies.Length-1)
            {
               
                X++;
                TargetEnemies();
            }
        }
    }

    public void LaunchSearch()
    {
        targetsetParrot = true;
        TargetEnemies();
    }

    private void TargetEnemies()
    {
        agentParrot.SetDestination(Enemies[X].transform.position);  
    }
    private void RetourPLayer()
    {
        Debug.Log("Nombre Enemies : " + Enemies.Length );
        X = 0;
        nbrEnemies = 0;
        targetsetParrot = false;

    }

    
    IEnumerator CouldownActivePet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        activePet = true;
    }
}


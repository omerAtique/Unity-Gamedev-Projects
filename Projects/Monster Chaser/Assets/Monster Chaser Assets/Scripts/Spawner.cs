using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;

    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;
    private int randomSide;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    // Update is called once per frame
    IEnumerator SpawnMonsters(){
        while(true)
        {
            //Waiting between each spawn
            yield return new WaitForSeconds(Random.Range(1, 5));

            //Choosing which monster is going to spawn
            randomIndex = Random.Range(0, monsterReference.Length);

            //Choosing which side it will be that the monster is spawned at
            randomSide = Random.Range(0, 2);

            //stores which monster is going to be spawned
            spawnedMonster = Instantiate(monsterReference[randomIndex]);

            if(randomSide == 0)
            {
                //left side
                //makes sure that that the monster is going to spawn at the desired side
                spawnedMonster.transform.position = leftPos.position;
                //gives the spawned monster a certain speed at spawn and the speed is taken from the youkai script
                spawnedMonster.GetComponent<Youkai>().speed = Random.Range(4, 10);
            }
            else 
            {
                //right side
                //similar to the left side
                spawnedMonster.transform.position = rightPos.position;
                //similar to the left side but the range of the speed is in neg cause the monsters will be moving the opposite way 
                spawnedMonster.GetComponent<Youkai>().speed = -Random.Range(4, 10);
                //to flip the monster as it is coming form the right side 
                spawnedMonster.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }//while loop so that multiple monsters are spawned not just one 
    }
}

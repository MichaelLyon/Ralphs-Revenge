using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        timeBetweenSpawns = seccondsBetweenSpawn;
        for (int i = 0; i < easyPresets.Count; ++i)
        {
            easyHolders.Add(easyPresets[i].GetComponent<Holder>());
            easyPresets[i].SetActive(false);
        }
        for (int i = 0; i < mediumPresets.Count; ++i)
        {
            mediumHolders.Add(mediumPresets[i].GetComponent<Holder>());
            mediumPresets[i].SetActive(false);
        }
        for (int i = 0; i < hardPresets.Count; ++i)
        {
            hardHolders.Add(hardPresets[i].GetComponent<Holder>());
            hardPresets[i].SetActive(false);
        }
        for (int i = 0; i < happyPresets.Count; ++i)
        {
            happyHolders.Add(happyPresets[i].GetComponent<Holder>());
            happyPresets[i].SetActive(false);
        }
        lists[0] = easyPresets;
        holders[0] = easyHolders;
        lists[1] = mediumPresets;
        holders[1] = mediumHolders;
        lists[2] = hardPresets;
        holders[2] = hardHolders;
    }
    List<KeyValuePair<GameObject, Holder>> activeList = new List<KeyValuePair<GameObject, Holder>>();
    public List<GameObject> easyPresets = new List<GameObject>();
    List<Holder> easyHolders = new List<Holder>();
    public List<GameObject> mediumPresets = new List<GameObject>();
    List<Holder> mediumHolders = new List<Holder>();
    public List<GameObject> hardPresets = new List<GameObject>();
    List<Holder> hardHolders = new List<Holder>();
    public List<GameObject> happyPresets = new List<GameObject>();
    List<Holder> happyHolders = new List<Holder>();
    List<GameObject>[] lists = new List<GameObject>[3];
    List<Holder>[] holders = new List<Holder>[3];
    float counter;
    int spawns;
    float timeBetweenSpawns;
    public float seccondsBetweenSpawn;
    public float timerReduction;
    public int spawnsBeforeProgress;
    int listMax =0;
    int listMin = 0;
    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter > timeBetweenSpawns)
        {
            int listPick = Random.Range((int)listMin,(int)listMax);
            int item = Random.Range((int)0,(int)lists[listPick].Count-1);
            Spawn(item, lists[listPick], holders[listPick]);
            counter = 0;
            spawns++;
            timeBetweenSpawns -= timerReduction;
            if(spawns%spawnsBeforeProgress==0)
            {
                if(listMax<2)
                {
                    listMax++;
                    timeBetweenSpawns = seccondsBetweenSpawn;
                }
                else if(listMin<2)
                {
                    listMin++;
                    timeBetweenSpawns = seccondsBetweenSpawn;
                }
            }
        }

        for (int i = activeList.Count - 1; i >= 0; --i)
        {
            if(!activeList[i].Key.activeSelf)
            {
                activeList.RemoveAt(i);
            }
           /* bool remove = false;
            foreach (GameObject obj in activeList[i].Value.holder)
            {
                if (obj.activeSelf)
                {
                    remove = false;
                    break; //go through the gameObjects in the holder, if any of them are active exit the loop
                }
                else
                {
                    remove = true;
                }

            }
            if (remove)
            {
                activeList[i].Key.SetActive(false); // if none of the objects in the holder are active set the holder to inactive and remove it from the active list
                activeList.RemoveAt(i);
            }*/
        }
    }
    void Spawn(int index, List<GameObject> list, List<Holder> holder)
    {

        if (!list[index].activeSelf) // try to spawn the desired preset
        {
            list[index].SetActive(true);
            list[index].SendMessage("Reset", new Vector3(0, Camera.main.transform.position.y, 0));//transform.position = new Vector3(0, Functions.ScreenPointTopLeft.y + 2, 0);
            activeList.Add(new KeyValuePair<GameObject, Holder>(list[index], holder[index]));
        }
        else // if the desired preset is already active, find the first inactive preset and spawn it
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (!list[i].activeSelf)
                {
                    list[i].SetActive(true);
                    list[i].SendMessage("Reset", new Vector3(0, Camera.main.transform.position.y, 0));
                    activeList.Add(new KeyValuePair<GameObject, Holder>(list[i], holder[i]));
                    break;
                }
            }

        }
    }
}

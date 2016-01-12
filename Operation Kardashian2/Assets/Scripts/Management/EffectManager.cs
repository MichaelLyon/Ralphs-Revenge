using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour
{
    public ParticleSystem waterSplash;
    public List<GameObject> coinHolder = new List<GameObject>();
    public List<GameObject> birdPooHolder = new List<GameObject>();
    public GameObject coinPrefab;
    public GameObject birdPooPrefab;
    //private
    static EffectManager instance;
    public static EffectManager Instance
    {
        get
        {
            if(instance==null)
            {
                instance = GameObject.Find("Game Manager").GetComponent<EffectManager>();
            }
            return instance;
        }
    }
    
    // Use this for initialization
    void Start()
    {
        waterSplash = GameObject.Find("WaterSplash").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject system in birdPooHolder)
        {
            if (system.GetComponent<ParticleSystem>().isPlaying == false && system.activeSelf)
            {
                system.SetActive(false);
            }
        }
    }
    GameObject GetObject(List<GameObject> holder)
    {
        foreach (GameObject item in holder)
        {
            if (item.activeSelf == false)
            {
                item.SetActive(true);
                return item;
            }
        }
        return null;
    }
    GameObject CloneNewObject(List<GameObject> holder, GameObject original)
    {
        GameObject clone = (GameObject)GameObject.Instantiate(original);
        clone.name = original.name;
        holder.Add(clone);
        return clone;
    }

    public void SpawnCoin(Vector3 position)
    {
        GameObject coin = GetObject(coinHolder);
        if (coin == null)
        {
            coin = CloneNewObject(coinHolder, coinPrefab);
        }
        coin.SendMessage("Reset", position);
    }
    public void SpawnPoo(Vector3 position)
    {
        GameObject birdPoo = GetObject(birdPooHolder);
        if (birdPoo == null)
            birdPoo = CloneNewObject(birdPooHolder, birdPooPrefab);
        birdPoo.transform.position = position;
    }

}

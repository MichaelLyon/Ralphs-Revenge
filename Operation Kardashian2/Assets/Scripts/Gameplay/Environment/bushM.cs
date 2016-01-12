using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class bushM : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        foreach (GameObject obj in bushes)
        {
            obj.transform.localScale *= Camera.main.aspect;
        }
    }
    public List<GameObject> bushes = new List<GameObject>();
    public GameObject bush;
    float timer;
    float spawntime = 1;
    Vector3 TL, TR;
    // Update is called once per frame
    void Update()
    {
        TL = Functions.ScreenPointTopLeft;
        TR = Functions.ScreenPointTopRight;
        timer += Time.deltaTime;
        if (timer > spawntime)
        {
            GameObject ob = RetrieveObject(bushes, bush);
            ob.transform.position = new Vector3(Random.Range(TL.x, TR.x), TL.y + 1, .1f);
            ob.SetActive(true);
            timer = 0;
            spawntime = Random.Range((float)1, (float)3);
        }
    }
    public GameObject RetrieveObject(List<GameObject> holder, GameObject prefab)
    {
        GameObject clone = FindObject(holder);
        if (clone == null)
        {
            clone = MakeObject(prefab);
            holder.Add(clone);
        }
        return clone;
    }

    private GameObject FindObject(List<GameObject> holder)
    {
        // GameObject explosion;
        foreach (GameObject item in holder)
        {
            if (!item.activeSelf)
            {
                return item;
            }
        }
        return null;
    }
    private GameObject MakeObject(GameObject prefab)
    {
        GameObject clone = (GameObject)GameObject.Instantiate(prefab);
        clone.name = prefab.name + "(clone)";
        return clone;
    }
}

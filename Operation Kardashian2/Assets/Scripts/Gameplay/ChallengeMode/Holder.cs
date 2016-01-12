using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Holder : MonoBehaviour
{
    float timer;
    void Awake()
    {
        foreach (GameObject obj in holder)
        {

            obj.transform.localScale *= Camera.main.aspect;
            positions.Add(obj.transform.position);
        }
    }
    // Use this for initialization
    void Start()
    {
        
    }
    public List<GameObject> holder = new List<GameObject>();
    List<Vector3> positions = new List<Vector3>();
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>10)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }
    public void Reset(Vector3 position)
    {
        transform.position = Vector3.zero;
        for(int i =0; i<holder.Count;++i)
        {
            holder[i].transform.position = positions[i];
            holder[i].transform.position += position;
            holder[i].SetActive(true);
            holder[i].SendMessage("BecomeActive");
        }
        
    }
}

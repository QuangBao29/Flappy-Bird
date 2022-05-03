using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{
    //[SerializeField]
    private float Speed = 4;
    [SerializeField]
    private float limit = 19;
    GameObject obj;
    Vector3 OldPos;

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        OldPos = obj.transform.position;
    }
    private void Update()
    {
        //position cu + them 1 vecto moi
        obj.transform.Translate(new Vector3(-1*Time.deltaTime*Speed, 0, 0));
        if (Vector3.Distance(obj.transform.position, OldPos) > limit)
        {
            obj.transform.position = OldPos;
        }
    }


}

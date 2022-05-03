using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    //[SerializeField]
    private float Speed = 4;
    
    GameObject obj;
    private float OldPos;

    //[SerializeField]
    private float MinY = -8.9f;
    //[SerializeField]
    private float MaxY = -2.5f;

    private string RESPAWN_TAG = "Respawn";

    // Start is called before the first frame update
    void Start()
    {
        obj = gameObject;
        OldPos = 12.6f;
        
    }
    private void Update()
    {
        //position cu + them 1 vecto moi
        obj.transform.Translate(new Vector3(-1 * Time.deltaTime * Speed, 0, 0));
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(RESPAWN_TAG))
        {
            obj.transform.position = new Vector3(OldPos, Random.Range(MinY, MaxY), 0);
        }
        
    }
}

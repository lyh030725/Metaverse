using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour

{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
    }


    //Local <-> World <-> Viewport <-> Screen
    //Input.mousePosition   Screen 좌표
    //Camera.main.ScreenToViewportPoint(Input.mousePosition)    Viewport 좌표

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //int mask = 1 << 6;
            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");


            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.tag}");

            }
        }


        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousepos - Camera.main.transform.position;
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");

        //    }
        //}


    }
}

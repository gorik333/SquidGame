using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnvWorld : MonoBehaviour
{

    public List<GameObject> m_list= new List<GameObject>();


    void Start()
    {
        Build();
    }

    void Build()
    {
        for(int i= 0; i< 60; ++i)
        {
            GameObject go= Instantiate( m_list[ Random.Range(0,m_list.Count) ], transform );

            float x= Random.Range(-60f, 60f );
            float y= Random.Range(-80f,-30f );
            float z= Random.Range( 10f,400f );

            go.transform.localPosition= new Vector3( x, y, z );

            go.transform.localRotation= Quaternion.EulerAngles( 0, Random.Range( 0, 300 ), 0 );

            go.transform.localScale= Vector3.one * (Random.Range( 0.5f, 0.5f ) + Mathf.Abs( y / 100f ));


        }
    }

}

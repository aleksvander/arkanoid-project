using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public Transform Ta, Tb, Ttop;

    Vector3 a, b, midPoint;



    void Update () {

        if (Ta && Tb && Ttop) {

            a = Ta.position;

            b = Tb.position;

            midPoint = (a + b) / 2;

            midPoint.y = Ttop.position.y;

            Ttop.position = midPoint;

        }

    }



    void OnDrawGizmos () {

        Gizmos.color = Color.red;

        Gizmos.DrawLine(a, b);

        Vector3 dir = b - a;

        float count = 20;

        Vector3 lastP = a;

        for (float i = 0; i < count + 1; i++) {

            Vector3 p = a + (dir / count) * i;

            p.y = Mathf.Sin((i / count) * Mathf.PI) * midPoint.y;

            Gizmos.color = i % 2 == 0 ? Color.blue : Color.green;

            Gizmos.DrawLine(lastP, p);

            lastP = p;

        }

    }
}

using UnityEngine;
using System.Collections;

public class MariaBehaviour : MonoBehaviour
{

    public Transform Stanko;
    public float maxY = 0.1f;    
    public float minY = 0.1f;
    public float flySpeed = 0.01f;

    bool ascending =  true;
    new Vector3 midPoint;
    // Use this for initialization
    void Start()
    {
        midPoint = this.transform.localPosition ;
    }

    // Update is called once per frame
    void Update()
    {

        if (this.transform.localPosition.y < midPoint.y - Mathf.Abs(minY))
            ascending = true;
        if (this.transform.localPosition.y > midPoint.y + Mathf.Abs(maxY))
            ascending = false;
    }

    void FixedUpdate()
    {
        if (ascending)
        {
            this.transform.position += new Vector3(0, flySpeed, 0);
        }
        else 
        {
            this.transform.position -= new Vector3(0, flySpeed, 0);
        }
    }
}

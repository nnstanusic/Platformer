using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{

    public Transform PlayerPos;
    public GameObject Background;

    public float Vdistance = 3;


    Vector3 backgroundCoordinates;
    float backgroundW;
    float backgroundH;
    float cameraHeight;
    float cameraWidth;
    float leftBoundary;
    float rightBoundary;
    float topBoundary;
    float bottomBoundary;

    // Use this for initialization
    void Start()
    {
        backgroundW =
            Background.GetComponent<SpriteRenderer>().bounds.size.x;
        backgroundH =
            Background.GetComponent<SpriteRenderer>().bounds.size.y;
        backgroundCoordinates =
            Background.transform.position;

        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;

        leftBoundary =
            backgroundCoordinates.x - backgroundW / 2f + cameraWidth / 1.9f;
        rightBoundary =
            backgroundCoordinates.x + backgroundW / 2f - cameraWidth / 1.9f;
        topBoundary =
            backgroundCoordinates.y + backgroundH / 2f - cameraHeight / 1.9f;
        bottomBoundary =
            backgroundCoordinates.y - backgroundH / 2f + cameraHeight / 1.9f;

        this.transform.position = new Vector3(leftBoundary,
                                              this.transform.position.y,
                                              this.transform.position.z);


    }

    // Update is called once per frame
    void Update()
    {
        updateX();
        updateY();
    }

    void updateX()
    {

        if (PlayerPos.position.x > leftBoundary &&
            PlayerPos.position.x < rightBoundary)
        {
            transform.position = new Vector3(PlayerPos.position.x,
                                             this.transform.position.y,
                                             this.transform.position.z);
        }

    }

    void updateY()
    {

        if (PlayerPos.position.y + Vdistance > bottomBoundary &&
            PlayerPos.position.y + Vdistance < topBoundary)
        {
            transform.position = new Vector3(this.transform.position.x,
                                             PlayerPos.position.y + Vdistance,
                                             this.transform.position.z);

        }
    }


}


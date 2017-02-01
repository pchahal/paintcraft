using UnityEngine;

public class SkinCamera : MonoBehaviour
{

    Vector3 originalCam;
    Vector3 headCam;
    Vector3 bodyCam;
    Vector3 leftArmCam;
    Vector3 rightArmCam;
    Vector3 leftLegCam;
    Vector3 rightLegCam;


    // Use this for initialization
    void Start()
    {
        originalCam = new Vector3(0, -.5f, -6);
        headCam = new Vector3(0, 0.5f, -3);
        bodyCam = new Vector3(0, -0.5f, -3);
        leftArmCam = new Vector3(-.75f, -0.5f, -2.6f);
        rightArmCam = new Vector3(.75f, -0.5f, -2.6f);
        leftLegCam = new Vector3(-0.25f, -2f, -2.5f);
        rightLegCam = new Vector3(.25f, -2f, -2.5f);


    }

    public void ZoomIn(BodyPart bodyPart)
    {
        if (bodyPart == BodyPart.HEAD || bodyPart == BodyPart.HAT)
            Camera.main.transform.position = headCam;
        else if (bodyPart == BodyPart.BODY || bodyPart == BodyPart.JACKET)
            Camera.main.transform.position = bodyCam;
        else if (bodyPart == BodyPart.RIGHTARM || bodyPart == BodyPart.RIGHTSLEEVE)
            Camera.main.transform.position = rightArmCam;
        else if (bodyPart == BodyPart.LEFTARM || bodyPart == BodyPart.LEFTSLEEVE)
            Camera.main.transform.position = leftArmCam;
        else if (bodyPart == BodyPart.LEFTLEG || bodyPart == BodyPart.LEFTPANT)
            Camera.main.transform.position = leftLegCam;
        else if (bodyPart == BodyPart.RIGHTLEG || bodyPart == BodyPart.RIGHTPANT)

            Camera.main.transform.position = rightLegCam;
        else
            Camera.main.transform.position = originalCam;


    }



}

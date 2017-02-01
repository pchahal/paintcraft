using UnityEngine;

public class Painter : MonoBehaviour
{
    private Camera cam;

    Actions actions;
    Steve steve;

    void Start()
    {
        cam = GetComponent<Camera>();
        actions = GetComponent<Actions>();
        steve = GameObject.Find("StevePaintable").GetComponent<Steve>();
    }

    void Update()
    {
        GameObject modal = GameObject.Find("ModalPanel");
        if (!modal)
        {

            if (steve.isEditMode)
            {
                if (!Input.GetMouseButton(0))
                    return;

                CheckHit();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CheckHit();
                steve.isEditMode = true;
            }
        }
    }

    void CheckHit()
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        var localNormal = hit.transform.InverseTransformDirection(hit.normal);

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
            return;

        Texture2D tex = rend.material.mainTexture as Texture2D;
        Vector2 pixelUV = hit.textureCoord;


        //HACK use clothes UV set, since mesh is not updating when changing from body to clothes.
        Vector2 uvOffset = hit.collider.gameObject.GetComponent<UVEditor>().uvOffset;
        if (steve.bodyClothes == 1)
            pixelUV += uvOffset;


        BodyPart bodyPart = SteveDict.bodyPartsDict[hit.collider.name];
        bodyPart = steve.GetCurrentBodyPart(bodyPart);
        actions.DoAction(tex, pixelUV, localNormal, bodyPart);
    }
}
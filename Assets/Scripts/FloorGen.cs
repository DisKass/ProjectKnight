using UnityEngine;

public class FloorGen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject model;
    [SerializeField]
    private float scaleFactor;
    [SerializeField]
    private Vector2 floorSizeX, floorSizeY;

    void Start()
    {
        BoxCollider tmp;
        //modelSize.x = model.transform.localScale.x;
        //modelSize.y = model.transform.localScale.z;
        GameObject floorContainer = new GameObject("Floor");
        floorContainer.isStatic = true;
        floorContainer.tag = "Floor";
        tmp = floorContainer.AddComponent<BoxCollider>();
        tmp.center = new Vector3((floorSizeX.x + floorSizeX.y) / 2f - floorContainer.transform.position.x,
                                0.0625f / 2 - floorContainer.transform.position.y,
                                (floorSizeY.y + floorSizeY.x) / 2f - floorContainer.transform.position.z);
        tmp.size = new Vector3(floorSizeX.y - floorSizeX.x, scaleFactor / 10, floorSizeY.y - floorSizeY.x);

        for (float i = floorSizeX.x + 0.5f; i < floorSizeX.y; i++)
        {
            for (float j = floorSizeY.x + 0.5f; j < floorSizeY.y; j++)
            {
                Instantiate(model, new Vector3(i, 0, j), Quaternion.identity, floorContainer.transform).isStatic = true;//.
                        //AddComponent<BoxCollider>();
                //tmp.size = new Vector3(1, 0.0625f, 1);
                //tmp.center = new Vector3(0, 0.0625f / 2, 0);
                //tmp.tag = "Player";
            }
        }
        floorContainer.AddComponent<CombiningChildMesh>();
    }
}

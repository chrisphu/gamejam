using System.Linq;
using UnityEngine;

public class PylonController : MonoBehaviour
{
    Joint2D jointOnPylon;
    GameObject[] connections;

    // Start is called before the first frame update
    void Start()
    {
        connections = new GameObject[512];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((jointOnPylon = gameObject.GetComponent<DistanceJoint2D>()))
        {
            Annihilate();
            foreach (var joint in gameObject.GetComponents<DistanceJoint2D>())
                Destroy(joint);
            connections = new GameObject[512];
            Destroy(gameObject);
        }
    }

    void GetListOfConnections(GameObject obj)
    {
        var connectedBodies = obj.GetComponents<DistanceJoint2D>().Select(x => x.connectedBody.gameObject);
        foreach (var enemy in connectedBodies)
        {
            if (!enemy.GetComponent<LensFlare>() && !enemy.Equals(gameObject))
            {
                enemy.AddComponent<LensFlare>();
                connections.Append(enemy);
                GetListOfConnections(enemy);
                Destroy(enemy);
            }
        }
    }

    void Annihilate()
    {
        GetListOfConnections(gameObject);
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    //float maxDistance = 2.0f;
    //float swayEffect = 1.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        //Vector2 towardsMouse = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);

        transform.position = new Vector2(player.transform.position.x, player.transform.position.y);// + towardsMouse.normalized * Mathf.Clamp(towardsMouse.magnitude, 0.0f, maxDistance) / maxDistance * swayEffect;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
    }
}

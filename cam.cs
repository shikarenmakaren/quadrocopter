using UnityEngine;

public class cam : MonoBehaviour {
    public GameObject DRONE;

	void Update ()
    {
        transform.position = new Vector3(DRONE.transform.position.x, DRONE.transform.position.y, -10f);
	}
}

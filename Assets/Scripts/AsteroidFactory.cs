using UnityEngine;
using System.Collections;

public class AsteroidFactory : MonoBehaviour {
  public GameObject asteroidObject;

  public float minSpawnTime;
  public float maxSpawnTime;

  public Vector3 minVelocity;
  public Vector3 maxVelocity;

  void Start () {
    SpawnAsteroid();
  }

  void Update () {

  }

  void SpawnAsteroid() {
    var rock = Instantiate(asteroidObject) as GameObject;
    var body = rock.GetComponent<Rigidbody>();
    rock.transform.position = transform.position;
    body.velocity = new Vector3(
      Random.Range(minVelocity.x, maxVelocity.x),
      Random.Range(minVelocity.y, maxVelocity.y),
      Random.Range(minVelocity.z, maxVelocity.z)
    );
Debug.Log(body.velocity);
    Invoke("SpawnAsteroid", Random.Range(minSpawnTime, maxSpawnTime));
  }
}

using UnityEngine;
using System.Collections;

public class AsteroidFactory : MonoBehaviour {
  public GameObject asteroidObject;

  public float startDelay;
  public float minSpawnTime;
  public float maxSpawnTime;

  public Vector3 minVelocity;
  public Vector3 maxVelocity;

  IEnumerator Start () {
    SpawnAsteroid();
    yield return new WaitForSeconds(startDelay);
    StartCoroutine(AsteroidSpawner());
  }

  void Update () {

  }

  IEnumerator AsteroidSpawner() {
    while (true) {
      yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
      SpawnAsteroid();
    }
  }

  public GameObject SpawnAsteroid() {
    var asteroid = Instantiate(asteroidObject) as GameObject;
    var body = asteroid.GetComponent<Rigidbody>();
    asteroid.transform.position = transform.position;
    body.velocity = new Vector3(
      Random.Range(minVelocity.x, maxVelocity.x),
      Random.Range(minVelocity.y, maxVelocity.y),
      Random.Range(minVelocity.z, maxVelocity.z)
    );
Debug.Log(body.velocity);

    return asteroid;
  }
}

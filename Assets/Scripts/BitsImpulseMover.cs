using UnityEngine;
using System.Collections;

public class BitsImpulseMover : MonoBehaviour {
  public float headingVariance;
  public float impulseForce;
  public GameObject target;

  void Awake() {
    var forceVec = (target.transform.position - transform.position).normalized;
    forceVec *= impulseForce;
    forceVec += Vector3.right * Random.Range(-headingVariance, headingVariance);

    var body = GetComponent<Rigidbody>();
    body.AddForce(forceVec, ForceMode.Impulse);
    body.AddRelativeTorque(0, 0, Random.Range(impulseForce / 2f, impulseForce * 2f));
  }
}

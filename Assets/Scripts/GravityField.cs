using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GravityField : MonoBehaviour {
  public GameObject homePlanet;
  public float forcePerSecond;
  public float minRadius;
  private List<Rigidbody> _bodies;

  void Start() {
    _bodies = new List<Rigidbody>();
  }

  void OnTriggerEnter(Collider c) {
    var parent = c.gameObject.transform.parent;
    if (parent && parent.tag == "Character") {
Debug.Log("Got " + parent.name);
      _bodies.Add(parent.GetComponent<Rigidbody>());
    }
  }

  void OnTriggerExit(Collider c) {
    var parent = c.gameObject.transform.parent;
    if (parent && parent.tag == "Character") {
Debug.Log("Lost " + parent.name);
      _bodies.Remove(parent.GetComponent<Rigidbody>());
    }
  }

  void Update() {
    _bodies = _bodies.Aggregate(new List<Rigidbody>(), (sum, body) => {
      var distVector = transform.position - body.transform.position;

      if (distVector.sqrMagnitude <= Mathf.Pow(minRadius, 2)) {
        // Touchdown! Parent to this object and stop adding forces.
        body.gameObject.transform.parent = transform;
        body.velocity = Vector3.zero;
        body.gameObject.SendMessage("OnLanded");
        body.isKinematic = true;
        // var feetDown = body.gameObject.GetComponent<FeetDown>();
        // feetDown.target = gameObject;
        var bodyTrans = body.gameObject.transform;
        var feetAxis = bodyTrans.position - transform.position;
        LeanTween.value(body.gameObject, vec => bodyTrans.up = vec, bodyTrans.up, feetAxis, 0.75f)
          .setEase(LeanTweenType.easeInOutElastic);
      } else {
        // Inside gravity well? Attract. Keep attracting.
        body.AddForce(distVector.normalized * forcePerSecond * Time.fixedDeltaTime * 1/distVector.sqrMagnitude);
        sum.Add(body);
      }
      return sum;
    });
    // foreach (var body in _bodies) {
    // }
  }
}
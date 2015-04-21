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
    if (!parent) return;
    var isCharacter = parent.tag == "Character";
    var isActive = parent.gameObject.layer != LayerMask.NameToLayer("InactiveCharacter");
    if (isCharacter && isActive) {
      _bodies.Add(parent.GetComponent<Rigidbody>());
    }
  }

  void OnTriggerExit(Collider c) {
    var parent = c.gameObject.transform.parent;
    if (parent && parent.tag == "Character") {
      _bodies.Remove(parent.GetComponent<Rigidbody>());
    }
  }

  void Update() {
    _bodies = _bodies.Aggregate(new List<Rigidbody>(), (sum, body) => {
      // Sometimes characters get swallowed by other asteroids.
      if (!body) return sum;

      var distVector = transform.position - body.transform.position;

      if (distVector.sqrMagnitude <= Mathf.Pow(minRadius, 2)) {
        Touchdown(body);
      } else {
        // Inside gravity well? Attract. Keep attracting.
        body.AddForce(distVector.normalized * forcePerSecond * Time.fixedDeltaTime * 1/distVector.sqrMagnitude);
        sum.Add(body);
      }
      return sum;
    });
  }

  void Touchdown(Rigidbody body) {
    // Touchdown! Parent to this object and stop adding forces.
    var charObj = body.gameObject;
    var handle = new GameObject();
    // Insert a handle between the planet and the character so animations
    // play back relative to handle's orientation.
    handle.name = "PedHandle";
    handle.transform.position = charObj.transform.position;
    handle.transform.rotation = charObj.transform.rotation;

    // Parenting: Asteroid => handle => character
    handle.transform.parent = transform;
    charObj.transform.parent = handle.transform;

    // Let the character know it's landed.
    body.gameObject.SendMessage("OnLanded");
    // Let the asteroid know about a newcomer.
    SendMessage("OnLanded", body.gameObject);

    // Stop the character and turn its physics off.
    body.velocity = Vector3.zero;
    body.isKinematic = true;

    var handleTrans = handle.transform;
    var feetAxis = handleTrans.position - transform.position;
    // Pedestrians have handles; rotate the parent handle, not the ped.
    // Flop the pedestrian so its feet point toward the planet.
    LeanTween.value(handleTrans.parent.gameObject, vec => handleTrans.up = vec, handleTrans.up, feetAxis, 0.75f)
      .setEase(LeanTweenType.easeInOutElastic);
  }
}
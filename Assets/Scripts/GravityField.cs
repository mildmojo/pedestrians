using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GravityField : MonoBehaviour {
  private List<Rigidbody> _bodies;

  void Start() {
    _bodies = new List<GameObject>();
  }

  void OnTriggerEnter(Collider c) {
    _bodies.Add(c.gameObject.GetComponent<Rigidbody>());
  }

  void OnTriggerExit(Collider c) {
    _bodies.Remove(c.gameObject.GetComponent<RigidBody>());
  }

  void Update() {

  }
}
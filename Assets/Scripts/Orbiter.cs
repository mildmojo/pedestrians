using UnityEngine;
using System.Collections;

public class Orbiter : MonoBehaviour {
  public GameObject target;
  public float distance;
  public float degreesPerSecond;

  private float _angle;

  // Use this for initialization
  void Start () {
    _angle = 0;
  }

  // Update is called once per frame
  void Update () {
    _angle += degreesPerSecond * Time.fixedDeltaTime;
    var rot = Quaternion.Euler(0, _angle, 0);
    transform.position = target.transform.position + rot * Vector3.forward * distance;
  }
}

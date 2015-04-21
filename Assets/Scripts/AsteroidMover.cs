using UnityEngine;
using System.Collections.Generic;

public class AsteroidMover : MonoBehaviour {
  public float forcePerMiner;
  public float steeringCorrectionForce;
  public float minPlanetRange;
  public GameObject pieces;

  private List<GameObject> _miners;
  private Dictionary<GameObject, float> _forces;
  private Rigidbody _body;
  private SpriteRenderer _renderer;
  private bool _wasVisible;

  void Awake() {
    _body = GetComponent<Rigidbody>();
    _forces = new Dictionary<GameObject, float>();
    _miners = new List<GameObject>();
    _renderer = GetComponentInChildren<SpriteRenderer>();
  }

  void FixedUpdate() {
    foreach(var planet in _forces.Keys) {
      var homeworldVec = planet.transform.position - transform.position;
      var homeworldVecNorm = homeworldVec.normalized;
      var homeworldVelocity = homeworldVecNorm * Vector3.Dot(_body.velocity, homeworldVecNorm);
      var dragVec = _body.velocity - homeworldVelocity;

      homeworldVec *= _forces[planet];
      dragVec *= steeringCorrectionForce * _miners.Count;

      _body.AddForce((homeworldVec - dragVec) * Time.fixedDeltaTime);
    }

    // When becomes invisible, destroy after 5 secs if it has no miners.
    if (_renderer.isVisible) {
      _wasVisible = true;
    } else if (_wasVisible && _miners.Count == 0) {
      _wasVisible = false;
      Invoke("DestroyIfEmpty", 5f);
    }
  }

  public bool IsMinedBy(GameObject planet) {
    return _forces.ContainsKey(planet);
  }

  void OnScore(GameObject planet) {
    // EjectMiners(planet);
    BlowUp();
  }

  void OnLanded(GameObject miner) {
    var planet = miner.GetComponent<DataBucket>().Get("homeworld") as GameObject;
    if (!_forces.ContainsKey(planet)) _forces[planet] = 0f;
    _forces[planet] += forcePerMiner;
    _miners.Add(miner);
  }

  void OnTakeoff(GameObject miner) {
    var planet = miner.GetComponent<DataBucket>().Get("homeworld") as GameObject;
    _forces[planet] -= forcePerMiner;
    _miners.Remove(miner);
  }

  void DestroyIfEmpty() {
    if (_miners.Count == 0) {
      Destroy(gameObject);
    }
  }

  void EjectMiners(GameObject planet) {
    foreach (var miner in _miners) {
      // Stop animating
      miner.GetComponent<Animator>().enabled = false;
      // Detach
      miner.transform.parent = null;
      // Spin
      var body = miner.GetComponent<Rigidbody>();
      body.AddTorque(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), ForceMode.Impulse);
      // Use parent's velocity
      body.velocity = Vector3.forward * 10f;
    }
  }

  void BlowUp() {
    // pieces.transform.parent = null;
    // pieces.transform.localScale = Vector3.zero;

    // pieces.SetActive(true);
    // LeanTween.scale(pieces, Vector3.one, 0.2f);
    LeanTween.scale(gameObject, Vector3.zero, 1f).setOnComplete(() => Destroy(gameObject));
  }

}
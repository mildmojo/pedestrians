using UnityEngine;
using UnityEngine.Audio;

public class ScoreZone : MonoBehaviour {
  public AudioClip scoreSound;
  private float _colliderRadiusSqr;
  private int _asteroidLayer;
  private AudioSource _audioSource;

  void Awake() {
    _colliderRadiusSqr = Mathf.Pow(GetComponent<SphereCollider>().radius, 2);
    _asteroidLayer = LayerMask.NameToLayer("Asteroid");
    _audioSource = GetComponent<AudioSource>();
  }

  void OnTriggerEnter(Collider c) {
    Debug.Log("you've entered the SCORING ZONE");
  }

  void OnTriggerStay(Collider c) {
    var colliderDistSqr = (c.gameObject.transform.position - transform.position).sqrMagnitude;
    var isClose = colliderDistSqr < _colliderRadiusSqr;
    if (!isClose) return;

    if (c.gameObject.layer == _asteroidLayer) {
Debug.Log("Asteroid in view");
      var mover = c.gameObject.GetComponent<AsteroidMover>();
      if (mover.IsMinedBy(gameObject)) {
Debug.Log("Scoring asteroid");
        c.gameObject.layer = LayerMask.NameToLayer("Default");
        c.gameObject.SendMessage("OnScore", gameObject);
        DoScore(c.gameObject);
      }
    }
  }

  void DoScore(GameObject asteroid) {
    _audioSource.PlayOneShot(scoreSound);
    HudManager.Instance.OnScore();
  }
}
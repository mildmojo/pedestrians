using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Driller : MonoBehaviour {

  public AudioClip riseClip;
  public AudioClip slamClip;

  public delegate void Slam(GameObject self);
  public event Slam OnSlam = delegate {};
  public delegate void Rise(GameObject self);
  public event Rise OnRise = delegate {};

  private AudioSource _audio;
  private Animator _animator;

  void Awake () {
    _audio = GetComponent<AudioSource>();
    _animator = GetComponent<Animator>();
    _animator.enabled = false;
  }

  void Update () {

  }

  // Called by animation.
  public void RiseUp() {
    _audio.pitch = Random.Range(0.98f, 1.02f);
    OnRise(gameObject);
  }

  // Called by animation.
  public void GroundSlam() {
    _audio.pitch = Random.Range(0.98f, 1.02f);
    _audio.PlayOneShot(slamClip);
    OnSlam(gameObject);
  }

  // Broadcast by character collision with asteroid.
  public void OnLanded() {
    LeanTween.delayedCall(1f, () => {
      _animator.enabled = true;
    });
  }
}

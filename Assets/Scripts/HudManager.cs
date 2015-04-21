using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class HudManager : MonoBehaviour {
  public static HudManager Instance;

  public float timeLimit;
  public Text scoreText;
  public Text timerText;
  public Text muteText;
  public CanvasGroup alphaPanel;
  public GameObject buttonsHandle;
  public AudioMixer mixer;
  public AudioClip gameOverSound;

  [HideInInspector] [System.NonSerialized]
  public int score;

  private float _timer;
  private string _baseScoreText;
  private string _baseTimerText;
  private AudioSource _audioSource;

  void Awake() {
    Instance = this;
    _baseScoreText = scoreText.text;
    _baseTimerText = timerText.text;
    _audioSource = GetComponent<AudioSource>();
    Reset();
  }

  void Reset() {
    score = 0;
    _timer = timeLimit;
    UpdateScoreText();
    buttonsHandle.GetComponent<CanvasGroup>().alpha = 0;
    buttonsHandle.SetActive(false);
  }

  void Start() {
    ScreenFader.Instance.FadeOut(0f);
    ScreenFader.Instance.FadeIn(0.5f, () => {
      GameState.Play();
    });
    AudioFader.Instance.FadeIn(0.5f);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.M)) {
      OnMuteButton();
    }

    if (!GameState.IsPlaying) return;

    _timer -= Time.deltaTime;

    if (_timer <= 0) {
      GameOver();
    }

    UpdateTimerText();
  }

  public void OnScore() {
    if (GameState.IsPlaying) {
      score += 50;
      UpdateScoreText();
    }
  }

  void UpdateScoreText() {
    scoreText.text = _baseScoreText + score;
  }

  void UpdateTimerText() {
    timerText.text = _baseTimerText + Mathf.Ceil(_timer);
  }

  public void OnBackButton() {
    AudioFader.Instance.FadeOut(0.5f);
    ScreenFader.Instance.FadeOut(0.5f, () => {
      LeanTween.delayedCall(0.5f, () => Application.LoadLevel("menu"));
    });
  }

  public void OnRetryButton() {
    // float volume;
    // mixer.GetFloat("volume", out volume);
    // LeanTween.value(gameObject, val => mixer.SetFloat("volume", val), volume, -80f, 0.5f);
    AudioFader.Instance.FadeOut(0.5f);
    ScreenFader.Instance.FadeOut(0.5f, () => {
      LeanTween.delayedCall(0.5f, () => Application.LoadLevel("game"));
    });
  }

  public void OnMuteButton() {
    if (GameState.IsMuted) {
      mixer.FindSnapshot("Normal").TransitionTo(0.5f);
      GameState.Unmute();
      muteText.text = "Mute";
    } else {
      mixer.FindSnapshot("Mute").TransitionTo(0.5f);
      GameState.Mute();
      muteText.text = "Unmute";
    }
  }

  void GameOver() {
    Debug.Log("sizedelta");
    Debug.Log(scoreText.rectTransform.sizeDelta);

    GameState.Stop();

    buttonsHandle.SetActive(true);
    var buttonsGroup = buttonsHandle.GetComponent<CanvasGroup>();
    LeanTween.value(buttonsHandle, val => buttonsGroup.alpha = val, 0f, 1f, 1f);

    LeanTween.value(gameObject, val => alphaPanel.alpha = val, 0f, 1f, 1f);
    scoreText.alignment = TextAnchor.MiddleCenter;
    // timerText.alignment = TextAnchor.MiddleCenter;

    var centerPoint = new Vector2(Screen.width/2f, Screen.height/2f);

    LeanTween.value(gameObject, vec => scoreText.rectTransform.sizeDelta = vec, scoreText.rectTransform.sizeDelta, centerPoint*2, 1f)
      .setEase(LeanTweenType.easeInOutQuad);
    LeanTween.value(gameObject, vec => scoreText.rectTransform.position = vec, scoreText.rectTransform.position, centerPoint, 1f)
      .setEase(LeanTweenType.easeInOutQuad);

    if (!GameState.IsMuted) {
      AudioFader.Instance.Muffle(1f);
      _audioSource.PlayOneShot(gameOverSound);
    }

    // Show high score info
    LeanTween.delayedCall(1.5f, () => {
      var _best = PlayerPrefs.GetInt("highscore", 0);
      if (score > _best) {
        PlayerPrefs.SetInt("highscore", score);
      }
      var scoreMsgs = new string[] {
        "\n\nPREVIOUS BEST: ",
        _best.ToString(),
        score > _best ? "\n\nFINE WORK, PEDESTRIAN" : "\n\nYOU CAN DO BETTER, PEDESTRIAN"
      };
      StartCoroutine(DrawScoreText(scoreMsgs));
    });
  }

  private IEnumerator DrawScoreText(string[] messages) {
    foreach (var msg in messages) {
      yield return new WaitForSeconds(0.5f);
      for (var i = 0; i < msg.Length; i++) {
        scoreText.text += msg[i];
        yield return new WaitForSeconds(0.025f);
      }
    }
  }

}
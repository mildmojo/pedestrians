using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PedestrianManager : MonoBehaviour {
  public static PedestrianManager Instance;

  public struct NameClips {
    public string name;
    public List<AudioClip> clips;
    public NameClips(string newName, List<AudioClip> newClips) {
      clips = newClips;
      name = newName;
    }
  }

  [System.Serializable] public class SpeakerToClips: GenericPair<string, List<AudioClip>> {
    public SpeakerToClips(string key, List<AudioClip> value) : base(key, value) {}
  }

  public List<SpeakerToClips> nameAudioAssets;
  public List<GameObject> pedestrianPrefabs;

  // {"loretta.firsts": {"Leeroy Jenkins": leeroy_jenkins.mp3}}
  private Dictionary<string, Dictionary<string, AudioClip>> _namesAudio;
  private List<string> _speakerNames;
  private List<string> _nameTypes = new List<string>(new[] {"first", "last"});
  private ShuffleDeck _speakerDeck;
  private Dictionary<string, ShuffleDeck> _nameDecks;
  private ShuffleDeck _pedestrianPrefabDeck;

  void Awake() {
    Instance = this;

    // Construct a nested dict like nameAudioAssets["loretta.first"]["Leeroy Jenkins"] == List<AudioClip>
    _namesAudio = new Dictionary<string, Dictionary<string, AudioClip>>();
    _speakerDeck = new ShuffleDeck();
    _nameDecks = new Dictionary<string, ShuffleDeck>();
    foreach (var speakerPair in nameAudioAssets) {
      var key = speakerPair.key;
      _nameDecks[key] = new ShuffleDeck();
      _namesAudio[key] = new Dictionary<string, AudioClip>();
      foreach (var clip in speakerPair.value) {
        var pedName = string.Join(" ", clip.name.Split('.').First().Split('_'));
        _nameDecks[key].Add(pedName);
        _namesAudio[key][pedName] = clip;
      }
    }

    _pedestrianPrefabDeck = new ShuffleDeck(pedestrianPrefabs);
  }

  public NameClips DrawNameClips() {
    var speakerName = _speakerDeck.Draw() as string;
    var firstName = _nameDecks[speakerName + ".first"].Draw() as string;
    var lastName = _nameDecks[speakerName + ".last"].Draw() as string;
    var firstClip = _namesAudio[speakerName + ".first"][firstName];
    var lastClip = _namesAudio[speakerName + ".last"][lastName];
    var newName = firstName + " " + lastName;
    return new NameClips(newName, new List<AudioClip>(new[] {firstClip, lastClip}));
  }

  public GameObject CreatePedestrian(GameObject parent, Vector3 position, GameObject homeworld) {
    var ped = Instantiate(_pedestrianPrefabDeck.Draw() as GameObject) as GameObject;
    ped.transform.position = position;
    ped.transform.parent = parent.transform;

    ped.GetComponent<Rigidbody>().isKinematic = true;
    ped.layer = LayerMask.NameToLayer("InactiveCharacter");

    var bucket = ped.AddComponent<DataBucket>();
    bucket.Set("homeworld", homeworld);

    var feetDown = ped.AddComponent<FeetDown>();
    feetDown.target = homeworld;

    return ped;
  }

}
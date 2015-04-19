using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataBucket : MonoBehaviour {
  private Dictionary<string, object> _data;

  void Awake() {
    _data = new Dictionary<string, object>();
  }

  public object Get(string key) {
    return _data.ContainsKey(key) ? _data[key] : null;
  }

  public object Set(string key, object value) {
    _data[key] = value;
    return value;
  }
}
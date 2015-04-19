using System;

[Serializable]
public class GenericPair<Tkey, Tvalue> {
  public Tkey key;
  public Tvalue value;

  public GenericPair(Tkey newKey, Tvalue newValue) {
    key = newKey;
    value = newValue;
  }
}

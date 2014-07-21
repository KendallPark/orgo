using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using Compounds;

public class CreateMolecule : MonoBehaviour {

  public float yAdjust;
  private TextAsset compoundData;
  private Compound compound;
  private string compoundId = "";
  private bool enterPressed = false;
  private bool busy = false;

  // Use this for initialization
  void Start () {

  }
  
  // Update is called once per frame
  void Update () {
    if (enterPressed) {
      enterPressed = false;
      compoundData = Resources.Load("Compounds/"+compoundId) as TextAsset;
      if (compoundData != null) {
        LoadMolecule();
      }
    }
  }

  void OnGUI() {
    compoundId = GUI.TextField (new Rect (25, 25, 100, 30), compoundId);
    if ((Event.current.type == EventType.keyUp) && (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter)) {
      print(Event.current.type);
      enterPressed = true;
    }
  }

  private void LoadMolecule() {
    if (compound != null) {
      Destroy(compound.atomsObject);
      Destroy(compound.bondsObject);
      gameObject.transform.position = new Vector3(0, 0, 0);
    }
    JSONNode compoundNode = JSON.Parse(compoundData.text);
    compound = new Compound(compoundNode);
    compound.atomsObject.transform.parent = gameObject.transform;
    compound.bondsObject.transform.parent = gameObject.transform;
    gameObject.transform.position = new Vector3(0, yAdjust, 0);
  }

}

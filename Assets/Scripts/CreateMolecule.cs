using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using Elements;

public class CreateMolecule : MonoBehaviour {

  public float yAdjust;
  public TextAsset compoundData;
  private Atom[] atomArray;

  // Use this for initialization
  void Start () {

    GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
    atomArray = new Atom[100];

    print(compoundData.text);
    var compound = JSON.Parse(compoundData.text);
    JSONNode atoms = compound["structure"]["atoms"];
    Atom carbon = new Atom("c");

    int i = 0;
    foreach (JSONNode atom in atoms.Childs) {
      string elementSymbol = atom["element"].Value;
      Atom element = new Atom(elementSymbol);
      atomArray[i] = element;
      float atomScale = element.radius / carbon.radius;

      GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      ball.transform.parent = gameObject.transform;
      ball.transform.position = new Vector3(atom["x"].AsFloat, atom["y"].AsFloat + yAdjust, atom["z"].AsFloat);
      ball.transform.localScale = new Vector3(atomScale, atomScale, atomScale);
      ball.renderer.material.color = element.modelColor;
      
      i++;
    }

  }
  
  // Update is called once per frame
  void Update () {
  
  }
}

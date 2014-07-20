using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using Compounds;

public class CreateMolecule : MonoBehaviour {

  public float yAdjust;
  public TextAsset compoundData;
  private Compound compound;

  // Use this for initialization
  void Start () {
    JSONNode compoundNode = JSON.Parse(compoundData.text);
    compound = new Compound(compoundNode);
    compound.atomsObject.transform.parent = gameObject.transform;
    compound.bondsObject.transform.parent = gameObject.transform;
    gameObject.transform.position = new Vector3(0, yAdjust, 0);
  }
  
  // Update is called once per frame
  void Update () {
  
  }

}

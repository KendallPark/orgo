using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;
using Elements;
using Bonds;

public class CreateMolecule : MonoBehaviour {

  public float yAdjust;
  public TextAsset compoundData;
  private Atom[] atoms;
  private Bond[] bonds;

  // Use this for initialization
  void Start () {

    CreateAtoms();

    CreateBonds();

  }
  
  // Update is called once per frame
  void Update () {
  
  }

  private void CreateBonds() {
    bonds = new Bond[100];

    var compound = JSON.Parse(compoundData.text);
    JSONNode jsonBonds = compound["structure"]["bonds"];

    int j = 0;
    foreach (JSONNode jsonBond in jsonBonds.Childs) {
      Atom atom1 = atoms[jsonBond["vertices"].AsArray[0].AsInt];
      Atom atom2 = atoms[jsonBond["vertices"].AsArray[1].AsInt];
      string bondOrder = jsonBond["order"].Value;

      Bond bond = new Bond(atom1, atom2, bondOrder);
      bonds[jsonBond["bid"].AsInt] = bond;

      j++;
    }
  }

  private void CreateAtoms() {
    atoms = new Atom[100];

    var compound = JSON.Parse(compoundData.text);
    JSONNode jsonAtoms = compound["structure"]["atoms"];

    int i = 0;
    foreach (JSONNode jsonAtom in jsonAtoms.Childs) {
      string elementSymbol = jsonAtom["element"].Value;
      Atom atom = new Atom(elementSymbol, jsonAtom["x"].AsFloat, jsonAtom["y"].AsFloat + yAdjust, jsonAtom["z"].AsFloat, gameObject);
      atoms[jsonAtom["aid"].AsInt] = atom;
      
      i++;
    }
  }
}

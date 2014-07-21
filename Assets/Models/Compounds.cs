using UnityEngine;
using System.Collections;
using SimpleJSON;

namespace Compounds {

  public class Compound {

    public GameObject atomsObject;
    public GameObject bondsObject;
    private Atom[] atoms;
    private Bond[] bonds;
    private JSONNode compoundData;
    private GameObject parentGameObject;
    private float yAdjust;

    public Compound(JSONNode compoundData) {
      this.compoundData = compoundData;
      this.yAdjust = yAdjust;
      CreateAtoms();
      CreateBonds();
    }

    private void CreateAtoms() {
      atoms = new Atom[100];
      atomsObject = new GameObject("Atoms");

      JSONNode jsonAtoms = compoundData["structure"]["atoms"];

      int i = 0;
      foreach (JSONNode jsonAtom in jsonAtoms.Childs) {
        string elementSymbol = jsonAtom["element"].Value;
        Atom atom = new Atom(elementSymbol, jsonAtom["x"].AsFloat, jsonAtom["y"].AsFloat, jsonAtom["z"].AsFloat);
        atoms[jsonAtom["aid"].AsInt] = atom;

        atom.ball.transform.parent = atomsObject.transform;
        
        i++;
      }
    }

    private void CreateBonds() {
      bonds = new Bond[100];
      bondsObject = new GameObject("Bonds");

      JSONNode jsonBonds = compoundData["structure"]["bonds"];

      int j = 0;
      foreach (JSONNode jsonBond in jsonBonds.Childs) {
        Atom atom1 = atoms[jsonBond["vertices"].AsArray[0].AsInt];
        Atom atom2 = atoms[jsonBond["vertices"].AsArray[1].AsInt];
        string bondOrder = jsonBond["order"].Value;

        Bond bond = new Bond(atom1, atom2, bondOrder);
        bonds[jsonBond["bid"].AsInt] = bond;

        bond.stick.transform.parent = bondsObject.transform;

        j++;
      }
    }

  }

}

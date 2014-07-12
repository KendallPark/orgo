using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;

public class CreateMolecule : MonoBehaviour {

  public float yAdjust;
  public TextAsset compoundData;

  // Use this for initialization
  void Start () {
    print(compoundData.text);
    var compound = JSON.Parse(compoundData.text);
    JSONNode atoms = compound["structure"]["atoms"];

    foreach (JSONNode atom in atoms.Childs) {
      GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      ball.transform.parent = gameObject.transform;
      ball.transform.position = new Vector3(atom["x"].AsFloat, atom["y"].AsFloat + yAdjust, atom["z"].AsFloat);
      switch (atom["element"].Value) {
        case "c":
          ball.renderer.material.color = Color.gray;
          break;
        case "o":
          ball.renderer.material.color = Color.red;
          break;
        case "n":
          ball.renderer.material.color = Color.blue;
          break;
        case "h":
          ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
          break;
      }
    }

    // GameObject atom1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    // atom1.transform.position = new Vector3(0f, 0f + yAdjust, 0f);
    // atom1.renderer.material.color = Color.red;

    // GameObject atom2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    // atom2.transform.position = new Vector3(0.2774f, 0.8929f + yAdjust, 0.2544f);
    // atom2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

    // GameObject atom3 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    // atom3.transform.position = new Vector3(0.6068f, -0.2383f + yAdjust, -0.7169f);
    // atom3.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
  }
  
  // Update is called once per frame
  void Update () {
  
  }
}

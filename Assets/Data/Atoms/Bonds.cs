using UnityEngine;
using System.Collections;
using Elements;

namespace Bonds {

  public class Bond {

    public string order;
    public Atom atom1;
    public Atom atom2;
    public GameObject stick;
    public float midx, midy, midz;
    public float width;
    public float length;

    public Bond(Atom atom1, Atom atom2, string order) {
      this.atom1 = atom1;
      this.atom2 = atom2;
      this.order = order;

      switch (order) {
        case "single":
          width = 0.1f;
          break;
        case "double":
          width = 0.3f;
          break;
        case "triple":
          width = 0.5f;
          break;
      }

      length = Vector3.Distance(atom1.ball.transform.position, atom2.ball.transform.position);

      midx = atom1.ball.transform.position.x - (atom1.ball.transform.position.x - atom2.ball.transform.position.x)/2;
      midy = atom1.ball.transform.position.y - (atom1.ball.transform.position.y - atom2.ball.transform.position.y)/2;
      midz = atom1.ball.transform.position.z - (atom1.ball.transform.position.z - atom2.ball.transform.position.z)/2;
      
      stick = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
      stick.transform.position = new Vector3(midx, midy, midz);
      stick.transform.LookAt(atom1.ball.transform.position);

      stick.transform.localScale = new Vector3(width, length/2, width);
      stick.transform.Rotate(90f, 0f, 0f);
      stick.transform.position = new Vector3(midx, midy, midz);

    }

  }


}

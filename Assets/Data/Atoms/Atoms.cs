using UnityEngine;
using System.Collections;

namespace Compounds {

  public class Atom {

    private Element element;
    public float radius;
    public string symbol;
    public Color modelColor;
    public float x, y, z;
    public GameObject ball;

    public Atom() {

    }

    public Atom(string symbol) {
      switch (symbol) {
        case "h":
          element = new Hydrogen();
          break;
        case "c":
          element = new Carbon();
          break;
        case "n":
          element = new Nitrogen();
          break;
        case "f":
          element = new Flourine();
          break;
        case "o":
          element = new Oxygen();
          break;
        case "p":
          element = new Phosphorus();
          break;
        case "s":
          element = new Sulfur();
          break;
        case "cl":
          element = new Chlorine();
          break;
        default: 
          element = new Carbon();
          break;
      }
      radius = element.radius;
      symbol = element.symbol;
      modelColor = element.modelColor;
    }

    public Atom(string symbol, float x, float y, float z) : this(symbol) {
      this.x = x;
      this.y = y;
      this.z = z;

      Carbon carbon = new Carbon();
      float atomScale = radius / carbon.radius;

      ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      ball.transform.position = new Vector3(x, y, z);
      ball.transform.localScale = new Vector3(atomScale, atomScale, atomScale);
      ball.renderer.material.color = modelColor;
    }

  }

  public class Element {
    public float radius;
    public string symbol;
    public Color modelColor;
  }

  public class Hydrogen : Element {
    public Hydrogen() {
      radius = 1.2f;
      symbol = "h";
      modelColor = Color.white;
    }
  }

  public class Carbon : Element {
    public Carbon() {
      radius = 1.7f;
      symbol = "c";
      modelColor = Color.gray;
    }
  }

  public class Nitrogen : Element {
    public Nitrogen() {
      radius = 1.55f;
      symbol = "n";
      modelColor = Color.blue;
    }
  }

  public class Oxygen : Element {
    public Oxygen() {
      radius = 1.52f;
      symbol = "o";
      modelColor = Color.red; 
    }
  }

  public class Flourine : Element {
    public Flourine() {
      radius = 1.47f;
      symbol = "f";
      modelColor = Color.green;
    }
  }

  public class Phosphorus : Element {
    public Phosphorus() {
      radius = 1.8f;
      symbol = "p";
      modelColor = new Color(255, 102, 0);
    }
  }

  public class Sulfur : Element {
    public Sulfur() {
      radius = 1.8f;
      symbol = "s";
      modelColor = Color.yellow;
    }
  }

  public class Chlorine : Element {
    public Chlorine() {
      radius = 1.75f;
      symbol = "cl";
      modelColor = Color.green;
    }
  }

}

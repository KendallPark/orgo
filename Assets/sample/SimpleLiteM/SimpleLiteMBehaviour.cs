using UnityEngine;
using System;
using System.Collections;
using jp.nyatla.nyartoolkit.cs.markersystem;
using jp.nyatla.nyartoolkit.cs.core;
using NyARUnityUtils;
using System.IO;

/// <summary>
/// AR camera behaviour.
/// This sample shows simpleLite demo.
/// 1.Connect webcam to your computer.
/// 2.Start sample program
/// 3.Take a "HIRO" marker and "KANJI" on capture image
///
/// </summary>
public class SimpleLiteMBehaviour : MonoBehaviour
{
  private NyARUnityMarkerSystem _ms;
  private NyARUnityWebCam _ss;
  private int mid1;//marker id
  private int mid2;//marker id
  private int molecule_id;
  private GameObject _bg_panel;
  void Awake()
  {
    //setup unity webcam
    WebCamDevice[] devices= WebCamTexture.devices;
    WebCamTexture w;
    if (devices.Length > 0){
      w=new WebCamTexture(1280, 720, 15);
      this._ss=new NyARUnityWebCam(w);
      NyARMarkerSystemConfig config = new NyARMarkerSystemConfig(w.requestedWidth,w.requestedHeight);
      this._ms=new NyARUnityMarkerSystem(config);
      molecule_id = this._ms.addNyIdMarker(2,5);
      mid1 = this._ms.addNyIdMarker(0, 5);
      mid2 = this._ms.addNyIdMarker(1, 5);
      // mid1=this._ms.addARMarker(
      //  new StreamReader(new MemoryStream(((TextAsset)Resources.Load("patt_hiro",typeof(TextAsset))).bytes)),
      //  16,25,80);
      // mid2=this._ms.addARMarker(
      //  new StreamReader(new MemoryStream(((TextAsset)Resources.Load("patt_kanji",typeof(TextAsset))).bytes)),
      //  16,25,80);

      //setup background
      this._bg_panel=GameObject.Find("Plane");
      this._bg_panel.renderer.material.mainTexture=w;
      this._ms.setARBackgroundTransform(this._bg_panel.transform);
      
      //setup camera projection
      this._ms.setARCameraProjection(this.camera);

      //set gamemarker pos
      // this._ms.setMarkerTransform(molecule_id, GameObject.Find("Molecule").transform);
      
    }else{
      Debug.LogError("No Webcam.");
    }
  }
  // Use this for initialization
  void Start ()
  {
    //start sensor
    this._ss.start();
  }
  // Update is called once per frame
  void Update ()
  {
    //Update SensourSystem
    this._ss.update();
    //Update marker system by ss
    this._ms.update(this._ss);
    //update Gameobject transform
    if(this._ms.isExistMarker(molecule_id)){
      Vector3 pos = new Vector3();
      Quaternion rot = new Quaternion();
      this._ms.getMarkerTransform(molecule_id, ref pos, ref rot);

      Debug.Log(pos);
      Debug.Log(rot);

      GameObject.Find("Molecule").transform.position = pos;
      GameObject.Find("Molecule").transform.rotation = rot;
      

      // this._ms.setMarkerTransform(molecule_id, GameObject.Find("Molecule").transform);
    }else{
      // hide Game object
      GameObject.Find("Molecule").transform.localPosition=new Vector3(0,0,-100);
    }

    // if(this._ms.isExistMarker(mid1)){
    //  this._ms.setMarkerTransform(mid1,GameObject.Find("MarkerObject").transform);
    // }else{
    //  // hide Game object
    //  GameObject.Find("MarkerObject").transform.localPosition=new Vector3(0,0,-100);
    // }
    // if(this._ms.isExistMarker(mid2)){
    //  this._ms.setMarkerTransform(mid2,GameObject.Find("MarkerObject2").transform);
    // }else{
    //  // hide Game object
    //  GameObject.Find("MarkerObject2").transform.localPosition=new Vector3(0,0,-100);
    // }
  }
}

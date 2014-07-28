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
  private NyARUnityMarkerSystem nyMarkerSystem;
  private NyARUnityWebCam nyWebCam;
  private int mid1;//marker id
  private int mid2;//marker id
  private int molecule_id;
  private GameObject backgroundPanel;
  void Awake()
  {
    //setup unity webcam
    WebCamDevice[] devices = WebCamTexture.devices;
    WebCamTexture w;
    if (devices.Length > 0){
      w = new WebCamTexture(1280, 720, 15);
      this.nyWebCam = new NyARUnityWebCam(w);
      NyARMarkerSystemConfig config = new NyARMarkerSystemConfig(w.requestedWidth, w.requestedHeight);
      this.nyMarkerSystem=new NyARUnityMarkerSystem(config);
      molecule_id = this.nyMarkerSystem.addNyIdMarker(2,5);
      mid1 = this.nyMarkerSystem.addNyIdMarker(0, 5);
      mid2 = this.nyMarkerSystem.addNyIdMarker(1, 5);
      // mid1=this.nyMarkerSystem.addARMarker(
      //  new StreamReader(new MemoryStream(((TextAsset)Resources.Load("patt_hiro",typeof(TextAsset))).bytes)),
      //  16,25,80);
      // mid2=this.nyMarkerSystem.addARMarker(
      //  new StreamReader(new MemoryStream(((TextAsset)Resources.Load("patt_kanji",typeof(TextAsset))).bytes)),
      //  16,25,80);

      //setup background
      this.backgroundPanel = GameObject.Find("Plane");
      this.backgroundPanel.renderer.material.mainTexture = w;
      this.nyMarkerSystem.setARBackgroundTransform(this.backgroundPanel.transform);
      
      //setup camera projection
      this.nyMarkerSystem.setARCameraProjection(this.camera);

      //set gamemarker pos
      // this.nyMarkerSystem.setMarkerTransform(molecule_id, GameObject.Find("Molecule").transform);
      
    }else{
      Debug.LogError("No Webcam.");
    }
  }
  // Use this for initialization
  void Start ()
  {
    //start sensor
    this.nyWebCam.start();
  }
  // Update is called once per frame
  void Update ()
  {
    //Update SensourSystem
    this.nyWebCam.update();
    //Update marker system by ss
    this.nyMarkerSystem.update(this.nyWebCam);
    //update Gameobject transform
    if(this.nyMarkerSystem.isExistMarker(molecule_id)){
      Vector3 pos = new Vector3();
      Quaternion rot = new Quaternion();
      this.nyMarkerSystem.getMarkerTransform(molecule_id, ref pos, ref rot);

      Debug.Log("position: " + pos);
      Debug.Log("rotation: " + rot);

      GameObject.Find("Molecule").transform.position = pos;
      // GameObject.Find("Molecule").transform.Rotate(rot.eulerAngles);
      // GameObject.Find("Molecule").transform.localRotation = rot;
      GameObject.Find("Molecule").transform.rotation = rot;
      

      // this.nyMarkerSystem.setMarkerTransform(molecule_id, GameObject.Find("Molecule").transform);
    }else{
      // hide Game object
      // GameObject.Find("Molecule").transform.localPosition=new Vector3(0,0,-100);
    }

    // if(this.nyMarkerSystem.isExistMarker(mid1)){
    //  this.nyMarkerSystem.setMarkerTransform(mid1,GameObject.Find("MarkerObject").transform);
    // }else{
    //  // hide Game object
    //  GameObject.Find("MarkerObject").transform.localPosition=new Vector3(0,0,-100);
    // }
    // if(this.nyMarkerSystem.isExistMarker(mid2)){
    //  this.nyMarkerSystem.setMarkerTransform(mid2,GameObject.Find("MarkerObject2").transform);
    // }else{
    //  // hide Game object
    //  GameObject.Find("MarkerObject2").transform.localPosition=new Vector3(0,0,-100);
    // }
  }
}

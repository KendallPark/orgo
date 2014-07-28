using UnityEngine;
using System.Collections;
using NyARUnityUtils;
using jp.nyatla.nyartoolkit.cs.markersystem;
using jp.nyatla.nyartoolkit.cs.core;

namespace NyARUnityUtils
{
	/// <summary>
	/// This class provides WebCamTexture wrapper derived  from NyARMarkerSystemSensor.
	/// </summary>
	/// <exception cref='NyARException'>
	/// Is thrown when the ny AR exception.
	/// </exception>
	public class NyARUnityWebCam :NyARSensor
	{
		private WebCamTexture _wtx;
	    private NyARUnityRaster _raster;	
		public NyARUnityWebCam(WebCamTexture i_wtx): base(new NyARIntSize(i_wtx.requestedWidth,i_wtx.requestedHeight))
		{		
	        //Generation of RGB raster (Webtexture is upside down necessary)
	        this._raster = new NyARUnityRaster(i_wtx.requestedWidth,i_wtx.requestedHeight,true);
	        //Set of raster
	        base.update(this._raster);
			this._wtx=i_wtx;
		}
    /**
     * This function will start the asynchronous update of JMF.
     */
    public void stop()
    {
        this._wtx.Stop();
    }
    /**
     * This function will start the asynchronous update of JMF.
     */
    public void start()
    {
        this._wtx.Play();
    }
		/**
		 * Call this function on update!
		 */
		public void update()
		{
			if(!this._wtx.didUpdateThisFrame){
				return;
			}
			//Texture if it has been updated, and update the raster
			this._raster.updateByWebCamTexture(this._wtx);
			//To update the time stamp of the sensor
			base.updateTimeStamp();
			return;
		}
		public override void update(INyARRgbRaster i_input)
		{
			throw new NyARException();
		}
		public void dGetGsTex(Texture2D tx)
		{
			int[] s=(int[])this._gs_raster.getBuffer();
			Color32[] c=new Color32[320*240];
			for(int i=0;i<240;i++){
				for(int i2=0;i2<320;i2++){
					c[i*320+i2].r=c[i*320+i2].g=c[i*320+i2].b=(byte)s[i*320+i2];
					c[i*320+i2].a=0xff;
				}
			}
			tx.SetPixels32(c);
			tx.Apply( false );
		}
	}

}


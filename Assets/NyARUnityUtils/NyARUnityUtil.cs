using System;
using jp.nyatla.nyartoolkit.cs.core;
using UnityEngine;
namespace NyARUnityUtils
{
	public static class NyARUnityUtil
	{
		/**
		 * NyARToolKit 2.53 Scale value to be compatible with the previous code.
		 * {@link #toCameraFrustumRH} of i_scale It is set to be, and keeping compatibility with numeral system of previous versions.
		 */
		public const double SCALE_FACTOR_toCameraFrustumRH_NYAR2=1.0;
		/**
		 * NyARToolKit 2.53 Scale value to be compatible with the previous code.
		 * {@link #toCameraViewRH}It is set to be the i_scale, keeping compatibility with numeral system of previous versions.
		 */
		public const double SCALE_FACTOR_toCameraViewRH_NYAR2=1/0.025;
	
	
		/**
		 * From the camera parameters of ARToolKit style, this function calculates the CameraFrustam. 
		 * Among the elements of the camera parameters, we use the only ProjectionMatrix component. 
		 * @param i_arparam 
		 * ARToolKit camera parameters of style. 
		 * @param i_scale 
		 * I specifies the scale value. Is 1 = 1mm. It is a 1 = 1m if, 1000 1 = 1cm if 10. 
		 * When you make it compatible with previous NyARToolkit 2.53, please specify the {@ link # SCALE_FACTOR_toCameraFrustumRH_NYAR2}.
		 * @param i_near 
		 * I will specify the nearPoint of Mikiri-tai. Unit is determined by the value set in i_scale. 
		 * @param i_far 
		 * I will specify the farPoint of Mikiri-tai. Unit is determined by the value set in i_scale. 
		 * @param o_gl_projection 
		 * It is ProjectionMatrix of OpenGL style. I specify double [16].
		 */
		public static void toCameraFrustumRH(NyARParam i_arparam,double i_scale,double i_near,double i_far,ref Matrix4x4 o_mat)
		{
			toCameraFrustumRH(i_arparam.getPerspectiveProjectionMatrix(),i_arparam.getScreenSize(),i_scale,i_near,i_far,ref o_mat);
			return;
		}
		/**
		 * From ProjectionMatrix of ARToolKit style, this function calculates the CameraFrustam. 
		 * @param i_promat 
		 * @param i_size 
		 * I will specify the screen size.
		 * @param i_scale
		 * {@link #toCameraFrustumRH(NyARParam i_arparam,double i_scale,double i_near,double i_far,double[] o_gl_projection)} See.
		 * @param i_near
		 * {@link #toCameraFrustumRH(NyARParam i_arparam,double i_scale,double i_near,double i_far,double[] o_gl_projection)} See.
		 * @param i_far
		 * {@link #toCameraFrustumRH(NyARParam i_arparam,double i_scale,double i_near,double i_far,double[] o_gl_projection)} See.
		 * @param o_gl_projection
		 * {@link #toCameraFrustumRH(NyARParam i_arparam,double i_scale,double i_near,double i_far,double[] o_gl_projection)} See.
		 */
		public static void toCameraFrustumRH(NyARPerspectiveProjectionMatrix i_promat,NyARIntSize i_size,double i_scale,double i_near,double i_far,ref Matrix4x4 o_mat)
		{
			NyARDoubleMatrix44 m=new NyARDoubleMatrix44();
			i_promat.makeCameraFrustumRH(i_size.w,i_size.h,i_near*i_scale,i_far*i_scale,m);
			o_mat.m00=(float)m.m00;
			o_mat.m01=(float)m.m01;
			o_mat.m02=(float)m.m02;
			o_mat.m03=(float)m.m03;
			o_mat.m10=(float)m.m10;
			o_mat.m11=(float)m.m11;
			o_mat.m12=(float)m.m12;
			o_mat.m13=(float)m.m13;
			o_mat.m20=(float)m.m20;
			o_mat.m21=(float)m.m21;
			o_mat.m22=(float)m.m22;
			o_mat.m23=(float)m.m23;
			o_mat.m30=(float)m.m30;
			o_mat.m31=(float)m.m31;
			o_mat.m32=(float)m.m32;
			o_mat.m33=(float)m.m33;
			return;
		}
		public static void toCameraViewRH(ref Matrix4x4 mat,double i_scale,ref Matrix4x4 o_mat)
		{
			o_mat.m00  =(float)-mat.m00; 
			o_mat.m01  =(float)mat.m01;
			o_mat.m02  =(float)mat.m02;

			o_mat.m10  =(float)mat.m10;
			o_mat.m11  =(float)-mat.m11;
			o_mat.m12  =(float)-mat.m12;

			o_mat.m20  =(float)-mat.m20;			
			o_mat.m21  =(float)mat.m21;			
			o_mat.m22  =(float)mat.m22;
			
			o_mat.m30  =(float)0.0;			
			o_mat.m31  =(float)0.0;
			o_mat.m32  = (float)0.0;		
			double scale=1/i_scale;
			o_mat.m03 =(float)(mat.m03*scale);
			o_mat.m13 =-(float)(mat.m13*scale);
			o_mat.m23 =(float)(mat.m23*scale);
			o_mat.m33 = (float)1.0;
			return;
		}		
		/**
		 * This function converts to ModelView matrix of the OpenGL NyARTransMatResult. 
		 * @param mat 
		 * Matrix of the transformation source 
		 * @param i_scale 
		 * I specifies the scale value of the coordinate system. Is 1 = 1mm. It is a 1 = 1m if, 1000 1 = 1cm if 10. 
		 * When you make it compatible with previous NyARToolkit 2.53, please specify the {@ link # SCALE_FACTOR_toCameraViewRH_NYAR2}. 
		 * @param o_gl_result 
		 * It is ProjectionMatrix of OpenGL style. I specify double [16].
		 */

		public static void toCameraViewRH(NyARDoubleMatrix44 mat,double i_scale,ref Matrix4x4 o_mat)
		{
			o_mat.m00  =(float)-mat.m00; 
			o_mat.m01  =(float)mat.m01;
			o_mat.m02  =(float)mat.m02;

			o_mat.m10  =(float)mat.m10;
			o_mat.m11  =(float)-mat.m11;
			o_mat.m12  =(float)-mat.m12;

			o_mat.m20  =(float)-mat.m20;			
			o_mat.m21  =(float)mat.m21;			
			o_mat.m22  =(float)mat.m22;
			
			o_mat.m30  =(float)0.0;			
			o_mat.m31  =(float)0.0;
			o_mat.m32  = (float)0.0;		
			double scale=1/i_scale;
			o_mat.m03 =(float)(mat.m03*scale);
			o_mat.m13 =-(float)(mat.m13*scale);
			o_mat.m23 =(float)(mat.m23*scale);
			o_mat.m33 = (float)1.0;
			return;
		}
		public static void toCameraViewRH(NyARDoubleMatrix44 mat,double i_scale,ref NyARDoubleMatrix44 o_mat)
		{
			o_mat.m00  =(float)-mat.m00; 
			o_mat.m01  =(float)mat.m01;
			o_mat.m02  =(float)mat.m02;

			o_mat.m10  =(float)mat.m10;
			o_mat.m11  =(float)-mat.m11;
			o_mat.m12  =(float)-mat.m12;

			o_mat.m20  =(float)-mat.m20;			
			o_mat.m21  =(float)mat.m21;			
			o_mat.m22  =(float)mat.m22;
			
			o_mat.m30  =(float)0.0;			
			o_mat.m31  =(float)0.0;
			o_mat.m32  = (float)0.0;		
			double scale=1/i_scale;
			o_mat.m03 =(float)(mat.m03*scale);
			o_mat.m13 =-(float)(mat.m13*scale);
			o_mat.m23 =(float)(mat.m23*scale);
			o_mat.m33 = (float)1.0;
			return;
		}		
		public static void toCameraViewRH(NyARDoubleMatrix44 mat,double i_scale,ref Vector3 o_pos,ref Quaternion o_rot)
		{
			mat2Rot(
				-mat.m00,mat.m01,mat.m02,
				mat.m10,-mat.m11,-mat.m12,
				-mat.m20,mat.m21,mat.m22,
				ref o_rot);			
			double scale=1/i_scale;
			o_pos.x =(float)(mat.m03*scale);
			o_pos.y =-(float)(mat.m13*scale);
			o_pos.z =(float)(mat.m23*scale);
			return;
		}
		public static void toCameraViewRH(ref Matrix4x4 mat,double i_scale,ref Vector3 o_pos,ref Quaternion o_rot)
		{
			mat2Rot(
				-mat.m00,mat.m01,mat.m02,
				mat.m10,-mat.m11,-mat.m12,
				-mat.m20,mat.m21,mat.m22,
				ref o_rot);			
			double scale=1/i_scale;
			o_pos.x =(float)(mat.m03*scale);
			o_pos.y =-(float)(mat.m13*scale);
			o_pos.z =(float)(mat.m23*scale);
			return;
		}		
		/// <summary>
		/// I break down and Vector Rotation to the matrix.
		/// </summary>
		/// <param name='mat'>
		/// Mat.
		/// </param>
		/// <param name='i_scale'>
		/// I_scale.
		/// </param>
		/// <param name='o_pos'>
		/// O_pos.
		/// </param>
		/// <param name='o_rot'>
		/// O_rot.
		/// </param>
		public static void mat2UnityVecRot(NyARDoubleMatrix44 mat,double i_scale,ref Vector3 o_pos,ref Quaternion o_rot)
		{
			mat2Rot(
				mat.m00,mat.m01,mat.m02,
				mat.m10,mat.m11,mat.m12,
				mat.m20,mat.m21,mat.m22,ref o_rot);
			double scale=1/i_scale;
			o_pos.x =(float)(mat.m03*scale);
			o_pos.y =(float)(mat.m13*scale);
			o_pos.z =(float)(mat.m23*scale);
			return;
		}
		public static void mat2UnityVecRot(ref Matrix4x4 mat,double i_scale,ref Vector3 o_pos,ref Quaternion o_rot)
		{
			mat2Rot(
				mat.m00,mat.m01,mat.m02,
				mat.m10,mat.m11,mat.m12,
				mat.m20,mat.m21,mat.m22,ref o_rot);
			double scale=1/i_scale;
			o_pos.x =(float)(mat.m03*scale);
			o_pos.y =(float)(mat.m13*scale);
			o_pos.z =(float)(mat.m23*scale);
			return;			
		}
		private static void mat2Rot(
			double m00,double m01,double m02,
			double m10,double m11,double m12,
			double m20,double m21,double m22,
			ref Quaternion o_rot)
		{
			// Find the maximum component
		    double elem0 = m00 - m11 - m22 + 1.0f;
		    double elem1 = -m00 + m11 - m22 + 1.0f;
		    double elem2 = -m00 - m11 + m22 + 1.0f;
		    double elem3 = m00 + m11 + m22 + 1.0f;
			if(elem0>elem1 && elem0>elem2 && elem0>elem3){
			    double v = Math.Sqrt(elem0) * 0.5f;
			    double mult = 0.25f / v;
				o_rot.x = (float)v;
		        o_rot.y = (float)((m10 + m01) * mult);
		        o_rot.z = (float)((m02 + m20) * mult);
		        o_rot.w = (float)((m21 - m12) * mult);
			}else if(elem1>elem2 && elem1>elem3){
			    double v = Math.Sqrt(elem1) * 0.5f;
			    double mult = 0.25f / v;
		        o_rot.x = (float)((m10 + m01) * mult);
				o_rot.y = (float)(v);
		        o_rot.z = (float)((m21 + m12) * mult);
		        o_rot.w = (float)((m02 - m20) * mult);
			}else if(elem2>elem3){
			    double v = Math.Sqrt(elem2) * 0.5f;
			    double mult = 0.25f / v;
		        o_rot.x =(float)((m02 + m20) * mult);
		        o_rot.y =(float)((m21 + m12) * mult);
				o_rot.z =(float)(v);
		        o_rot.w =(float)((m10 - m01) * mult);
			}else{
			    double v = Math.Sqrt(elem3) * 0.5f;
			    double mult = 0.25f / v;
		        o_rot.x =(float)((m21 - m12) * mult);
		        o_rot.y =(float)((m02 - m20) * mult);
		        o_rot.z =(float)((m10 - m01) * mult);
				o_rot.w =(float)v;
			}			
		}
			
	}
}


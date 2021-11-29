using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
	
	void Awake()
	{
		Camera cam= GetComponent<Camera>();

		if( ScreenRatio <= 0.55f )
			cam.fieldOfView= 46f;
	}

	public static int ScreenWidth { get { return Mathf.Min( Screen.width, Screen.height ); } }
	public static int ScreenHeight { get { return Mathf.Max( Screen.width, Screen.height ); } }

	public static float ScreenRatio { get { return ScreenWidth / (float)ScreenHeight; } }

	public static bool isIPhoneXSeries { get { return	(ScreenWidth == 1125 && ScreenHeight == 2436) ||
														(ScreenWidth == 1242 && ScreenHeight == 2688) ||
														(ScreenWidth == 828 && ScreenHeight == 1792); }	}

	public static bool isIPhoneX { get { return ScreenWidth == 1125 && ScreenHeight == 2436; }	}
	public static bool isIPhoneXR { get { return ScreenWidth == 828 && ScreenHeight == 1792; }	}
	public static bool isIPhoneXSMax { get { return ScreenWidth == 1242 && ScreenHeight == 2688; }	}

}

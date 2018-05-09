using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;
using System.Collections;

public class DynamicLightAboutWindow : EditorWindow {

	static DynamicLightAboutWindow  window;
	//public Rect _handleArea;
	private bool _nodeOption, _options, _handleActive, _action;
	private Texture2D _rawIcon;
	private GUIContent _icon;
	private float _winMinX, _winMinY;
	private int _mainwindowID;

	[MenuItem("Window/Node Editor Layout")]
	static void Init()
	{
		//window = (DynamicLightAboutWindow )EditorWindow.GetWindow(typeof(DynamicLightAboutWindow));
		//EditorWindow.GetWindow( typeof(DynamicLightAboutWindow), true, "2DDL" );
		int result = EditorUtility.DisplayDialogComplex("", "2D Lights and Shadows PRO v1.3.1 \n http://www.martinysa.com", "Check updates" ,"Go to web", "Close");
		switch (result) {
		case 0:
			//checkUpdates();
			//EditorCoroutine.start(tryToLoadPHP());

			break;

		case 1:
			Application.OpenURL("http://www.martinysa.com");
			break;

		case 2:
			break;

		default:
			break;
		}

	}


	static IEnumerator tryToLoadPHP()
	{
		yield return new WaitForSeconds(15f);
		Debug.Log("works");
	}
	
	void OnGUI()
	{
			
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();

		EditorGUILayout.LabelField("2DDL PRO v1.3.1");

		GUILayout.EndHorizontal();

	}
}

using UnityEngine;
using System.Collections;
using UnityEditor;
using System;


/// <summary>
/// Tag layer class. Revision 1.3.1 . Loop between layers, and only create "Layer Name" if doesn't exist 
/// and also is slot are null or empty.
/// </summary>
#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class TagLayerClass{

	internal const string LayerName = "ShadowLayer-2DDL";
	internal const string msg = "2DDL is trying to set the Shadow Layer: " + LayerName + " . Do you allow to 2DDL create a new layer in a empty slot?";


	static TagLayerClass()
	{
		findLayer(LayerName);
		createLayer();

	}

	static bool layerHasBeenCreated(){
		int r = PlayerPrefs.GetInt("2ddlLayerCreated",0);
		if(r > 0){
			return true;
		}else{
			return false;
		}
	}

	static void SaveNoLayerExist(){
		PlayerPrefs.SetInt("2ddlLayerCreated",0);
	}
	static void SaveWhenCreateLayer(){
		PlayerPrefs.SetInt("2ddlLayerCreated",1);//
	}

	static void findLayer(string layerName){
		SerializedObject SerializedObjectTagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
		bool showChildren = true;
		bool layerWasCreated = false;

		SerializedProperty layers = SerializedObjectTagManager.FindProperty("layers");
		if (layers == null || !layers.isArray)
		{
			Debug.LogWarning("Can't set up the layers.  It's possible the format of the layers and tags data has changed in this version of Unity.");
			Debug.LogWarning("Layers is null: " + (layers == null));
			return;
		}

		while(layers.NextVisible (showChildren))
		{
			if(layers.displayName.Contains("Elem") && layers.stringValue.Contains(layerName)){
				layerWasCreated = true;//
				break;
			}

		}

		if (!layerWasCreated) {//
			SaveNoLayerExist();
		}

	}
	
	static void createLayer(){


		SerializedObject SerializedObjectTagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
		bool showChildren = true;
		bool layerWasCreated = layerHasBeenCreated();

		if(layerWasCreated)
			return;

#if UNITY_5

		if(EditorUtility.DisplayDialog("2DDL Pro",msg,"Yes", "Cancel")){


			SerializedProperty layers = SerializedObjectTagManager.FindProperty("layers");
			if (layers == null || !layers.isArray)
			{
				Debug.LogWarning("Can't set up the layers.  It's possible the format of the layers and tags data has changed in this version of Unity.");
				Debug.LogWarning("Layers is null: " + (layers == null));
				return;
			}

			int countLayer = 0;
			while(layers.NextVisible (showChildren))
			{

				if(countLayer > 8){
					if(layers.displayName.Contains("Elem") && string.IsNullOrEmpty(layers.stringValue)){
						//Debug.Log(layers.displayName);
						//Debug.Log(layers.stringValue);//
						layers.stringValue = LayerName;
						SaveWhenCreateLayer();
						// display ok
						EditorUtility.DisplayDialog("2DDL Pro", "Layer [" + LayerName + "] has been created in User Layer Slot " + (countLayer-1), "Ok");
						break;
					}
				}

				if(layers.editable)
					countLayer++;
			}
		}


			




#else


		if(EditorUtility.DisplayDialog("2DDL Setup Layer Name",msg,"Yes", "Cancel")){

			SerializedProperty it = SerializedObjectTagManager.GetIterator();

			while(it.NextVisible (showChildren)){


				string a = it.displayName;
				bool canLoop = a.Contains("User Layer");
				if(canLoop && layerWasCreated == false){
					if(string.IsNullOrEmpty(it.stringValue)){
						it.stringValue = LayerName;
						SaveWhenCreateLayer();
						break;
					}
				}

			}
		}
#endif

		SerializedObjectTagManager.ApplyModifiedProperties();

	}
}

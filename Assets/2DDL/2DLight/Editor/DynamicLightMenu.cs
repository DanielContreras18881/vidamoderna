using UnityEngine;
using UnityEditor;
using System.Collections;

public class DynamicLightMenu : Editor {

	static internal DynamicLight light;
	const string menuPath = "GameObject/2D Dynamic Light [2DDL]";
	
	internal void OnEnable(){
		light = target as DynamicLight;
	}




	[MenuItem ( menuPath + "/Lights/ ☼ Radial NO Material",false,20)]
	static void Create(){
		GameObject gameObject = new GameObject("2DLight");
		gameObject.AddComponent<DynamicLight>();
		//s.Rebuild();
	}
	
	[MenuItem ( menuPath + "/Lights/ ☀ Radial Procedural Gradient ",false,31)]
	static void addGradient(){
		
		Material m = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Materials/StandardLightMaterialGradient.mat", typeof(Material)) as Material;
		GameObject gameObject = new GameObject("2DLight");
		DynamicLight s = gameObject.AddComponent<DynamicLight>();
		s.setMainMaterial(m);
		s.Rebuild();
		
	}
	
	[MenuItem ( menuPath + "/Lights/ ☀ Radial with Texture",false,32)]
	static void addTexturized(){
		
		Material m = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Materials/StandardLightTexturizedMaterial.mat", typeof(Material)) as Material;
		GameObject gameObject = new GameObject("2DLight");
		DynamicLight s = gameObject.AddComponent<DynamicLight>();
		s.SolidColor = true;
		s.LightColor = Color.red;
		s.setMainMaterial(m);
		s.Rebuild();
		
	}

	[MenuItem ( menuPath + "/Lights/ ☀ Radial with Ilumination ",false,33)]
	static void addRadialIlum(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Lights/2DDL-Point2DLightWithIlumination.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "2DDL-Point2DLightWithIlumination";
	}

		
	[MenuItem ( menuPath + "/Lights/ ☀ Spot ",false,45)]
	static void addSpot(){
		
		Material m = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Materials/StandardLightMaterialGradient.mat", typeof(Material)) as Material;
		GameObject gameObject = new GameObject("2DLight");
		DynamicLight s = gameObject.AddComponent<DynamicLight>();
		s.setMainMaterial(m);
		s.Rebuild();
		s.Segments = 4;
		s.RangeAngle = 90;
		
	}

	[MenuItem ( menuPath + "/Lights/ ☀ Spot with Ilumination ",false,46)]
	static void addSpotIlum(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Lights/2DDL-Spot2DWithIlumination.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "2DDL-Point2DLightWithIlumination";
	}

	
	
	[MenuItem ( menuPath + "/Casters/Empty",false,20)]
	static void addEmpty(){
		
		GameObject empty = new GameObject("empty");
		int layerDest =  light.getLayerNumberFromLayerMask(light.Layer.value);
		empty.layer = layerDest;
		empty.AddComponent<SpriteRenderer>();
		GameObject emptyChild = new GameObject("collider");
		emptyChild.AddComponent<PolygonCollider2D>();
		emptyChild.transform.parent = empty.transform;
		emptyChild.layer = empty.layer;
		empty.transform.position = new Vector3(5,0,0);
		DynamicLight.reloadMeshes = true;
	}
	
	[MenuItem ( menuPath + "/Casters/ ☐ Square - PolygonCollider2D",false,31)]
	static void addSquare(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/square.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		if(light){
			int layerDest =  light.getLayerNumberFromLayerMask(light.Layer.value);
			hex.layer = layerDest;
			hex.transform.Find("collider").gameObject.layer = layerDest;
		}
		
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Square";
		DynamicLight.reloadMeshes = true;
	}
	
	[MenuItem ( menuPath + "/Casters/ ☐ Square - BoxCollider2D",false,32)]
	static void addSquare2(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/square - BoxCollider2D.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

		hex.transform.localEulerAngles = new Vector3 (0, 0, 0.001f);
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Square - BoxCollider2D";
		DynamicLight.reloadMeshes = true;
	}

	[MenuItem ( menuPath + "/Casters/ ☐ Square Iluminated",false,32)]
	static void addSquareIlum(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/Iluminated - Square.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Square - Iluminated";
		DynamicLight.reloadMeshes = true;
	}
	
	[MenuItem ( menuPath +"/Casters/ ◯ Circle - polygonCollider2D",false,43)]
	static void addCircle(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/circle.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		if(light){
			int layerDest =  light.getLayerNumberFromLayerMask(light.Layer.value);
			hex.layer = layerDest;
			hex.transform.Find("collider").gameObject.layer = layerDest;
		}
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Circle";
		DynamicLight.reloadMeshes = true;
		
	}
	
	[MenuItem ( menuPath + "/Casters/ ◯ Circle - CircleCollider2D",false,44)]
	static void addCircle2(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/circle - CircleCollider2D.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		if(light){
			int layerDest =  light.getLayerNumberFromLayerMask(light.Layer.value);
			hex.layer = layerDest;
			//hex.transform.FindChild("collider").gameObject.layer = layerDest;
		}
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Circle - CircleCollider2D";
		DynamicLight.reloadMeshes = true;
		
	}
	
	[MenuItem ( menuPath + "/Casters/ ◯ Circle - Intellider",false,45)]
	static void addIntelliderCircle(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/circleWithintellider.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		if(light){
			int layerDest =  light.getLayerNumberFromLayerMask(light.Layer.value);
			hex.layer = layerDest;
			hex.transform.Find("collider").gameObject.layer = layerDest;
		}
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Circle - Intellider";
		DynamicLight.reloadMeshes = true;
		
	}
	[MenuItem ( menuPath + "/Casters/ ◯ Circle Iluminated",false,45)]
	static void addCircleIlum(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/Iluminated - Circle.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Circle - Iluminated";
		DynamicLight.reloadMeshes = true;
	}
	
	[MenuItem ( menuPath + "/Casters/ ⎯ ⎯ ⎯ Line Edge - EdgeCollider2D",false,56)]
	static void addEdge(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/edge - EdgeCollider2D.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		if(light){
			int layerDest =  light.getLayerNumberFromLayerMask(light.Layer.value);
			hex.layer = layerDest;
			//hex.transform.FindChild("collider").gameObject.layer = layerDest;
		}
		hex.transform.position = new Vector3(5,3,0);
		hex.name = "Edge - EdgeCollider2D";
		DynamicLight.reloadMeshes = true;
		
	}
	
	[MenuItem ( menuPath + "/Casters/Hexagon",false,67)]
	static void addHexagon(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/hexagon.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		if(light){
			int layerDest =  light.getLayerNumberFromLayerMask(light.Layer.value);
			hex.layer = layerDest;
			hex.transform.Find("collider").gameObject.layer = layerDest;
		}
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Hexagon";
		DynamicLight.reloadMeshes = true;
		
	}
	[MenuItem ( menuPath + "/Casters/Pacman",false,68)]
	static void addPacman(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/pacman.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Pacman";
		DynamicLight.reloadMeshes = true;
		
	}
	[MenuItem ( menuPath + "/Casters/Star",false,69)]
	static void addStar(){
		
		Object prefab = AssetDatabase.LoadAssetAtPath("Assets/2DDL/Prefabs/Casters/star.prefab", typeof(GameObject));
		GameObject hex = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		hex.transform.position = new Vector3(5,0,0);
		hex.name = "Star";
		DynamicLight.reloadMeshes = true;
	}





	///*
	[MenuItem ( menuPath + "/About/• Open in AssetStore",false,120)]
	static void openAssetStore(){
		Application.OpenURL("http://u3d.as/asp");
	}

	[MenuItem ( menuPath + "/About/• OnLine Documentation",false,131)]
	static void gotoDoc(){
		Application.OpenURL("http://martinysa.com/2d-dynamic-lights-doc/");
	}

	[MenuItem ( menuPath + "/About/• Contact Developer",false,132)]
	static void gotoMail(){
		Application.OpenURL("mailto:info@martinysa.com");
	}
//*/




}

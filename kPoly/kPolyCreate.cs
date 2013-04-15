/** http://www.k-lock.de  | Paul Knab 
 * 	_______________________________________
 * 	
 * 	kPolyCreate | V.1.0.1 | 05.04.2013
 *  ________________________________________
 * 
 * 	Editor Window for mesh creation  
 * 	in Unity3D Scene Editor
 * 
 * 
 * 	Properties for generating mesh component
 * 
 * 	- Segments 
 * 	- Size
 * 	- Pivot point for mesh
 * 	- Facing direction
 * 	- Triangle winding
 * 
 *  Extra :
 *  
 *  - Add your favorite MeshCollider / BoxCollider
 * 
 * */
using UnityEngine;
using UnityEditor;
using klock.geometry;

public class kPolyCreate : EditorWindow
{
    #region vars
	/** Static instance to this editor class. */
	public static kPolyCreate 	instance;
	private 	  string 		_meshName = "";
	private 	  float 		_width = 1;
	private 	  float 		_height = 1;
	private 	  float 		_depth = 1;
	private 	  int 			_uSegments = 1;
	private 	  int			_vSegments = 1;
	private 	  int			_zSegments = 1;
	private 	  int 			_pivotIndex = 0;
	//private 	  TextAnchor 	_pivot = TextAnchor.MiddleCenter;
	private 	  string[] 		_pivotLabels = {"UpperLeft","UpperCenter","UpperRight", "MiddleLeft","MiddleCenter","MiddleRight", "LowerLeft", "LowerCenter","LowerRight"};
	private 	  int 			_faceIndex = 0;
	//private 	  string[] 		_faceLabels = klock.geometry.kPoly.FACING; //;{"XZ","XY"};
	private		  string[]		_windinLabels = { "TopLeft","TopRight", "ButtomLeft", "ButtomRight" };
	private		  int 			_windinIndex = 2;
	private		  string[]		_colliderLabels = { "none" ,"MeshCollider", "BoxCollider" };
	private		  int 			_colliderIndex = 1;
	
	#endregion
	#region Editor
	/** The Unity EditorWindow start function.*/
	[MenuItem("Window/klock/kMesh/kPolyCreate %M1")]
	public static kPolyCreate Init ()
	{
		instance = (kPolyCreate)EditorWindow.GetWindow (typeof(kPolyCreate), false, "Create");
		instance.Show ();
		//	instance.OnEnable ();
		instance.position = new Rect (200, 100, 200, 228);
		
		return instance;
	}

	public static kPolyCreate Create ()
	{
		return CreateInstance<kPolyCreate> ();
	}
	/** Reset the editor values to default.*/
	private void ResetEditorValues ()
	{
		_width = 1;
		_height = 1;
		_depth = 1;
			
		_uSegments = 1;
		_vSegments = 1;
			
		_pivotIndex = 0;
		_faceIndex = 0;
		_colliderIndex = 1;
		_windinIndex = 2;
	}
    #endregion
	#region Unity
	private void OnEnable ()
	{
		if (instance == null) {
			instance = this;	
		}
		ResetEditorValues ();
	}

	/*private void OnDisable ()
	{
	
	}
	
	private void Update ()
	{
	
	}*/

	private void OnGUI ()
	{	
		DrawPanel ();
	}
	
	
	
	#endregion
	#region Editor GUI
	/** Main GUI draw function.*/
	
	public void DrawPanel ()
	{
		//Debug.Log(instance);
		DrawPanel3 ();
	}

	private void DrawPanel3 ()
	{
		bool GUI_TEMP = GUI.enabled;
		EditorGUILayout.BeginVertical ();//new GUIStyle { contentOffset = new Vector2 (0, 0) });
		//OBJECT_TYPES_INDEX = EditorGUILayout.Popup (OBJECT_TYPES_INDEX, OBJECT_TYPES);
		
		// OBJECT TYPE 
		FOLD_object = EditorGUILayout.Foldout (FOLD_object, "Object Type");
		if (FOLD_object) {
			GUILayout.BeginHorizontal ();
			//EditorGUILayout.Space ();
			GUI.color = Color.white;
			GUI.color = (P_OBJECT_TYPE_INDEX == 0) ? Color.grey : Color.white;
			if (GUILayout.Button ("Cube")) {
				P_OBJECT_TYPE_INDEX = (P_OBJECT_TYPE_INDEX == 0) ? -1 : 0;
			}
			//EditorGUILayout.Space ();
			GUI.color = Color.white;
			GUI.color = (P_OBJECT_TYPE_INDEX == 1) ? Color.grey : Color.white;
			if (GUILayout.Button ("Sphere")) {
				P_OBJECT_TYPE_INDEX = (P_OBJECT_TYPE_INDEX == 1) ? -1 : 1;
			}
			//EditorGUILayout.Space ();
			GUI.color = Color.white;
			GUI.color = (P_OBJECT_TYPE_INDEX == 2) ? Color.grey : Color.white;
			if (GUILayout.Button ("Plane")) {
				P_OBJECT_TYPE_INDEX = (P_OBJECT_TYPE_INDEX == 2) ? -1 : 2;
			}
			GUI.color = Color.white;
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
			//EditorGUILayout.Space ();
            GUI.color = Color.white;
            GUI.color = (P_OBJECT_TYPE_INDEX == 3) ? Color.grey : Color.white;
            if (GUILayout.Button("Cone"))
            {
                P_OBJECT_TYPE_INDEX = (P_OBJECT_TYPE_INDEX == 3) ? -1 : 3;
            }
            GUI.color = Color.white;
            //EditorGUILayout.Space ();
            GUILayout.EndHorizontal();
		}
		EditorGUILayout.Space ();
		// OBJECT NAME
		FOLD_name = EditorGUILayout.Foldout (FOLD_name, "Object Name");
		
		if (FOLD_name) {
			GUILayout.BeginHorizontal ();
			EditorGUILayout.Space ();
			GUI.enabled = P_OBJECT_TYPE_INDEX != -1;
			_meshName = EditorGUILayout.TextField (_meshName);
			GUI.enabled = GUI_TEMP;
			EditorGUILayout.Space ();
			GUILayout.EndHorizontal ();
		}
		EditorGUILayout.Space ();
		// OBJECT PARAMETERS
		FOLD_para = EditorGUILayout.Foldout (FOLD_para, "Parameters");
		if (FOLD_para) {
			GUILayout.BeginHorizontal ();
			GUI.enabled = P_OBJECT_TYPE_INDEX != -1;
			
			switch (P_OBJECT_TYPE_INDEX) {
			case 0:
				DrawPanel_Cube ();
				break;	
			case 1:
				
				break;	
			case 2:
				DrawPanel2 ();
				break;
            case 3:
                DrawPanelCone();
                break;	
			}
				
			
			GUILayout.EndHorizontal ();
		}
		EditorGUILayout.EndHorizontal ();
	}
    public int numVertices = 10;
    public float radiusTop = 0f;
    public float radiusBottom = 1f;
    public float length = 1f;
    public float openingAngle = 0f; // if >0, create a cone with this angle by setting radiusTop to 0, and adjust radiusBottom according to length;
    public bool outside = true;
    public bool inside = false;
    public bool addCollider = false;

    private void DrawPanelCone()
    {
        EditorGUILayout.BeginVertical();

        // Editor value reset button
        if (GUILayout.Button(new GUIContent("Reset Editor"), EditorStyles.miniButton))
        {
            ResetEditorValues();
        }
        EditorGUILayout.Space();
        numVertices = EditorGUILayout.IntField("numVertices", numVertices);
        EditorGUILayout.Space();
        radiusTop = EditorGUILayout.FloatField("radiusTop", radiusTop);
        radiusBottom = EditorGUILayout.FloatField("radiusBottom", radiusBottom);
        length = EditorGUILayout.FloatField("length", length);
        EditorGUILayout.Space();
        openingAngle = EditorGUILayout.FloatField("openingAngle", openingAngle);
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        outside = EditorGUILayout.Toggle("outside", outside);
        inside = EditorGUILayout.Toggle("inside", inside);
        GUILayout.EndHorizontal();
        if (GUILayout.Button(new GUIContent("Create Mesh")))
        {
            kPoly.Create_Cone_Object(_meshName, numVertices, radiusTop, radiusBottom, length, openingAngle, outside, inside);
        }
        EditorGUILayout.EndVertical();
    }
	private void DrawPanel_Cube ()
	{
		EditorGUILayout.BeginVertical ();
		
		// Editor value reset button
		if (GUILayout.Button (new GUIContent ("Reset Editor"), EditorStyles.miniButton)) {
			ResetEditorValues ();
		}
		EditorGUILayout.Space ();
		// Editor value for width and height of the created mesh [ float ]
		_width = EditorGUILayout.FloatField ("Width", _width);
		_height = EditorGUILayout.FloatField ("Height", _height);
		_depth = EditorGUILayout.FloatField ("Depth", _depth);
		EditorGUILayout.Space ();
		// Editor value for width and height segments of the created mesh [ int ]
		_uSegments = EditorGUILayout.IntField ("uSegments", _uSegments);
		_vSegments = EditorGUILayout.IntField ("vSegments", _vSegments);
		_zSegments = EditorGUILayout.IntField ("zSegments", _zSegments);
		EditorGUILayout.Space ();
		
		if (GUILayout.Button (new GUIContent ("Create Mesh"))) {
			//CreateMesh ();
			//CreateCube.CreateMesh 
			kPoly.Create_Cube_Object(_meshName, _uSegments, _vSegments, _zSegments, _width, _height, _depth 
                /*_faceIndex, _windinIndex, _pivotIndex*/);

		}
		EditorGUILayout.EndVertical ();

	}

	private void DrawPanel2 ()
	{
		EditorGUI.BeginChangeCheck ();
		EditorGUILayout.BeginVertical ();//new GUIStyle { contentOffset = new Vector2 (0, 0) });
		GUILayout.BeginHorizontal ();
		// Editor value reset button
		if (GUILayout.Button (new GUIContent ("Reset Editor"), EditorStyles.miniButton)) {
			ResetEditorValues ();
		}
		GUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		// Editor value for width and height of the created mesh [ float ]
		_width = EditorGUILayout.FloatField ("Width", _width);
		_height = EditorGUILayout.FloatField ("Height", _height);
		EditorGUILayout.Space ();
		// Editor value for width and height segments of the created mesh [ int ]
		_uSegments = EditorGUILayout.IntField ("uSegments", _uSegments);
		_vSegments = EditorGUILayout.IntField ("vSegments", _vSegments);
		EditorGUILayout.Space ();
		GUILayout.BeginHorizontal ();
		// Editor value for the pivot point of the created mesh Unity.TextAnchor
		GUILayout.Label ("Pivot");
		GUILayout.Space (18);
		_pivotIndex = EditorGUILayout.Popup (_pivotIndex, _pivotLabels);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		// Editor value for the mesh face direction FACING.XZ
		GUILayout.Label ("Facing ");
		GUILayout.Space (10);
		_faceIndex = EditorGUILayout.Popup (_faceIndex, klock.geometry.kPoly.FACING);
		GUILayout.EndHorizontal ();	
		GUILayout.BeginHorizontal ();
		// Editor value for triangle winding order
		GUILayout.Label ("Winding");
		GUILayout.Space (2);
		_windinIndex = EditorGUILayout.Popup (_windinIndex, _windinLabels);
		GUILayout.EndHorizontal ();	
		GUILayout.BeginHorizontal ();
		// Editor value for collider export
		GUILayout.Label ("Collider ");
		GUILayout.Space (3);
		_colliderIndex = EditorGUILayout.Popup (_colliderIndex, _colliderLabels);
		GUILayout.EndHorizontal ();	
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		// Starting GUI changes check
		if (EditorGUI.EndChangeCheck ()) {
			_width = Mathf.Clamp (_width, 0, int.MaxValue);
			_height = Mathf.Clamp (_height, 0, int.MaxValue);
			_uSegments = Mathf.Clamp (_uSegments, 1, int.MaxValue);
			_vSegments = Mathf.Clamp (_vSegments, 1, int.MaxValue);
			Debug.Log ("Change Editor");
		}
		// Editor Button for start mesh creation
		if (GUILayout.Button (new GUIContent ("Create Mesh"))) {
			//CreateMesh ();
			//CreatePlane.CreateMesh 
			kPoly.Create_Plane_Object
								  (_meshName,
									_uSegments, _vSegments,
									_width, _height,
									_faceIndex, _windinIndex, _pivotIndex, _colliderIndex);
		}
		EditorGUILayout.EndVertical ();
	}

	#endregion
	private 	bool 		FOLD_para = true;
	private 	bool 		FOLD_name = false;
	private 	bool 		FOLD_object = false;
	private 	int 		OBJECT_TYPES_INDEX = 0;
	private 	string[] 	OBJECT_TYPES = new string[2]{"Standard","Extra"};
	private 	int 		P_OBJECT_TYPE_INDEX = 0;
	private 	string[]	P_OBJECT_TYPE = new string[4]{"Cube","Sphere","Plane","Cone"};
}

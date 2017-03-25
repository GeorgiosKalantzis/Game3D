using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour {

    public Vector3 Lastposition = Vector3.zero;

    public Quaternion Lastrotation = Quaternion.identity;

    public Transform trans = null;

	void SaveProject()
    {
        string Outputpath = Application.persistentDataPath + @"\Objectposition.json";

        Lastposition = trans.position;

        Lastrotation = trans.rotation;

        StreamWriter writer = new StreamWriter(Outputpath);

        writer.WriteLine(JsonUtility.ToJson(this));

        writer.Close();

        Debug.Log(Outputpath);
    }
    void LoadProject()
    {
        string Inputpath = Application.persistentDataPath + @"\Objectposition.json";

        StreamReader read = new StreamReader(Inputpath);

        string JSONstring = read.ReadToEnd();

        Debug.Log(JSONstring);

        JsonUtility.FromJsonOverwrite(JSONstring, this);

        read.Close();

        trans.position = Lastposition;

        trans.rotation = Lastrotation;

   }

	void Awake () {
        trans = GetComponent<Transform>();
		
	}
    void Start()
    {
        LoadProject();
        SaveProject();
    }

}

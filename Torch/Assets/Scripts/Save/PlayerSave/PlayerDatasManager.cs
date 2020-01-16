using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class PlayerDatasManager {

	public static void Save(object entity, string fileName){
		BinaryFormatter formateur = new BinaryFormatter();
		FileStream stream = File.Create (Application.persistentDataPath + "/" + fileName);
		formateur.Serialize(stream, entity);
		stream.Close ();
	}

	public static object Load(string filename){
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = File.Open (Application.persistentDataPath + "/" + filename,FileMode.Open);
        PlayerDatas entity = (PlayerDatas)formatter.Deserialize(stream);
		stream.Close ();
		return entity;
	}
}

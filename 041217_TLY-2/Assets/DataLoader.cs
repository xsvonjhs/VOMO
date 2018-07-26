using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DataLoader : MonoBehaviour {

	public string[] items;
    private CheckLogin gettingID;
    string getuserID;

	IEnumerator Start()
    {
		WWW itemsData = new WWW("http://pictalive.com/TakeID_internet.php");
		yield return itemsData;
		string itemsDataString = itemsData.text;
		print (itemsDataString);
		items = itemsDataString.Split(';');
  
        /*print(GetDataValue(items[0], "ID:"));
        gettingID = GetComponent<CheckLogin>();
        getuserID = gettingID.check_username;*/



    }


	string GetDataValue(string data, string index)
    {
		string value = data.Substring(data.IndexOf(index)+index.Length);
		if(value.Contains("|"))value = value.Remove(value.IndexOf("|"));
		return value;
	}


}


























//void Start(){
//	string data = "ID:1|Name:Health Potion|Type:consumables|Cost:50";
//	print(GetDataValue(data, "Cost:"));
//}
//
//void Update(){
//	
//}
//
//string GetDataValue(string data ,string index){
//	string value = data.Substring(data.IndexOf(index)+index.Length);
//	if(value.Contains("|"))value = value.Remove(value.IndexOf("|"));
//	return value;
//}

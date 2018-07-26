using UnityEngine;
using System.Collections;

public class Takedata : MonoBehaviour
{

    public string[] items;
    public string dataindex = "";

    IEnumerator Start()
    {
        WWW itemsData = new WWW("http://localhost/Testing1/testingscript.php");
        yield return itemsData;
        string itemsDataString = itemsData.text;
        print(itemsDataString);

        items = itemsDataString.Split(';');
        print(GetDataValue(items[0], dataindex));
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }
}




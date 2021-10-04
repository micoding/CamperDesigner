using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public List<GameObject> prefabs;
    public ScrollRect scrollRect;

	public GameObject Button_Template;

	public GameObject originInTree;


	// Use this for initialization
	void Start()
	{
		foreach (var gameObject in prefabs)
		{
			string str = gameObject.name;
			GameObject go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			NewButton TB = go.GetComponent<NewButton>();
			TB.SetName(str);
			go.transform.SetParent(Button_Template.transform.parent);

		}
	}

	public void InstantiateResource(string name, Vector3? position, Quaternion? rotation, Vector3? scale)
	{
		GameObject newRoot;
		GameObject newObj;

		newRoot = (GameObject)Instantiate(Resources.Load(path: "Root"), transform);
		newObj = (GameObject)Instantiate(Resources.Load(path: name), transform);

		newRoot.transform.parent = originInTree.transform;
		newObj.transform.parent = originInTree.transform;

		newRoot.GetComponent<ConfigurableJoint>().connectedBody = newObj.GetComponent<Rigidbody>();
		newObj.GetComponent<Item>().root = newRoot;

		float posY = 0;

		if (name == "Boxshelf")
			posY = 547.7264f;
		else if (name == "Drawer")
			posY = 214.8213f;
		else if (name == "Kitchen")
			posY = 301.4504f;
		else if (name == "Table")
			posY = 474.7234f;
		else if (name == "Bed")
			posY = 272.8553f;
		else if (name == "Chair")
			posY = 424.5678f;

		newObj.transform.position = new Vector3(0, posY, 0);

		Debug.Log(name + " button clicked.");

		if (!position.HasValue && !rotation.HasValue && !scale.HasValue)
			return;

		newObj.transform.position = (Vector3)position;
		newObj.transform.rotation = (Quaternion)rotation;
		newObj.transform.localScale = (Vector3)scale;

		newRoot.transform.position = newObj.transform.position;
	}
	public void InstantiateResource(string name)
	{
		InstantiateResource(name, null, null, null);
	}
}

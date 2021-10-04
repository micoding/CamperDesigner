using UnityEngine;
using UnityEngine.UI;

public class NewButton : MonoBehaviour
{
	private string Name;
	public Text ButtonText;
	public PopulateGrid ScrollView;

	public void SetName(string name)
	{
		Name = name;
		ButtonText.text = name;
	}
	public void Button_Click()
	{
		ScrollView.InstantiateResource(Name);
	}
}

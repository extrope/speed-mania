using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleColourModifier : MonoBehaviour {
	// Vehicle to be saved
	public GameObject vehicle;
	// String variables for saving preference of vehicle colour
	private static readonly string VehicleBodyColourPref = "VehicleBodyColourPref";
	private static readonly string VehicleTireColourPref = "VehicleTireColourPref";
	private static readonly string VehicleSpoilerPref = "VehicleSpoilerColourPref";
	
	// Variable creation for; Body part Index,  colour holder, RGB holders
	private int partSelected;
	private Color partColour;
	private string partName;
	private float Red;
	private float Green;
	private float Blue;
	// Setting the parts of the vehicle that are to change colour
	private MeshRenderer bodyPart;
	public MeshRenderer vehicleBody;
	public MeshRenderer vehicleTire;
	public MeshRenderer vehicleSpoiler;
	// Setting the sliders that will change the RGB values
	public Slider red;
	public Slider green;
	public Slider blue;
	
	public void Start()
	{

		// Run if not first time customising vehicle body
		if(PlayerPrefs.HasKey(VehicleBodyColourPref))		
		{
			bodyPart = vehicleBody;
			// Debug.Log("Not first time");
			ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("VehicleBodyColourPref"), out partColour);
			
			vehicleBody.material.color = partColour;		
			vehicleBody.material.SetColor("_EmmisionColor", partColour);

			// Set current RGB values of slider
			Red = partColour.r;
			Green = partColour.g;
			Blue = partColour.b;

			red.value = Red;
			green.value = Green;
			blue.value = Blue;
		}

		// Run if not first time customising Tire colour
		if(PlayerPrefs.HasKey(VehicleTireColourPref))
		{
			// Debug.Log("FIRST RUN THROUGH");
			ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("VehicleTireColourPref"), out partColour);

			vehicleTire.material.color = partColour;		
			vehicleTire.material.SetColor("_EmmisionColor", partColour);
		}

		// Run if not first time customising Spoiler colour
		if(PlayerPrefs.HasKey(VehicleSpoilerPref))
		{
			// Debug.Log("FIRST RUN THROUGH");
			ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("VehicleSpoilerColourPref"), out partColour);

			vehicleSpoiler.material.color = partColour;		
			vehicleSpoiler.material.SetColor("_EmmisionColor", partColour);
		}
	}
	
	public void Update()
	{
		// Check if Body part is selected
		if(partSelected == 0)
		{
			bodyPart = vehicleBody;
			partName = "VehicleBody";
			SetSliderRGB("VehicleBody");  
			//Debug.Log("partselected is Body");
		}
		// Check if Tire part is selected
		if(partSelected == 1)
		{
			bodyPart = vehicleTire;
			partName = "VehicleTire";
			SetSliderRGB("VehicleTire");
			//Debug.Log("partselected is Tire");
		}
		// Check if Spoiler part is selected
		if(partSelected == 2)
		{
			bodyPart = vehicleSpoiler;
			partName = "VehicleSpoiler";
			SetSliderRGB("VehicleSpoiler");
			//Debug.Log("partselected is Spoiler");
		}
	}
	
	// Run when slider values change
	public void OnEdit() {
		// Set RGB colour values (based on slider value)
		Color color = bodyPart.material.color;
		color.r = red.value;
		color.g = green.value;
		color.b = blue.value;		
		
		// Set body colour 
		bodyPart.material.color = color;		
		bodyPart.material.SetColor("_EmmisionColor", color);

		// Save colour to Player Pref
		partColour = color;		
		PlayerPrefs.SetString(partName + "ColourPref", ColorUtility.ToHtmlStringRGBA(partColour));
		// Debug.Log("SAVING GAME" + " " + partColour + " " + partName);
	}
	
	public void Body()
	{
		partSelected = 0; 
	}
	
	public void Tire()
	{
		partSelected = 1;
	}
	
	public void Spoiler()
	{
		partSelected = 2;
	}
	
	/*
	public void saveColor()
	{  
		PlayerPrefs.SetString(partName + "ColourPref", ColorUtility.ToHtmlStringRGBA(partColour));
		Debug.Log("SAVING GAME" + " " + partColour + " " + partName);
	}
	*/
	
	void SetSliderRGB(string x)
	{
		ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString(x+"ColourPref"), out partColour);		
		// Debug.Log("SLIDER SETTING" + " " + partColour + " " + x);
		// Set RGB slider values
		Red = partColour.r;
		Green = partColour.g;
		Blue = partColour.b;

		red.value = Red;
		green.value = Green;
		blue.value = Blue;
	}
	
	public void DontDestroyVehicle()
	{
		DontDestroyOnLoad(vehicle);
	}
}

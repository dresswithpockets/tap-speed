using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyStrokesPerSecond : MonoBehaviour {

	public Text KSPMText;
	public Text TimeText;
	public Text StartTappingText;
	public InputField TimeField;

	public float KeyPressesPerSecond
	{
		get
		{
			float kpps = KeyPressesSinceBurstBegan / TimeSinceBurstBegan;

			if (kpps.ToString("N") == "NaN")
			{
				return 0f;
			}

			return KeyPressesSinceBurstBegan / TimeSinceBurstBegan;
		}
	}

	private int m_KeyPressesSinceBurstBegan = 0;
	public int KeyPressesSinceBurstBegan
	{
		get
		{
			return m_KeyPressesSinceBurstBegan;
        }
		set
		{
			m_KeyPressesSinceBurstBegan = value;
			KSPMText.text = "  Key Strokes Per Second: " + KeyPressesPerSecond.ToString("N");
		}
	}

	private float m_TimeSinceBurstBegan = 0f;
	public float TimeSinceBurstBegan
	{
		get
		{
			return m_TimeSinceBurstBegan;
		}
		set
		{
			m_TimeSinceBurstBegan = value;
			TimeText.text = "  Time: " + TimeSinceBurstBegan.ToString("N");
		}
	}
	public float BurstTime = 10f;

	public bool Burst = false;
	public bool Reset = true;

	void Start()
	{
		StartTappingText.text = "Start tapping Z or X to begin the burst!\n\r" +
			"The burst will last for 10 seconds.";
	}

	// Update is called once per frame
	void Update()
	{
		if (Burst)
		{
			if (BurstKeyDown())
			{
				KeyPressesSinceBurstBegan++;
			}

			if (TimeSinceBurstBegan > BurstTime)
			{
				Burst = false;
			}
			else
			{
				TimeSinceBurstBegan += Time.deltaTime;
			}
		}
		else
		{
			if (BurstKeyDown() && Reset)
			{
				Burst = true;
				Reset = false;
				KeyPressesSinceBurstBegan++;
				BurstTime = float.Parse(TimeField.text);
			}
		}

	}

	public void ResetBurst()
	{
		TimeSinceBurstBegan = 0f;
		KeyPressesSinceBurstBegan = 0;
		Burst = false;
		Reset = true;
	}

	public void StopBurst()
	{
		Burst = false;
	}

	bool BurstKeyDown()
	{
		return Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X);
	}
}

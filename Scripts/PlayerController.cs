using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("general setup Settings")]
	[Tooltip("How fast ship moves up and down")][SerializeField] float xMovespeed = 2.0f;
	[SerializeField] float yMovespeed = 2.0f;
	[SerializeField] float xRange = 5f;
	[SerializeField] float yRange = 5f;
	[SerializeField] GameObject [] lasers;
	
	[SerializeField] float positionPitchFactor = -2f;
	[SerializeField] float controlPitchFactor = -12f;
	[SerializeField] float positionRollFactor = 5f;
	[SerializeField] float controlRollFactor = 5f;
	[SerializeField] float positionYawFactor = 5f;
	
	
	float xThrow;
	float yThrow;
	
    void Update()
    {
		ProcessTranslation();
		ProcessRotation();
		Processfiring();
	}
	
	
	void ProcessTranslation()
	{
		
        xThrow = Input.GetAxis("Horizontal");
		yThrow = Input.GetAxis("Vertical");
		
	
		
		float xOffset = xThrow * Time.deltaTime * xMovespeed;	
		float yOffset = yThrow * Time.deltaTime * yMovespeed;	
		
		
		float xRawPosition = transform.localPosition.x + xOffset;
		float yRawPosition = transform.localPosition.y + yOffset;
		
		float clampedxPos = Mathf.Clamp(xRawPosition, -xRange, xRange);
		float clampedyPos = Mathf.Clamp(yRawPosition, -yRange, yRange);
		
		transform.localPosition = new Vector3(clampedxPos ,clampedyPos,transform.localPosition.z);
		
	}
	
	void ProcessRotation()
	{
		float pitchdueToPosition = transform.localPosition.y * positionPitchFactor;
		float pitchDueToControlthrow = yThrow * controlPitchFactor;
		
		float pitch =  pitchdueToPosition + pitchDueToControlthrow;
		float yaw = transform.localPosition.x * positionYawFactor;
		float roll = transform.localPosition.z * positionRollFactor + xThrow * controlRollFactor;
		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	void Processfiring(){

		if(Input.GetButton("Fire1")){
			SetLasersActive(true);
		}
		else{
			SetLasersActive(false);
		}

	}
	void SetLasersActive(bool isActive){
		foreach (GameObject laser in lasers){
			var emissionModule = laser.GetComponent<ParticleSystem>().emission;
			emissionModule.enabled = isActive;
		}
	}

}





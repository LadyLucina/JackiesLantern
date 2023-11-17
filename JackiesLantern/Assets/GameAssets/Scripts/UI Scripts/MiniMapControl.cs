using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Author Joshua G
Purpose: This script stops the mini map from rotating whenever the player turns
*/

public class MiniMapControl : MonoBehaviour
{
	public GameObject Player;

	public void LateUpdate()
	{
		transform.position = new Vector3(Player.transform.position.x , 40 , Player.transform.position.z);
	}
}

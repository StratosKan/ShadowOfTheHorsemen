using UnityEngine;

public class SimpleRotatorScript : MonoBehaviour
{
    //it really is a simple rotator script it takes the public vector3 and rotates it by Time.deltaTime this is mostly used for cosmetic purposes on orbs
    public Vector3 rotateDegrees;

	private void Update ()
    {
        transform.Rotate(rotateDegrees * Time.deltaTime);
    }
}
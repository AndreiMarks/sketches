using UnityEngine;
using UnityDesigner;

public class FarmBoardSquare : MonoBehaviour
{
	public Square square;

	public void SetColor( Color color )
	{
		square.SetColor( color );
	}
	
}

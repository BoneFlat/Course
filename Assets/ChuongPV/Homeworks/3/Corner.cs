using UnityEngine;

public enum CornerType
{
	TopLeft = 0,
	DownLeft,
	TopRight,
	DownRight
}

public class Corner : MonoBehaviour
{
	[SerializeField] private CornerType _cornerType;

	public CornerType CornerType => _cornerType;

	void Start()
	{
		Vector3 pos                = default;
		float   distanceFromCamera = 10;

		switch (_cornerType)
		{
			case CornerType.DownLeft:
				pos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, distanceFromCamera));
				break;
			case CornerType.DownRight:
				pos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, distanceFromCamera));
				break;
			case CornerType.TopLeft:
				pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, distanceFromCamera));
				break;
			case CornerType.TopRight:
				pos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceFromCamera));
				break;
		}

		transform.position = pos;
	}
}
using UnityEngine;
using System.Collections;
public class Cam : MonoBehaviour
{
    private Transform target;
	public float smootPosition;

	public float smootSize;

	public float height = 10f;

	public float minSize = 12f;

	public float maxSize = 18f;
    private Camera cam;
    Transform camT;

	[HideInInspector]
	public float _sizeCamera;
private void Start()
	{
		target = GameObject.Find("Player").transform;
        cam = Camera.main;
		_sizeCamera = cam.orthographicSize;
        camT = transform;
	}
	// private void LateUpdate()
	// {
	// 	if(target != null)
	// 	{
	// 	Vector3 b = new Vector3(target.position.x,target.position.y,height);
	// 	camT.position = Vector3.Lerp(transform.position, b, Time.deltaTime * smootPosition);	
	// 	}
	// 	if(_sizeCamera > cam.orthographicSize)
	// 	{
	// 		cam.orthographicSize += Time.deltaTime;
	// 	}
	// }
	private void FixedUpdate()
	{
		if(target != null)
		{
		Vector3 b = new Vector3(target.position.x,target.position.y,height);
		camT.position = Vector3.Lerp(transform.position, b, Time.deltaTime * smootPosition);	
		}
		if(_sizeCamera > cam.orthographicSize)
		{
			cam.orthographicSize += Time.deltaTime;
		}
	}
// 	private IEnumerator ShakeCameraCor(float duration, float magnitude, float noize)
// {
//     //Инициализируем счётчиков прошедшего времени
//     float elapsed = 0f;
//     //Сохраняем стартовую локальную позицию
//     Vector3 startPosition = transform.localPosition;
//     //Генерируем две точки на "текстуре" шума Перлина
//     Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
//     Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

//     //Выполняем код до тех пор пока не иссякнет время
//     while (elapsed < duration)
//     {
//         //Генерируем две очередные координаты на текстуре Перлина в зависимости от прошедшего времени
//         Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
//         Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
//         //Создаём новую дельту для камеры и умножаем её на длину дабы учесть желаемый разброс
//         Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
//         cameraPostionDelta *= magnitude;

//         //Перемещаем камеру в нувую координату
//         transform.localPosition = startPosition + (Vector3)cameraPostionDelta;

//         //Увеличиваем счётчик прошедшего времени
//         elapsed += Time.deltaTime;
//         //Приостанавливаем выполнение корутины, в следующем кадре она продолжит выполнение с данной точки
//         yield return null;
//     }
// }
// 	public void CameraVibration(float duration, float magnitude, float noize)
// 	{
// 		StartCoroutine(ShakeCameraCor(duration, magnitude,noize));
// 	}
}
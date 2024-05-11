[SerializeField]
private Camera _mainCamera;

private void LateUpdate()
{
	Vector3 cameraPosition = _mainCamera.transform.position;
	cameraPosition.y = transform.position.y;
	transform.LookAt(cameraPosition);
	transform.Rotate(0.f, 180.f, 0.f);
}
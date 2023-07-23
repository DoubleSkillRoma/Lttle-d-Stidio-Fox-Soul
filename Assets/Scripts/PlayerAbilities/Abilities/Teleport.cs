using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private GameObject _ballPrefab;

    private SpriteRenderer _playerSpriteRenderer;

    private readonly float _spawnDistance = 2.0f;
    private readonly float _returnDistance = 15.0f;

    private bool _isShiftPressed = false;
    private bool _wasShiftPressed = false;

    private void Start()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _wasShiftPressed = _isShiftPressed;
        _isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (_wasShiftPressed && !_isShiftPressed)
        {
            CheckDistanceAndCreateBall();
        }

        if (_isShiftPressed)
        {
            _ballPrefab.SetActive(true);
            _playerSpriteRenderer.color = Color.red;
        }
        else
        {
            _playerSpriteRenderer.color = Color.white;
        }
    }

    public void CheckDistanceAndCreateBall()
    {
        float distance = Vector2.Distance(transform.position, _ballPrefab.transform.position);
        if (distance > 0 && distance <= _returnDistance)
        {
            _ballPrefab.SetActive(false);

            Vector2 leftRayOrigin = (Vector2)transform.position - (Vector2)transform.right * _spawnDistance;
            RaycastHit2D leftHit = Physics2D.Raycast(leftRayOrigin + Vector2.up, Vector2.down, 10f, _groundLayer);

            if (leftHit.collider == null || !leftHit.collider.CompareTag("Ground"))
            {
                Vector2 rightRayOrigin = (Vector2)transform.position + (Vector2)transform.right * _spawnDistance;
                RaycastHit2D rightHit = Physics2D.Raycast(rightRayOrigin + Vector2.up, Vector2.down, 10f, _groundLayer);

                if (rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
                {
                    SpawnBall(rightHit.point);
                }
            }
            else
            {
                SpawnBall(leftHit.point);
            }
        }
    }

    public void SpawnBall(Vector2 position)
    {
        _ballPrefab.transform.position = position + _ballPrefab.transform.localScale.y * 1.0f * Vector2.up;
        _ballPrefab.SetActive(true);

        StartCoroutine(FadeInObject(_ballPrefab, 1.0f));
    }

    private IEnumerator FadeInObject(GameObject objåct, float duration)
    {
        SetObjectAlpha(objåct, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            SetObjectAlpha(objåct, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetObjectAlpha(objåct, 1f);
    }

    private void SetObjectAlpha(GameObject obj, float alpha)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}

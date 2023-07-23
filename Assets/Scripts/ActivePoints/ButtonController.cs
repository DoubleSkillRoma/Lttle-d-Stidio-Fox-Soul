using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private SunRoof _sunRoof;
    private Renderer _objectRenderer;
    private Color _originalColor;

    private bool _isPlayerInside = false;
    private bool _isBallInside = false;

    private void Start()
    {
        _objectRenderer = GetComponent<Renderer>();
        _originalColor = _objectRenderer.material.color;
    }

    private void Update()
    {
        if (_isPlayerInside || _isBallInside)
        {
            _sunRoof.Open();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _objectRenderer.material.color = Color.red;
            _isPlayerInside = true;
        }
        else if (collision.CompareTag("Ball"))
        {
            _objectRenderer.material.color = Color.red;
            _isBallInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            _isPlayerInside = false;
        }
        else if (collision.CompareTag("Ball"))
        {
            _isBallInside = false;
        }

        if (!_isPlayerInside && !_isBallInside)
        {
            _objectRenderer.material.color = _originalColor;
            _sunRoof.Close();
        }
    }
}

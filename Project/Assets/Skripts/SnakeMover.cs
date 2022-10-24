using UnityEngine;
using UnityEngine.Events;

public class SnakeMover : MonoBehaviour
{
    [SerializeField] private Transform segmentPrefab;
    [SerializeField] private TrailRenderer trailRenderer;

    [SerializeField] internal UnityEvent onEat;
    [SerializeField] internal UnityEvent onRotatate;

    private bool canRotate = true;

    private Vector2 direction = Vector2.right;

    private void Start()
    {
        trailRenderer.time = 0.4f;
        SnakeManager.snakeSegments = new();
        SnakeManager.snakeSegments.Add(this.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) RotateSnake(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) RotateSnake(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) RotateSnake(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) RotateSnake(Vector2.right);
    }

    private void FixedUpdate()
    {
        MoveSnake();
    }

    private void MoveSnake()
    {
        for (int i = SnakeManager.snakeSegments.Count - 1; i > 0; i--)
        {
            SnakeManager.snakeSegments[i].position = SnakeManager.snakeSegments[i - 1].position;
        }

        this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x + direction.x),
                Mathf.Round(this.transform.position.y + direction.y),
                0.0f);
        canRotate = true;
        SwapColor();
    }

    private void RotateSnake(Vector2 _direction)
    {
        if (_direction == direction || !canRotate) return;
        onRotatate?.Invoke();
        canRotate = false;
        direction = _direction;
    }

    private void SnakeGrow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = SnakeManager.snakeSegments[SnakeManager.snakeSegments.Count - 1].position;
        SnakeManager.snakeSegments.Add(segment);
        PaintSnake();
    }

    public void ResetState()
    {
        for (int i = 1; i < SnakeManager.snakeSegments.Count; i++)
        {
            Destroy(SnakeManager.snakeSegments[i].gameObject);
        }
        SnakeManager.snakeSegments.Clear();
        SnakeManager.snakeSegments.Add(this.transform);

        this.transform.position = Vector3.zero;
        trailRenderer.time = 0.3f;

        SnakeManager.score = 0;
        SnakeManager.scoreMulti = 0;
        SnakeManager.comboTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            SnakeGrow();
            SnakeManager.score += SnakeManager.scoreMulti + 1;
            trailRenderer.time += 0.1f;
            onEat?.Invoke();
        }
        else if (other.CompareTag("Obstacle") || other.CompareTag("Snake"))
        {
            GameState.Instance.StopGame();
        }
    }

    private void PaintSnake()
    {
        SnakeManager.snakeSegments[1].GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void SwapColor()
    {
        for(int i = SnakeManager.snakeSegments.Count -1; i > 0; i--)
        {
            SnakeManager.snakeSegments[i].GetComponent<SpriteRenderer>().color = SnakeManager.snakeSegments[i - 1].GetComponent<SpriteRenderer>().color;
        }
    }
}

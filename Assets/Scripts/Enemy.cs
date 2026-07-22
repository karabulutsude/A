using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] points;

    private int i = 0;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (points == null || points.Length == 0) return;

        // X eksenindeki mesafeye göre nokta değiştirme
        float distanceX = Mathf.Abs(transform.position.x - points[i].position.x);

        if (distanceX < 0.2f)
        {
            i++;
            if (i >= points.Length)
            {
                i = 0;
            }
        }

        // Sadece X ekseninde hareket etme
        Vector3 targetPosition = new Vector3(points[i].position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Yön Dönüşü - GÜNCELLENEN KISIM
        if (transform.position.x < points[i].position.x)
        {
            // Sağa giderken
            spriteRenderer.flipX = true; // Görsel sola bakıyorsa sağa çevirmek için TRUE yap
        }
        else if (transform.position.x > points[i].position.x)
        {
            // Sola giderken
            spriteRenderer.flipX = false; // Görsel sola bakıyorsa (orijinal hali) FALSE yap
        }
    }
}
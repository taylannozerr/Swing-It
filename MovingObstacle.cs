using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Rota Ayarlari")]
    public Transform[] waypoints; // Nesnenin sırayla gideceği hedefler
    public float speed = 5f;      // Hareket hızı

    private int currentWaypointIndex = 0;

    void Update()
    {
        // Eğer hedef nokta atanmamışsa hata vermemesi için durdur
        if (waypoints.Length == 0) return;

        // Sıradaki hedef noktayı belirle
        Transform target = waypoints[currentWaypointIndex];

        // Nesneyi hedefe doğru sabit bir hızla hareket ettir
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Hedefe yeterince yaklaştıysak (0.1 birimden yakınsa) diğer noktaya geç
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;

            // Eğer listedeki son hedefe ulaştıysak, en başa (0. index'e) dön
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }
}
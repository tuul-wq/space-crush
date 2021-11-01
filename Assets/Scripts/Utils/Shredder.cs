using UnityEngine;

namespace Utils
{
    public class Shredder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            DestroyObject(collision);
        }

        private static void DestroyObject(Collider2D collision)
        {
            var collisionObject = collision.gameObject;
            collisionObject.SetActive(false);
            Destroy(collisionObject);
        }
    }
}
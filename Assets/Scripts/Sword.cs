using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static int _swordInShield = 0;
    public static int _helmetCount;
    public static bool _gameIsOver = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            _gameIsOver = true;
            SwordShot.ReadyToShot = true;
            Vibration.VibratePeek();
            return;
        }
        SwordShot.ReadyToShot = true;
        transform.SetParent(collision.gameObject.transform);
        GetComponent<Rigidbody2D>().isKinematic = true;
        Time.timeScale = 1f;
        GameObject shield = collision.gameObject;
        shield.GetComponent<Animation>().Play();
        _swordInShield++;
        Vibration.VibratePop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0.3f;
        if (collision.gameObject.CompareTag("Helmet"))
        {
            _helmetCount++;
            Destroy(collision.gameObject);
        }
    }
}

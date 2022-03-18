using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShot : MonoBehaviour
{
    [SerializeField] private GameObject swordPref;
    [SerializeField] private GameObject shieldPref;
    [SerializeField] private GameObject destroedShieldPref;
    [SerializeField] private Transform swordStartPos;
    [SerializeField] private Transform shieldPos;
    [SerializeField] private float swordSpeed;

    private static int maxSwordsCount;
    private static int currentLevel;
    public static int CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }
    private static bool readyToShot;
    public static bool ReadyToShot
    {
        get => readyToShot;
        set => readyToShot = value;
    }
    private GameObject currentSword;
    private GameObject currentShield;
    private Rigidbody2D rb;
    private static int swordsCount;
    private int swordInShield = 0;

    private void Awake()
    {
        maxSwordsCount = 3;
        currentSword = Instantiate(swordPref, swordStartPos.position, Quaternion.Euler(0, 0, 45));
        currentShield = Instantiate(shieldPref, shieldPos.position, Quaternion.identity);
        Sword._swordInShield = 0;
        currentLevel = 0;
        swordsCount = maxSwordsCount;
        readyToShot = false;
    }

    private void Update()
    {
        swordInShield = Sword._swordInShield;
        if (maxSwordsCount == swordInShield && UIManager.IsPlaying == true)
        {
            StartCoroutine("BlowShield");
        }
        if (UIManager.IsPlaying)
        {
            Shot();
        }
    }

    private void Shot()
    {
        if (swordsCount > 0 && readyToShot)
        {
            currentSword = Instantiate(swordPref, swordStartPos.position, Quaternion.Euler(0, 0, 45));
            readyToShot = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb = currentSword.gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.velocity = Vector2.up * swordSpeed;
            swordsCount--;
            currentSword = null;
        }
    }

    private IEnumerator BlowShield()
    {
        Vibration.VibratePeek();
        Destroy(currentShield.gameObject);
        GameObject dest = Instantiate(destroedShieldPref, shieldPos.position, Quaternion.identity);
        UIManager.IsPlaying = false;
        yield return new WaitForSeconds(2);
        Destroy(dest);
        currentShield = Instantiate(shieldPref, shieldPos.position, Quaternion.identity);
        Sword._swordInShield = 0;
        maxSwordsCount++;
        swordsCount = maxSwordsCount;
        currentLevel++;
        UIManager.IsPlaying = true;
    }

    public static string SwordsCounts()
    {
        string swords = swordsCount + "/" + maxSwordsCount;
        return swords;
    }
}

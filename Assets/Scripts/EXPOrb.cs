using UnityEngine;

public class EXPOrb : MonoBehaviour
{
    public int expValue = 1;

    // EXP ใหญ่หรือไม่
    public bool isBigEXP = false;

    // EXP จะอยู่บนพื้นกี่วินาที
    public float lifeTime = 10f;

    void Start()
    {
        // ถ้าไม่เก็บภายในเวลาที่กำหนด EXP จะหาย
        Destroy(gameObject, lifeTime);

        // ขนาด EXP
        if (isBigEXP)
        {
            transform.localScale = Vector3.one * 2.0f;
        }
        else
        {
            transform.localScale = Vector3.one * 1.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerEXP playerEXP =
                collision.GetComponent<PlayerEXP>();

            if (playerEXP != null)
            {
                playerEXP.AddEXP(expValue);

                Debug.Log("Collected EXP: " + expValue);
            }

            Destroy(gameObject);
        }
    }
}
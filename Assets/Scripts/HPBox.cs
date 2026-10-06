using UnityEngine;

public class HPBox : MonoBehaviour
{
    public int healAmount = 30;

    // HP Box จะอยู่บนพื้นกี่วินาที
    public float lifeTime = 10f;

    void Start()
    {
        // ถ้าไม่เก็บภายในเวลาที่กำหนด HP Box จะหาย
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player =
                collision.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.currentHP += healAmount;

                // HP ห้ามเกิน Max HP
                if (player.currentHP > player.maxHP)
                {
                    player.currentHP =
                        player.maxHP;
                }

                player.UpdateHPBar();

                Debug.Log(
                    "HP BOX | Heal +" + healAmount +
                    " | HP: " +
                    player.currentHP +
                    "/" +
                    player.maxHP
                );

                Destroy(gameObject);
            }
        }
    }
}
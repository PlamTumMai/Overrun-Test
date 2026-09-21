using UnityEngine;

public class EXPOrb : MonoBehaviour
{
    public int expValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("EXP touched: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            PlayerEXP playerEXP = collision.GetComponent<PlayerEXP>();

            if (playerEXP != null)
            {
                playerEXP.AddEXP(expValue);
                Debug.Log("EXP COLLECTED!");
            }

            Destroy(gameObject);
        }
    }
}
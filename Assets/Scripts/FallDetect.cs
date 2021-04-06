using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetect : MonoBehaviour
{
    // Funçao de deteçao das coordenadas do jogador com a box do falldetection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Quando o jogador colide com a box do fall detection, a vida do jogador fica a zero
        if (collision.gameObject.tag == "LocalPlayer")
            collision.gameObject.GetComponent<PlayerHealth>().Health -= 100;
    }
}

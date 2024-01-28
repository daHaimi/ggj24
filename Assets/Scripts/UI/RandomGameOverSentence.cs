using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RandomGameOverSentence : MonoBehaviour
{
    string[] gameOverPuns = {
    "This is no laughing matter.",
    "Game over, the final laugh track.",
    "Your quest ends with a laughter twist.",
    "In the realm of games, laughter is the ultimate game over.",
    "The last level brought to you by the power of laughter.",
    "Game over, but the echoes of laughter linger.",
    "The credits roll, accompanied by the sound of laughter.",
    "Your journey concludes with a symphony of laughter.",
    "Laugh your way out of this game over scenario.",
    "Game over, the punchline of your gaming saga.",
    "The final chapter, where laughter takes center stage.",
    "No respawn, only the echoes of laughter remain.",
    "Game over, a laughter-filled curtain call.",
    "Your adventure concludes with a burst of laughter.",
    "The laughter echoes as the screen fades to black.",
    "Game over, but the laughter lingers in the pixels.",
    "The game's final note: a crescendo of laughter.",
    "In the end, it's all about the laughter over game.",
    "Game over, a punchline that ends in laughter.",
    "As the pixels fade, laughter is the last sound you hear." };

    void Start()
    {
        int randomIndex = Random.Range(0, gameOverPuns.Length);
        GetComponent<Text>().text = gameOverPuns[randomIndex];

        gameObject.LeanScale(Vector3.one * 1.3f, 5f)
            .setEaseOutSine()
            .setOnComplete(() => GameOverController.ReturnToLastScene());
    }
}

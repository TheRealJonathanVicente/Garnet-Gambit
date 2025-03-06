using UnityEngine;

public class McGuffin : MonoBehaviour, IInteractable
{
    public GameObject Item;
    public AudioSource mcGuffinSound;
    public static int winCon = 0;

    public void Interact()
    {
        if (mcGuffinSound != null)
        {
            mcGuffinSound.Stop(); // Stop any currently playing sound
            mcGuffinSound.Play();
            Debug.Log("McGuffin sound played!");
        }
        else
        {
            Debug.LogError("mcGuffinSound is not assigned!");
        }

        Debug.Log("Found the McGuffin!");
        winCon += 1;
        Debug.Log(winCon);
        Item.SetActive(false);
    }
}
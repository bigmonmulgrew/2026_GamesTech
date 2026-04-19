using UnityEngine;

public class Firework : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystem fireworkEffectPrefab;


    public void Interact()
    {
        // Instatiate the firework effect at the position of the firework object
        ParticleSystem fireworkEffect = Instantiate(fireworkEffectPrefab, transform.position, Quaternion.identity);

        fireworkEffect.Play();
    }
}

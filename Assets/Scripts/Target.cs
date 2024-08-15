using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public enum Type
    {
        Mole,
        Ghost,
        Empty
    }
    [SerializeField] private Type type;
    public Type TargetType => type;

    [SerializeField] private ParticleSystem particleEffect;
    [SerializeField] private AudioSource dieAudio;
    [SerializeField] private GameObject visualsParent;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO questionCompleteEventChannel;

    public void Die()
    {
        if (particleEffect != null)
        {
            particleEffect.Play();
        }
        if (dieAudio != null)
        {
            dieAudio.Play();
        }
        if (visualsParent != null)
        {
            visualsParent.SetActive(false);
        }
        StartCoroutine(Co_Die());
    }

    IEnumerator Co_Die()
    {
        yield return new WaitForSeconds(2f);
        if (type == Type.Mole || type == Type.Ghost)
        {
            questionCompleteEventChannel.RaiseEvent();
        }
        Destroy(gameObject);
    }
}

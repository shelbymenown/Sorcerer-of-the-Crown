using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource source;

    void Start()
    {
        source = GameObject.Find("PauseMoveSfx").GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.Play();
    }
}

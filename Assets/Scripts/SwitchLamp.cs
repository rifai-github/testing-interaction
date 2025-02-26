using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SwitchLamp : MonoBehaviour
{
    [SerializeField] private Interactable swithInteractable;
    [SerializeField] private GameObject textBubble;
    [SerializeField] private Renderer indicator;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private CanvasGroup uiFeedback;
    [SerializeField] private TMP_Text textFeedback;

    [SerializeField] private List<Light> lights = new();

    private AudioSource audioSource;
    private bool isActive = false;

    private void OnEnable()
    {
        swithInteractable.OnInteract.AddListener(OnInteract);
    }

    private void OnDisable()
    {
        swithInteractable.OnInteract.RemoveListener(OnInteract);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (uiFeedback != null)
        {
            uiFeedback.alpha = 0;
        }

        TurnLight(false);
        PlayAnimation(false);
        textBubble.SetActive(true);
    }

    private void OnInteract()
    {
        isActive = !isActive;
        ShowUIFeedback();
        TriggerAudioFeedback();
        TurnLight(isActive);
        PlayAnimation(isActive);
        textBubble.SetActive(false);
    }


    private void PlayAnimation(bool active)
    {
        if (indicator != null)
        {
            indicator.material.color = active ? Color.green : Color.red;
        }

        if (animator != null)
        {
            animator.SetBool("isOn", active);
        }
    }

    private void TurnLight(bool active)
    {
        foreach (Light light in lights)
        {
            light.enabled = active;
        }
    }

    private void TriggerAudioFeedback()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }

    private void ShowUIFeedback()
    {
        if (uiFeedback != null)
        {
            uiFeedback.DOKill();
            uiFeedback.alpha = 1;
            uiFeedback.DOFade(0f, 1f).SetDelay(1f);
        }

        if (textFeedback != null)
        {
            textFeedback.text = isActive ? "Switch On" : "Switch Off";
        }
    }
}
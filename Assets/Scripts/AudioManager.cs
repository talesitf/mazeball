using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Não esqueça de incluir isso para usar Slider

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        // Verifica se já existe uma instância e garante que apenas uma exista
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Garante que o AudioManager persista entre cenas
    }
    
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip hit;
    public AudioClip background;
    public AudioClip death;
    public AudioClip point;
    public AudioClip inMenu;
    public AudioClip win;
    public AudioClip achievement;
    private float last_volume;
    private float finalTime; 
    private int finalScore;


    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        musicSource.clip = inMenu;
        musicSource.loop = true;
        musicSource.Play();
        // Define o valor inicial do slider com o volume atual
        musicSlider.value = musicSource.volume;
        musicSlider.onValueChanged.AddListener(SetVolume); // Adiciona o listener
        last_volume = musicSource.volume;
    }

    public void PlayBackground()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayDeath()
    {
        SFXSource.clip = death;
        SFXSource.Play();
    }

    public void PlayPoint(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayInMenu()
    {
        musicSource.clip = inMenu;
        musicSource.Play();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume; // Atualiza o volume da música
        SFXSource.volume = volume; // Atualiza o volume do SFX, se necessário
    }

    public void Save()
    {
        last_volume = musicSource.volume;
    }

    public void Out()
    {
        musicSource.volume = last_volume;
        SFXSource.volume = last_volume;
    }
    public void SetFinalTime(float time)
    {
        finalTime = time;
    }

    public float GetFinalTime()
    {
        return finalTime;
    }

      public void SetFinalScore(int score)
    {
        finalScore = score;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }
}

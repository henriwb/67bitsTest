using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClipsData audioData; // Referência ao ScriptableObject contendo os AudioClips
    [SerializeField] private int poolSize = 10; // Tamanho da pool

    private Dictionary<string, Queue<AudioSource>> audioSourcePool; // Pool de AudioSources
    private bool isSoundOn;

    public Button AudioOn;
    public Button AudioOff;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1; // Som habilitado por padrão
        InitPool();
    }

    private void Start()
    {
        if(AudioOn != null)
        {
            AudioOn.onClick.RemoveAllListeners();
            AudioOn.onClick.AddListener(delegate () 
            {
                AudioOn.gameObject.SetActive(false);
                AudioOff.gameObject.SetActive(true);
                SetSound(false);
            });

            AudioOff.onClick.RemoveAllListeners();
            AudioOff.onClick.AddListener(delegate ()
            {
                AudioOn.gameObject.SetActive(true);
                AudioOff.gameObject.SetActive(false);
                SetSound(true);
            });
            AudioOn.gameObject.SetActive(isSoundOn);
            AudioOff .gameObject.SetActive(!isSoundOn);
        }

       
        
    }

    // Inicializa o Object Pool para os sons
    private void InitPool()
    {
        audioSourcePool = new Dictionary<string, Queue<AudioSource>>();

        foreach (var clip in audioData.audioClips)
        {
            Queue<AudioSource> queue = new Queue<AudioSource>();

            // Criar objetos para o pool
            for (int i = 0; i < poolSize; i++)
            {
                AudioSource audioSource = new GameObject("AudioSource_" + clip.name).AddComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.playOnAwake = false;
                audioSource.gameObject.SetActive(false);
                audioSource.transform.SetParent(transform); // Organiza os objetos sob o SoundManager
                queue.Enqueue(audioSource);
            }

            audioSourcePool.Add(clip.name, queue);
        }
    }

    // Função para tocar um som a partir do nome do clip
    public void PlaySound(string clipName, float pitch = 1f, float volume = 1f)
    {
        if (!isSoundOn || !audioSourcePool.ContainsKey(clipName)) return;

        AudioSource source = GetPooledAudioSource(clipName);
        source.volume = volume;
        if (source != null)
        {
            source.pitch = pitch; // Ajusta o pitch do som
            source.gameObject.SetActive(true);
            source.Play();

            StartCoroutine(ReturnToPoolAfterPlay(source, clipName));
        }
        else
        {
            Debug.LogError("SOUND NOT FOUND");
        }
    }

    // Obtém um AudioSource da pool correspondente
    private AudioSource GetPooledAudioSource(string clipName)
    {
        if (audioSourcePool.ContainsKey(clipName))
        {
            AudioSource source = audioSourcePool[clipName].Dequeue();
            audioSourcePool[clipName].Enqueue(source); // Coloca de volta no final da fila
            return source;
        }
        return null;
    }

    // Retorna o AudioSource à pool após tocar
    private System.Collections.IEnumerator ReturnToPoolAfterPlay(AudioSource source, string clipName)
    {
        yield return new WaitWhile(() => source.isPlaying);
        source.gameObject.SetActive(false);
    }

    // Liga ou desliga o som
    public void SetSound(bool state)
    {
        isSoundOn = state;
        PlayerPrefs.SetInt("SoundOn", state ? 1 : 0); // Salva a preferência
    }

    // Função auxiliar para obter clip pelo nome (opcional)
    public AudioClip GetClipByName(string clipName)
    {
        foreach (var clip in audioData.audioClips)
        {
            if (clip.name == clipName)
                return clip;
        }
        return null;
    }
}

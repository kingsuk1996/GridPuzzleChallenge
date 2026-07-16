using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SplashLoadingController : MonoBehaviour
{
    [Header("Loading")]
    [SerializeField] private Image _loadingFill;
    [SerializeField] private float _loadingDuration = 2.5f;

    [Header("UI")]
    [SerializeField] private GameObject _loadingRoot;
    [SerializeField] private Button _playButton;

    [Header("Panels")]
    [SerializeField] private GameObject _splashPanel;
    [SerializeField] private GameObject _gameplayPanel;

    private void Awake()
    {
        _splashPanel.SetActive(true);
        _gameplayPanel.SetActive(false);

        _loadingFill.fillAmount = 0f;
        _loadingRoot.SetActive(true);

        _playButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener(Play);

        StartCoroutine(FakeLoading());
    }

    private IEnumerator FakeLoading()
    {
        float timer = 0f;

        while (timer < _loadingDuration)
        {
            timer += Time.deltaTime;

            _loadingFill.fillAmount = Mathf.Clamp01(timer / _loadingDuration);

            yield return null;
        }

        _loadingFill.fillAmount = 1f;

        _loadingRoot.SetActive(false);

        _playButton.gameObject.SetActive(true);
    }

    private void Play()
    {
        _splashPanel.SetActive(false);
        _gameplayPanel.SetActive(true);
    }
}
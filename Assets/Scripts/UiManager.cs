using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Components:")]
    [SerializeField] private ActionManager _actionManager;
    [SerializeField] private GameObject _loseTab;
    [SerializeField] private GameObject _winTab;
    [SerializeField] private Image _resizeBar;
    [SerializeField] private GameObject _player;

    private Ball _ball;

    private AudioSource _audioSource;

    private void Start()
    {
        _loseTab.SetActive(false);
        _winTab.SetActive(false);

        _audioSource = GetComponent<AudioSource>();

        _ball = _player.GetComponent<Ball>();
    }

    private void Update()
    {
        ChangeResizeBar();
    }

    private void ShowLoseTab()
    {
        _loseTab.SetActive(true);
    }

    private void ShowWinTab()
    {
        _winTab.SetActive(true);
    }

    public void LoadNextScene()
    {
        _audioSource.Play();

        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCount+1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ReloadScene()
    {
        _audioSource.Play();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeResizeBar()
    {
        _resizeBar.fillAmount = _ball._scaleMultiplicator;
    }

    private void OnEnable()
    {
        _actionManager.win += ShowWinTab;
        _actionManager.lose += ShowLoseTab;
    }

    private void OnDisable()
    {
        _actionManager.win -= ShowWinTab;
        _actionManager.lose -= ShowLoseTab;
    }
}

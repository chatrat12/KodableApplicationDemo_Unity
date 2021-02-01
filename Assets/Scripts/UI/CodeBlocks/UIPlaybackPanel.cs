using UnityEngine;
using UnityEngine.UI;

public class UIPlaybackPanel : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private KarelComponent _karel;
    [SerializeField] private UIBlockEditor _editor;

    
    private void Start()
    {
        ReadyState();

        _editor.ProgramComplete.AddListener(() =>
        {
            ResetState();
        });

        _playButton.onClick.AddListener(() =>
        {
            _editor.StartProgram();
            PlayingState();
        });
        _pauseButton.onClick.AddListener(() =>
        {
            _karel.KarelInstance.Paused = true;
            PausedState();
        });
        _resumeButton.onClick.AddListener(() =>
        {
            _karel.KarelInstance.Paused = false;
            PlayingState();
        });
        _resetButton.onClick.AddListener(() =>
        {
            ReadyState();
            _karel.Reset();
        });
    }

    private void PlayingState()
    {
        _playButton.gameObject.SetActive(false);
        _resumeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        _resetButton.gameObject.SetActive(true);
    }

    private void PausedState()
    {
        _playButton.gameObject.SetActive(false);
        _resumeButton.gameObject.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        _resetButton.gameObject.SetActive(false);
    }

    private void ReadyState()
    {
        _playButton.gameObject.SetActive(true);
        _resumeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(false);
        _resetButton.gameObject.SetActive(false);
    }

    private void ResetState()
    {
        _playButton.gameObject.SetActive(false);
        _resumeButton.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(false);
        _resetButton.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreUi;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restart;
    [SerializeField]
    private Image _liveUi;
    [SerializeField]
    private Sprite[] _liveSprites;

    public void UpdateScore(int score)
    {
        _scoreUi.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int lives)
    {
        _liveUi.sprite = _liveSprites[lives];
        if (lives == 0)
        {
            StartCoroutine(TextFlickerRoutine());
            _restart.gameObject.SetActive(true);
        }
    }

    IEnumerator TextFlickerRoutine()
    {
        while (true)
        {
            _gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}

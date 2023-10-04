using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PartsManager : MonoBehaviour
{
    private int _level;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private List<SpriteContainer> _sprites;
    [SerializeField] private List<Part> _parts = new List<Part>();
    private List<Part> _activatedParts = new List<Part>();

    [SerializeField] private List<ParticleSystem> _winParts = new List<ParticleSystem>();

    private void Start()
    {
        _level = 0;
        if (PlayerPrefs.HasKey("Level"))
            _level = PlayerPrefs.GetInt("Level");

        SpriteContainer chosenSprite = _sprites[_level % _sprites.Count];

        _nextLevelButton.onClick.AddListener(LoadNextLevel);
        _nextLevelButton.gameObject.SetActive(false);

        foreach (var part in _parts)
        {
            part.Sprite = chosenSprite.Sprites[_parts.IndexOf(part)];
            part.UpdateSprite();

            part.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f * Random.Range(1, 4));
            if (Mathf.FloorToInt(part.transform.rotation.eulerAngles.z) == 0)
            {
                _activatedParts.Add(part);
                part.ShowChecker();
            }
        }
    }

    public void AddPart(int index)
    {
        if (!_activatedParts.Contains(_parts[index]))
            _activatedParts.Add(_parts[index]);
        CheckWin();
        Debug.Log("ADD");
    }

    public void RemovePart(int index)
    {
        if (!_activatedParts.Contains(_parts[index]))
            _activatedParts.Remove(_parts[index]);
        Debug.Log("DELETE");

    }

    private void CheckWin()
    {
        if (_activatedParts.Count == 16)
        {
            foreach (var particles in _winParts)
            {
                particles.Play();
            }

            foreach (var part in _parts)
            {
                part.Win();
                part.transform.DOScale(Vector3.one * 0.2265f, 0.2f).OnComplete(part.HideChecker);
            }

            _nextLevelButton.gameObject.SetActive(true);
            _nextLevelButton.transform.localScale = Vector3.zero;
            _nextLevelButton.transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBack);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Level", _level);
    }

    private void LoadNextLevel()
    {
        _level++;
        PlayerPrefs.SetInt("Level", _level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

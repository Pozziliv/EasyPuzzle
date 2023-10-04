using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class Part : MonoBehaviour
{
    [SerializeField] private int _id = 0;
    [SerializeField] private PartsManager _manager;
    private GameObject _checker;
    private SpriteRenderer _spriteRenderer;

    public Sprite Sprite { get; set; }

    private bool _isMouseIn = false;
    private bool _isWin = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _checker = transform.GetChild(0).gameObject;
    }

    public void UpdateSprite()
    {
        _spriteRenderer.sprite = Sprite;
    }

    void Update()
    {
        if(!_isWin && _isMouseIn && Mathf.FloorToInt(transform.rotation.eulerAngles.z) % 90 == 0)
        {
            RotatePart();
            _isMouseIn = false;
        }
    }

    private void RotatePart()
    {
        transform.DORotate((Mathf.FloorToInt(transform.rotation.eulerAngles.z / 90) + 1) * new Vector3(0, 0, 90f), 0.5f, RotateMode.Fast).OnComplete(CheckRotation);
    }

    private void CheckRotation()
    {
        if(Mathf.FloorToInt(transform.rotation.eulerAngles.z) == 0)
        {
            _manager.AddPart(_id);
            ShowChecker();
        }
        else
        {
            _manager.RemovePart(_id);
            HideChecker();
        }
    }

    private void OnMouseDown()
    {
        _isMouseIn = true;
    }

    public void ShowChecker()
    {
        _checker.SetActive(true);
    }

    public void HideChecker()
    {
        _checker.SetActive(false);
    }

    public void Win()
    {
        _isWin = true;
    }
}

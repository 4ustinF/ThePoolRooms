using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

struct Subtitle
{
    public string text;
    public float seconds;
    public Subtitle(string Text, float Seconds)
    {
        text = Text;
        seconds = Seconds;
    }
}

public class SubtitleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _subtitleText = null;

    [Header("Variables")]
    [SerializeField] private float _textSpeed = 1.5f;
    [SerializeField] private float _dialouge1Length = 4f;
    [SerializeField] private float _dialouge2aLength = 8f;
    [SerializeField] private float _dialouge2bLength = 8f;
    [SerializeField] private float _dialouge3aLength = 10f;
    [SerializeField] private float _dialouge3bLength = 6f;
    [SerializeField] private float _dialouge4Length = 12f;
    [SerializeField] private float _dialouge5aLength = 10f;
    [SerializeField] private float _dialouge5bLength = 4f;
    [SerializeField] private float _dialouge5cLength = 10f;
    [SerializeField] private float _dialouge6aLength = 12f;
    [SerializeField] private float _dialouge6bLength = 5f;
    [SerializeField] private float _dialouge7aLength = 4f;
    [SerializeField] private float _dialouge7bLength = 8f;
    [SerializeField] private float _dialouge7cLength = 6f;

    private Subtitle _currentSubtitle;
    private List<List<Subtitle>> _subtitles;
    private int _currentLine = 0;
    private int _currentLinePart = 0;
    private int _characterProgress = 1;
    private bool _readLine = false;
    private bool _startedReadingLine = false;
    private float _timePerLine = 0f;
    private float _timeSinceLastLine = 0f;
    private float _timeLineHasPlayed = 0f;

    private float _frame = 1 / 60f;

    public void Start()
    {
        _subtitles = new List<List<Subtitle>>();
        for (int i = 0; i < 7; i++)
        {
            _subtitles.Add(new List<Subtitle>());
        }
        _subtitles[0].Add(new Subtitle("Life begins with a stumble", _dialouge1Length));
        _subtitles[1].Add(new Subtitle("Just as this ball awkwardly descends the stairs, everyone begins their journey clumsily,", _dialouge2aLength));
        _subtitles[1].Add(new Subtitle("Relying on momentum and instinct over purposeful smooth decision making.", _dialouge2bLength));
        _subtitles[2].Add(new Subtitle("Everything is a first, but your destination is clear and although there may be bumps along your road,", _dialouge3aLength));
        _subtitles[2].Add(new Subtitle("You know where you are going and there is a comfort in that.", _dialouge3bLength));
        _subtitles[3].Add(new Subtitle("But once you’ve adjusted to the basics of life, adolescence begins and everything you thought you knew changes once again.", _dialouge4Length));
        _subtitles[4].Add(new Subtitle("Your life can begin to drift in a direction you never expected and your destination can become more and more unclear.", _dialouge5aLength));
        _subtitles[4].Add(new Subtitle("Who are you? Where are you going?", _dialouge5bLength));
        _subtitles[4].Add(new Subtitle("The future is obscured and only by going forward can we find the answer to these questions.", _dialouge5cLength));
        _subtitles[5].Add(new Subtitle("But ambiguity cannot last forever. Eventually everyone comes to an understanding of who they are and what they want out of life.", _dialouge6aLength));
        _subtitles[5].Add(new Subtitle("The darkness is gone and a clear road lies ahead.", _dialouge6bLength));
        _subtitles[6].Add(new Subtitle("Will you be able to make your way through them?", _dialouge7aLength));
        _subtitles[6].Add(new Subtitle("Once you know who you are, the tools to deal with whatever may come are right there for you.", _dialouge7bLength));
        _subtitles[6].Add(new Subtitle("All you need … is to pick them up", _dialouge7cLength));
        _subtitleText.text = "";
    }

    public void ReadNextLine()
    {
        if (_readLine == true)
        {
            if (_subtitles[_currentLine].Count - 1 == _currentLinePart)
            {
                _currentLine++;
                _currentLinePart = 0;
            }
            else
            {
                _currentLinePart++;
            }
        }
        else
        {
            _readLine = true;
        }
        _startedReadingLine = false;
    }

    public void ReadSpecificLine(int lineNum)
    {
        if (lineNum - 1 > 6)
        {
            return;
        }

        if (_readLine == true)
        {
            if (_subtitles[_currentLine].Count - 1 == _currentLinePart)
            {
                _currentLine++;
                _currentLinePart = 0;
            }
            else
            {
                _currentLinePart++;
            }
        }
        else
        {
            _readLine = true;
        }
        _startedReadingLine = false;
        _currentLine = lineNum-1;
        _currentLinePart = 0;
    }

    void FixedUpdate()
    {
        if (_readLine == false)
        {
            return;
        }

        //early return at max line count
        if (_currentLine > 6)
        {
            _subtitleText.text = "";
            _readLine = false;
            return;
        }

        if (_startedReadingLine == false)
        {
            _currentSubtitle = _subtitles[_currentLine][_currentLinePart];
            _startedReadingLine = true;
            _characterProgress = 1;
            _timeSinceLastLine = 0f;
            _timeLineHasPlayed = 0f;
            _subtitleText.color = new Color(_subtitleText.color.r, _subtitleText.color.g, _subtitleText.color.b, 1);
            if(_currentSubtitle.seconds - _textSpeed <= 0)
            {
                _timePerLine = .1f;
            }
            else
            {
                _timePerLine = (_currentSubtitle.seconds - _textSpeed) / _currentSubtitle.text.Length;
            }
            ShowText();
        }
        else
        {
            _timeSinceLastLine += _frame;
            _timeLineHasPlayed += _frame;
            if (_timeSinceLastLine > _timePerLine)
            {
                _timeSinceLastLine = 0f;
                ShowText();
            }

            if (_timeLineHasPlayed > _currentSubtitle.seconds)
            {
                _readLine = false;
                _startedReadingLine = false;
                if (_subtitles[_currentLine].Count - 1 == _currentLinePart)
                {
                    _currentLine++;
                    _currentLinePart = 0;
                    StartCoroutine(FadeTextToZeroAlpha(2f));
                }
                else
                {
                    _currentLinePart++;
                    StartCoroutine(FadeTextToZeroAlpha(1f));
                }
            }
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t)
    {
        _subtitleText.color = new Color(_subtitleText.color.r, _subtitleText.color.g, _subtitleText.color.b, 1f);
        while (_subtitleText.color.a > 0.0f)
        {
            if(_readLine == true)
            {
                yield break;
            }

            _subtitleText.color = new Color(_subtitleText.color.r, _subtitleText.color.g, _subtitleText.color.b, _subtitleText.color.a - (Time.deltaTime / t));
            yield return null;
        }

        if(_currentLinePart > 0)
        {
            _readLine = true;
        }
    }

    private void ShowText()
    {
        if (_characterProgress >= _currentSubtitle.text.Length)
        {
            _subtitleText.text = _currentSubtitle.text;
            return;
        }

        string visible = _currentSubtitle.text.Substring(0, _characterProgress);
        string notVisible = _currentSubtitle.text.Substring(_characterProgress, Mathf.Max(_currentSubtitle.text.Length - (_characterProgress), 0));
        _subtitleText.text = $"{visible}<color=#00000000>{notVisible}</color>";
        _characterProgress++;
    }
}

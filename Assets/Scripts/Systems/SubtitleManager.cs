using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SubtitleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _subtitleText = null;

    [Header("Variables")]
    [SerializeField] private float _textSlowness = 1f;

    private string _currentSubtitle;
    private List<List<string>> _subtitles;
    private int _currentLine = 0;
    private int _currentLinePart = 0;
    private int _characterProgress = 1;
    private bool _readLine = false;
    private bool _startedReadingLine = false;
    private float _timePerLine = 0f;
    private float _timeSinceLastLine = 0f;

    private float _frame = 1 / 60f;

    public void Start()
    {
        _subtitles = new List<List<string>>();
        for (int i = 0; i < 7; i++)
        {
            _subtitles.Add(new List<string>());
        }
        _subtitles[0].Add("Life begins with a stumble");
        _subtitles[1].Add("Just as this ball awkwardly descends the stairs, everyone begins their journey clumsily,");
        _subtitles[1].Add("Relying on momentum and instinct over purposeful smooth decision making.");
        _subtitles[2].Add("Everything is a first, but your destination is clear and although there may be bumps along your road,");
        _subtitles[2].Add("You know where you are going and there is a comfort in that.");
        _subtitles[3].Add("But once you’ve adjusted to the basics of life, adolescence begins and everything you thought you knew changes once again.");
        _subtitles[4].Add("Your life can begin to drift in a direction you never expected and your destination can become more and more unclear.");
        _subtitles[4].Add("Who are you? Where are you going?");
        _subtitles[4].Add("The future is obscured and only by going forward can we find the answer to these questions.");
        _subtitles[5].Add("But ambiguity cannot last forever. Eventually everyone comes to an understanding of who they are and what they want out of life.");
        _subtitles[5].Add("The darkness is gone and a clear road lies ahead.");
        _subtitles[6].Add("Will you be able to make your way through them?");
        _subtitles[6].Add("Once you know who you are, the tools to deal with whatever may come are right there for you.");
        _subtitles[6].Add("All you need . . . is to pick them up");
        _subtitleText.text = "";
    }


    public void ReadNextLine()
    {
        StartCoroutine(ReadNextLineFunc());
    }

    public IEnumerator ReadNextLineFunc()
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
            _readLine = false;
            yield return null;
        }
        _readLine = true;
        _startedReadingLine = false;
        StartCoroutine(ReadText());
    }

    public void ReadSpecificLine(int lineNum)
    {
        StartCoroutine(ReadSpecificLineFunc(lineNum));
    }

    private IEnumerator ReadSpecificLineFunc(int lineNum)
    {
        if (lineNum - 1 > 6)
        {
            yield break;
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
            _readLine = false;
            yield return null;
        }
        _readLine = true;
        _startedReadingLine = false;
        _currentLine = lineNum-1;
        _currentLinePart = 0;
        StartCoroutine(ReadText());
    }

    private IEnumerator ReadText()
    {
        while (_readLine == true) 
        {
            if (_startedReadingLine == false)
            {
                _currentSubtitle = _subtitles[_currentLine][_currentLinePart];
                _startedReadingLine = true;
                _characterProgress = 1;
                _timeSinceLastLine = 0f;
                _subtitleText.color = new Color(_subtitleText.color.r, _subtitleText.color.g, _subtitleText.color.b, 1);
                _timePerLine = ((_currentSubtitle.Length / 11f) / _currentSubtitle.Length) + (_textSlowness / 10f);
                ShowText();
            }
            else
            {
                _timeSinceLastLine += _frame;
                if (_timeSinceLastLine > _timePerLine)
                {
                    _timeSinceLastLine = 0f;
                    ShowText();
                }

                //after 5 timePerLine seconds with all text fully displayed the text fade begins
                if (_characterProgress > _currentSubtitle.Length + 5)
                {
                    _startedReadingLine = false;
                    _readLine = false;
                    if (_subtitles[_currentLine].Count - 1 == _currentLinePart)
                    {
                        _currentLine++;
                        _currentLinePart = 0;
                        StartFade(2f);
                    }
                    else
                    {
                        _currentLinePart++;
                        StartFade(1f);
                    }
                }
            }
            yield return null;
        }
    }

    private void StartFade(float t)
    {
        StartCoroutine(FadeTextToZeroAlpha(t));
    }

    private IEnumerator FadeTextToZeroAlpha(float t)
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
            ReadNextLine();
        }
    }

    private void ShowText()
    {
        if (_characterProgress >= _currentSubtitle.Length)
        {
            _characterProgress++;
            _subtitleText.text = _currentSubtitle;
            return;
        }

        string visible = _currentSubtitle.Substring(0, _characterProgress);
        string notVisible = _currentSubtitle.Substring(_characterProgress, Mathf.Max(_currentSubtitle.Length - (_characterProgress), 0));
        _subtitleText.text = $"{visible}<color=#00000000>{notVisible}</color>";
        _characterProgress++;
    }
}

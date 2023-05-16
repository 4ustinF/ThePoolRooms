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
    [SerializeField] private float lineTimeLength1 = 0.0f;
    [SerializeField] private float lineTimeLength2a = 0.0f;
    [SerializeField] private float lineTimeLength2b = 0.0f;
    [SerializeField] private float lineTimeLength3a = 0.0f;
    [SerializeField] private float lineTimeLength3b = 0.0f;
    [SerializeField] private float lineTimeLength4 = 0.0f;
    [SerializeField] private float lineTimeLength5a = 0.0f;
    [SerializeField] private float lineTimeLength5b = 0.0f;
    [SerializeField] private float lineTimeLength5c = 0.0f;
    [SerializeField] private float lineTimeLength6a = 0.0f;
    [SerializeField] private float lineTimeLength6b = 0.0f;
    [SerializeField] private float lineTimeLength7a = 0.0f;
    [SerializeField] private float lineTimeLength7b = 0.0f;
    [SerializeField] private float lineTimeLength7c = 0.0f;

    private string _currentSubtitle = "";
    private List<List<string>> _subtitles;
    private int _currentLine = 0;
    private int _currentLinePart = 0;
    private int _characterProgress = 1;
    private bool _readLine = false;
    private bool _startedReadingLine = false;
    private float _timePerCharacter = 0f;
    private float _timeSinceLastLine = 0f;

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
        _subtitles[6].Add("There may be troubles in your future. Anything could happen. Will you be able to make your way through them?");
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
        StartCoroutine(ReadText(-1f));
    }

    public void ReadSpecificLine(int lineNum, int linePartNum, float lineSpeed)
    {
        StartCoroutine(ReadSpecificLineFunc(lineNum, linePartNum, lineSpeed));
    }

    private IEnumerator ReadSpecificLineFunc(int lineNum, int linePartNum, float lineSpeed)
    {
        if (lineNum - 1 > 6)
        {
            yield break;
        }

        if (_readLine == true)
        {
            _readLine = false;
            yield return null;
        }
        _readLine = true;
        _startedReadingLine = false;
        _currentLine = lineNum - 1;
        _currentLinePart = linePartNum - 1;
        StartCoroutine(ReadText(lineSpeed));
    }

    private IEnumerator ReadText(float lineSpeed)
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
                if(lineSpeed != -1f)
                {
                    Debug.Log($"Reading line over {lineSpeed} seconds");
                    _timePerCharacter = lineSpeed / _currentSubtitle.Length;
                }
                else
                {
                    _timePerCharacter = GetLineTimeLength() / _currentSubtitle.Length;
                }
                ShowText();
            }
            else
            {
                _timeSinceLastLine += Time.deltaTime;
                if (_timeSinceLastLine > _timePerCharacter)
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
            if (_readLine == true)
            {
                yield break;
            }

            _subtitleText.color = new Color(_subtitleText.color.r, _subtitleText.color.g, _subtitleText.color.b, _subtitleText.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    private void ShowText()
    {
        if (_characterProgress >= _currentSubtitle.Length)
        {
            _characterProgress++;
            _subtitleText.text = "<line-height=150%>"+_currentSubtitle;
            return;
        }

        string visible = _currentSubtitle.Substring(0, _characterProgress);
        string notVisible = _currentSubtitle.Substring(_characterProgress, Mathf.Max(_currentSubtitle.Length - (_characterProgress), 0));
        _subtitleText.text = $"<line-height=150%>{visible}<color=#00000000>{notVisible}</color>";
        _characterProgress++;
    }


    private float GetLineTimeLength()
    {
        switch (_currentLine)
        {
            case 0:
                Debug.Log($"Running Line 1 for {lineTimeLength1} seconds");
                return lineTimeLength1;
            case 1:
                switch (_currentLinePart)
                {
                    case 0:
                        Debug.Log($"Running Line 2a for {lineTimeLength2a} seconds");
                        return lineTimeLength2a;
                    case 1:
                        Debug.Log($"Running Line 2b for {lineTimeLength2b} seconds");
                        return lineTimeLength2b;
                    default:
                        Debug.LogError("Uninitialized line part time trying to be found");
                        break;
                }
                break;
            case 2:
                switch (_currentLinePart)
                {
                    case 0:
                        Debug.Log($"Running Line 3a for {lineTimeLength3a} seconds");
                        return lineTimeLength3a;
                    case 1:
                        Debug.Log($"Running Line 3b for {lineTimeLength3b} seconds");
                        return lineTimeLength3b;
                    default:
                        Debug.LogError("Uninitialized line part time trying to be found");
                        break;
                }
                break;
            case 3:
                Debug.Log($"Running Line 4 for {lineTimeLength4} seconds");
                return lineTimeLength4;
            case 4:
                switch (_currentLinePart)
                {
                    case 0:
                        Debug.Log($"Running Line 5a for {lineTimeLength5a} seconds");
                        return lineTimeLength5a;
                    case 1:
                        Debug.Log($"Running Line 5b for {lineTimeLength5b} seconds");
                        return lineTimeLength5b;
                    case 2:
                        Debug.Log($"Running Line 5c for {lineTimeLength5c} seconds");
                        return lineTimeLength5c;
                    default:
                        Debug.LogError("Uninitialized line part time trying to be found");
                        break;
                }
                break;
            case 5:
                switch (_currentLinePart)
                {
                    case 0:
                        Debug.Log($"Running Line 6a for {lineTimeLength6a} seconds");
                        return lineTimeLength6a;
                    case 1:
                        Debug.Log($"Running Line 6b for {lineTimeLength6b} seconds");
                        return lineTimeLength6b;
                    default:
                        Debug.LogError("Uninitialized line part time trying to be found");
                        break;
                }
                break;
            case 6:
                switch (_currentLinePart)
                {
                    case 0:
                        Debug.Log($"Running Line 7a for {lineTimeLength7a} seconds");
                        return lineTimeLength7a;
                    case 1:
                        Debug.Log($"Running Line 7b for {lineTimeLength7b} seconds");
                        return lineTimeLength7b;
                    case 2:
                        Debug.Log($"Running Line 7c for {lineTimeLength7c} seconds");
                        return lineTimeLength7c;
                    default:
                        Debug.LogError("Uninitialized line part time trying to be found");
                        break;
                }
                break;
            default:
                Debug.LogError("Uninitialized line time trying to be found");
                break;
        }
        return 1f;
    }
}

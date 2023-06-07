using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SubtitleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _subtitleText = null;

    [Header("Speed")]
    [SerializeField] private AnimationCurve LineCurve1 = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve2a = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve2b = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve3a = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve3b = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve4 = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve5a = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve5b = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve5c = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve5d = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve6a = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve6b = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve7a = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve7b = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve7c = new AnimationCurve();
    [SerializeField] private AnimationCurve LineCurve7d = new AnimationCurve();

    private string _currentSubtitle = "";
    private List<List<string>> _subtitles;
    private int _currentLine = 0;
    private int _currentLinePart = 0;
    private bool _readLine = false;
    private bool _startedReadingLine = false;
    private float _timeSinceBeginningOfLine = 0f;

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
        _subtitles[2].Add("Everything is a first and the inexperience that comes with youth is often frightening,");
        _subtitles[2].Add("However, your destination is clear and although there may be bumps along your road, you know where you are going and there is comfort in that.");
        _subtitles[3].Add("But once you’ve adjusted to the basics of life, adolescence begins and everything you thought you knew changes once again.");
        _subtitles[4].Add("Friends come and go. Mentors and idols get replaced. Family members disappear.");
        _subtitles[4].Add("You can begin to drift in a direction you never expected and your destination can become more and more unclear.");
        _subtitles[4].Add("Who are you? Where are you going? Will you be someone else when you arrive?");
        _subtitles[4].Add("The future is obscured and only by going forward can we find the answers to these questions.");
        _subtitles[5].Add("But ambiguity cannot last forever. Eventually everyone comes to an understanding of who they are and what they want out of life.");
        _subtitles[5].Add("When that becomes true for you the mist will part, the darkness will end, and a clear road will lie ahead.");
        _subtitles[6].Add("That doesn’t mean you will know every turn life will take you on.");
        _subtitles[6].Add("There may be troubles in your future. Anything could happen. Will you be able to make your way through them?");
        _subtitles[6].Add("Once you know who you are, the tools to deal with whatever may come are right there for you.");
        _subtitles[6].Add("All you need . . . is to pick them up");
        _subtitleText.text = "";
    }

    public void ReadSpecificLine(int lineNum, int linePartNum, float lineSpeed)
    {
        StartCoroutine(ReadSpecificLineFunc(lineNum, linePartNum, lineSpeed));
    }

    private IEnumerator ReadSpecificLineFunc(int lineNum, int linePartNum, float lineTime)
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
        StartCoroutine(ReadText(lineTime));
    }

    private IEnumerator ReadText(float lineTime)
    {
        while (_readLine == true)
        {
            if (_startedReadingLine == false)
            {
                _currentSubtitle = _subtitles[_currentLine][_currentLinePart];
                _startedReadingLine = true;
                _timeSinceBeginningOfLine = 0f;
                _subtitleText.color = new Color(_subtitleText.color.r, _subtitleText.color.g, _subtitleText.color.b, 1);
                ShowText(lineTime);
            }
            else
            {
                _timeSinceBeginningOfLine += Time.deltaTime;
                ShowText(lineTime);

                //after 2 seconds with all text fully displayed the text fade begins
                if (_timeSinceBeginningOfLine >= lineTime + 2f)
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

    private void ShowText(float lineTime)
    {
        float linearProgress = _timeSinceBeginningOfLine / lineTime;
        float easeProgress = GetLineCurve().Evaluate(linearProgress);
        if (linearProgress >= 1)
        {
            _subtitleText.text = "<line-height=150%>" + _currentSubtitle;
            return;
        }
        int characterProgress = (int)(easeProgress * _currentSubtitle.Length);
        string visible = _currentSubtitle.Substring(0, characterProgress);
        string notVisible = _currentSubtitle.Substring(characterProgress, Mathf.Max(_currentSubtitle.Length - (characterProgress), 0));
        _subtitleText.text = $"<line-height=150%>{visible}<color=#00000000>{notVisible}</color>";
    }

    private AnimationCurve GetLineCurve()
    {
        switch (_currentLine)
        {
            case 0:
                return LineCurve1;
            case 1:
                switch (_currentLinePart)
                {
                    case 0:
                        return LineCurve2a;
                    case 1:
                        return LineCurve2b;
                    default:
                        Debug.LogError("Uninitialized line curve trying to be found");
                        break;
                }
                break;
            case 2:
                switch (_currentLinePart)
                {
                    case 0:
                        return LineCurve3a;
                    case 1:
                        return LineCurve3b;
                    default:
                        Debug.LogError("Uninitialized line curve trying to be found");
                        break;
                }
                break;
            case 3:
                return LineCurve4;
            case 4:
                switch (_currentLinePart)
                {
                    case 0:
                        return LineCurve5a;
                    case 1:
                        return LineCurve5b;
                    case 2:
                        return LineCurve5c;
                    case 3:
                        return LineCurve5d;
                    default:
                        Debug.LogError("Uninitialized line curve trying to be found");
                        break;
                }
                break;
            case 5:
                switch (_currentLinePart)
                {
                    case 0:
                        return LineCurve6a;
                    case 1:
                        return LineCurve6b;
                    default:
                        Debug.LogError("Uninitialized line curve trying to be found");
                        break;
                }
                break;
            case 6:
                switch (_currentLinePart)
                {
                    case 0:
                        return LineCurve7a;
                    case 1:
                        return LineCurve7b;
                    case 2:
                        return LineCurve7c;
                    case 3:
                        return LineCurve7d;
                    default:
                        Debug.LogError("Uninitialized line curve trying to be found");
                        break;
                }
                break;
            default:
                Debug.LogError("Uninitialized line curve trying to be found");
                break;
        }
        return new AnimationCurve();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class SubtitleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialText = null;
    private Subtitle _currentSubtitle;
    private List<List<Subtitle>> _tutorials;
    private int _currentLine = 0;
    private int _currentLinePart = 0;
    private int _characterProgress = 1;
    private bool _readLine = false;
    private bool _startedReadingLine = false;
    private float _timePerLine = 0f;
    private float _timeSinceLastLine = 0f;
    private float _timeLineHasPlayed = 0f;
    private 

    struct Subtitle
    {
        public string text;
        public float seconds;
        public Subtitle(string Text, int Seconds)
        {
            text = Text;
            seconds = Seconds;
        }
    }


    public void Start()
    {
        _tutorials = new List<List<Subtitle>>();
        for (int i = 0; i < 7; i++)
        {
            _tutorials.Add(new List<Subtitle>());
        }
        _tutorials[0].Add(new Subtitle("Life begins with a stumble", 4));
        _tutorials[1].Add(new Subtitle("Just as this ball awkwardly descends the stairs, everyone begins their journey clumsily,", 8));
        _tutorials[1].Add(new Subtitle("relying on momentum and instinct over purposeful smooth decision making.", 8));
        _tutorials[2].Add(new Subtitle("Everything is a first, but your destination is clear and although there may be bumps along your road,", 10));
        _tutorials[2].Add(new Subtitle("along your road, you know where you are going and there is a comfort in that.", 6));
        _tutorials[3].Add(new Subtitle("But once you’ve adjusted to the basics of life, adolescence begins and everything you thought you knew changes once again.", 12));
        _tutorials[4].Add(new Subtitle("Your life can begin to drift in a direction you never expected and your destination can become more and more unclear.", 10));
        _tutorials[4].Add(new Subtitle("Who are you? Where are you going?", 4));
        _tutorials[4].Add(new Subtitle("The future is obscured and only by going forward can we find the answer to these questions.", 10));
        _tutorials[5].Add(new Subtitle("But ambiguity cannot last forever. Eventually everyone comes to an understanding of who they are and what they want out of life.", 12));
        _tutorials[5].Add(new Subtitle("The darkness is gone and a clear road lies ahead.", 5));
        _tutorials[6].Add(new Subtitle("Will you be able to make your way through them?", 4));
        _tutorials[6].Add(new Subtitle("Once you know who you are, the tools to deal with whatever may come are right there for you.", 8));
        _tutorials[6].Add(new Subtitle("All you need … is to pick them up", 6));
        _tutorialText.text = "";
    }

    public void ReadNextLine()
    {
        _readLine = true;
    }

    void Update()
    {
        if (_readLine == false)
        {
            return;
        }

        if (_startedReadingLine == false)
        {
            _currentSubtitle = _tutorials[_currentLine][_currentLinePart];
            _startedReadingLine = true;
            _characterProgress = 1;
            _timeSinceLastLine = 0f;
            _timeLineHasPlayed = 0f;
            _tutorialText.color = new Color(_tutorialText.color.r, _tutorialText.color.g, _tutorialText.color.b, 1);
            _timePerLine = (_currentSubtitle.seconds - 2) / _currentSubtitle.text.Length;
            ShowText();
        }
        else
        {
            _timeSinceLastLine += Time.deltaTime;
            _timeLineHasPlayed += Time.deltaTime;
            if (_timeSinceLastLine > _timePerLine)
            {
                _timeSinceLastLine = 0f;
                ShowText();
            }
            if (_timeLineHasPlayed > _currentSubtitle.seconds)
            {
                _readLine = false;
                _startedReadingLine = false;
                StartCoroutine(FadeTextToZeroAlpha(2f));
                if (_tutorials[_currentLine].Count - 1 == _currentLinePart)
                {
                    _currentLine++;
                    _currentLinePart = 0;
                }
                else
                {
                    _currentLinePart++;
                }
            }
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t)
    {
        _tutorialText.color = new Color(_tutorialText.color.r, _tutorialText.color.g, _tutorialText.color.b, 1);
        while (_tutorialText.color.a > 0.0f)
        {
            if(_readLine == true)
            {
                yield break;
            }
            _tutorialText.color = new Color(_tutorialText.color.r, _tutorialText.color.g, _tutorialText.color.b, _tutorialText.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    private void ShowText()
    {
        if (_characterProgress >= _currentSubtitle.text.Length)
        {
            _tutorialText.text = _currentSubtitle.text;
            return;
        }
        string visible = _currentSubtitle.text.Substring(0, _characterProgress);
        string notVisible = _currentSubtitle.text.Substring(_characterProgress, Mathf.Max(_currentSubtitle.text.Length - (_characterProgress), 0));
        _tutorialText.text = $"{visible}<color=#00000000>{notVisible}</color>";
        _characterProgress++;
    }
}

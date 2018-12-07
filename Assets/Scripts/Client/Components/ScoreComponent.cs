using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreComponent
    : MonoBehaviour
{
    [SerializeField] private List<Image> Images;
    [SerializeField] private Sprite GrayTexture;
    [SerializeField] private Sprite ColorTexture;

    private int Score;
    public void Setup(int score)
    {
        this.Score = score;
    }

    private void Refresh()
    {
        var i = 0;
        foreach(var image in this.Images)
        {
            if (i++ < this.Score)
                image.sprite = this.ColorTexture;
            else
                image.sprite = this.GrayTexture;

        }
    }
}

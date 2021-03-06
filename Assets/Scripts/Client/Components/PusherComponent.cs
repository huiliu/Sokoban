﻿using System;
using System.Collections;
using UnityEngine;
using Logic;

public class PusherComponent
    : SmoothMove
{
    public Action<int> OnWin;

    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;

    private SpriteRenderer SpriteRenderer;
    private Animator Animator;
    protected void Start()
    {
        this.Animator = GetComponent<Animator>();
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private Pusher Pusher;
    private LevelMap CurrentMap { get { return this.Pusher.Cell.Map; } }
    public void Setup(Pusher pusher)
    {
        this.Pusher = pusher;
    }

    private Direction FaceDirection;
    private void SetFaceDirection(Direction direction)
    {
        this.FaceDirection = direction;
    }

    private IEnumerator SetFaceSprite(Direction direction)
    {
        yield return new WaitForSeconds(0.05f);

        var p = GetFaceSprint(direction);
        Base.Log.Info("Game", "Set Pusher FaceDirection: {0}/{1}", direction, p.name);
        this.SpriteRenderer.sprite = p;
    }

    private Sprite GetFaceSprint(Direction direction)
    {
        switch(direction)
        {
            case Direction.Down:
                return this.downSprite;
            case Direction.Left:
                return this.leftSprite;
            case Direction.Right:
                return this.rightSprite;
            case Direction.Up:
                return this.upSprite;
            default:
                Base.Log.Assert(false, "unknow Direction: {0}", direction);
                return null;
        }
    }

    protected void Update()
    {
        if (this.Pusher != null)
            this.TryMove();
    }

    private bool MoveFlag = false;
    private void TryMove()
    {
        var targetPos = this.Pusher.Cell.Position.ToEntityLayerPosition();
        if (this.CanMove(targetPos))
        {
            this.MoveFlag = true;
            this.Move((targetPos - this.transform.localPosition).ToVector2().ToDirection());
        }
    }

    private bool CanMove(Vector3 targetPos)
    {
        if (this.transform.localPosition == targetPos)
            return false;

        if (this.MoveFlag)
            return false;

        //if (this.CurrentMap.Finished)
        //    return false;

        return true;
    }

    private void Move(Direction direction)
    {
        this.SetFaceDirection(direction);
        this.PlayAnimation(direction);
        this.MoveTo(this.Pusher.Cell.Position.ToEntityLayerPosition());
    }

    private void PlayAnimation(Direction d)
    {
        switch (d)
        {
            case Direction.Down:
                this.Animator.SetTrigger("Down");
                break;
            case Direction.Left:
                this.Animator.SetTrigger("Left");
                break;
            case Direction.Right:
                this.Animator.SetTrigger("Right");
                break;
            case Direction.Up:
                this.Animator.SetTrigger("Up");
                break;
        }
    }

    protected override void MoveEnded()
    {
        this.MoveFlag = false;
        Base.Log.Info("Game", "Pusher Move End!");

        this.TryWin();
    }

    private void TryWin()
    {
        if (this.CurrentMap.Finished)
        {
            this.OnWin.SafeInvoke(this.CurrentMap.MoveCount);
        }
    }
}

using UnityEngine;
using Logic;

public class CellComponent
    : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Sprite floorSprite;
    [SerializeField] private Sprite goalSprite;
    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite boxSprite;
    [SerializeField] private Sprite boxOnGoalSprite;
    [SerializeField] private Sprite pusherOnGoalSprite;

    private Cell Cell;
    public void SetUp(Cell cell)
    {
        this.Cell = cell;
    }


}

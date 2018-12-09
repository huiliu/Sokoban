using UnityEngine;
using UnityEngine.UI;

namespace Sokoban.Client
{
    public class StatusComponent
        : MonoBehaviour
    {
        [SerializeField] private Button ReturnButton;
        [SerializeField] private Button RestartButton;
        [SerializeField] private Button HelpButton;
        [SerializeField] private Text LevelText;
        [SerializeField] private Text StepText;

        private void Awake()
        {
            this.ReturnButton.onClick.AddListener(() =>
            {
                this.GameComponent.BackToLevel();
            });

            this.RestartButton.onClick.AddListener(() =>
            {
                this.GameComponent.Restart();
            });
        }

        private GameComponent GameComponent;
        public void Init(GameComponent gameComponent)
        {
            this.GameComponent = gameComponent;
        }

        public void Refresh()
        {
            this.LevelText.text = string.Format("{0}/{1}", this.GameComponent.CurrentMap.LevelID, this.GameComponent.TotalLevelCount);
            this.StepText.text = this.GameComponent.StepCount.ToString();
        }
    }
}

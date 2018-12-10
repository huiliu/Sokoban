using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Base;

namespace Sokoban.Client
{
    [RequireComponent(typeof(SpriteRef))]
    [RequireComponent(typeof(Image))]
    public class StarComponent
        : MonoBehaviour
    {
        [SerializeField] private GameObject go0;
        [SerializeField] private GameObject go1;
        [SerializeField] private GameObject go2;

        private SpriteRef refs;
        private SpriteRef SpriteRef
        {
            get
            {
                if (this.refs == null)
                    this.refs = this.GetComponentInChildren<SpriteRef>(true);

                return this.refs;
            }
        }

        private int starCount;
        public void ShowStar(int star)
        {
            this.SetActiveEx(true);
            this.starCount = star;
            this.InitImages();
        }

        private void OnEnable()
        {
            this.StartCoroutine(this.ShowAnimation());
        }

        private IEnumerator ShowAnimation()
        {
            do
            {
                if (--this.starCount < 0)
                    break;

                yield return new WaitForSeconds(0.1f);

                this.go0.GetComponent<Image>().sprite = this.SpriteRef["Yellow"];
                this.go0.GetComponent<Animator>().SetTrigger("Show");
                yield return new WaitForSeconds(0.8f);

                if (--this.starCount < 0)
                    break;

                this.go1.GetComponent<Image>().sprite = this.SpriteRef["Yellow"];
                this.go1.GetComponent<Animator>().SetTrigger("Show");
                yield return new WaitForSeconds(0.8f);

                if (--this.starCount < 0)
                    break;

                this.go2.GetComponent<Image>().sprite = this.SpriteRef["Yellow"];
                this.go2.GetComponent<Animator>().SetTrigger("Show");
            } while (false);
        }

        private void InitImages()
        {
            this.go0.GetComponent<Image>().sprite = this.SpriteRef["Gray"];
            this.go1.GetComponent<Image>().sprite = this.SpriteRef["Gray"];
            this.go2.GetComponent<Image>().sprite = this.SpriteRef["Gray"];
        }
    }
}

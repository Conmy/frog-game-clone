using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class SceneController : MonoBehaviour
    {
        private List<GameObject> _splats;
        public int score = 0;
        public GameObject scoreValue;

        private void Start()
        {
            _splats = new List<GameObject>();
            SetScore(0);
        }

        public void AddSplat(GameObject newSplat)
        {
            if (_splats.Count < 5)
            {
                _splats.Add(newSplat);
                return;
            }

            var go = _splats[0];
            _splats.RemoveAt(0);
            Destroy(go);
            _splats.Add(newSplat);
        }

        public void SetScore(int value)
        {
            score = value;
            var textScore = scoreValue.GetComponent<Text>();
            textScore.text = score.ToString();
            
        }

        public void AddScore(int value)
        {
            SetScore(score + value);
        }
    }
}

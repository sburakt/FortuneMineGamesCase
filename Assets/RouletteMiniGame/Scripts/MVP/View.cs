using RouletteMiniGame.AssetManagement;
using RouletteMiniGame.MVP.Helpers;
using UnityEngine;

namespace RouletteMiniGame.MVP
{
    public class View : MonoBehaviour
    {
        private GameObject _sceneBuilderPrefab;
        private GameObject _circleRowPrefab;
        private SceneBuilder _sceneBuilder;
        private Model _model;
        
        public void Initialize(Model model)
        {
            _model = model;
            LoadAssets();
            BuildScene();
        }

        private void LoadAssets()
        {
            foreach (var slot in _model.slots)
            {
                RewardSpriteLoader.AddReward(slot.Reward,Resources.Load<Sprite>(_model.AssetConfig.RewardSpritesPaths[(int)slot.Reward]));
            }
            _sceneBuilderPrefab = Resources.Load<GameObject>(_model.AssetConfig.SceneBuilderPrefabPath);
            _circleRowPrefab = Resources.Load<GameObject>(_model.AssetConfig.CircleRowPrefabPath);
        }
        //assets are unloaded when nre scene is loaded as far as i know

        void BuildScene()
        {
            GameObject sceneBuilder = Instantiate(_sceneBuilderPrefab);
            _sceneBuilder = sceneBuilder.GetComponent<SceneBuilder>();
            _sceneBuilder.Initialize(_model.slots, _circleRowPrefab);
        }
    }
}

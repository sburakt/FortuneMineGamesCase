using UnityEngine;

namespace RouletteMiniGame.MVP
{
    public class Initializer : MonoBehaviour
    { 
        void Awake()
        {
            Model model = new Model();
            ModelLocator.SetModel(model);
            new Presenter(model);
            gameObject.AddComponent<View>().Initialize(model);
        }
    }
}
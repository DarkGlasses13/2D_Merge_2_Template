using UnityEngine;

namespace Assets._Project
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float CooldownSpeed {  get; private set; }
    }
}

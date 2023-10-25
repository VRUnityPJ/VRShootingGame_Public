using System.Threading;
using Cysharp.Threading.Tasks;

namespace VRShooting.Scripts.Stage.Interfaces
{
    public interface ISpawnLevelScale
    {
        public UniTask StartNormalSpawn(CancellationToken token);
        public UniTask SpawnBonusTimeAsync( CancellationToken token);
    }
}

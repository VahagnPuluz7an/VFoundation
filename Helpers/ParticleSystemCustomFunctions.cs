using UniRx;
using UnityEngine;

namespace VFoundation.Helpers
{
    public static class ParticleSystemCustomFunctions 
    {
        public static void PlayWithMusic(this ParticleSystem particle, AudioSource source)
        {
            source.Play();
            particle.Play();
        }
        
        public static void PlayWithMusic(this ParticleSystem particle)
        {
            if(particle.gameObject.TryGetComponent(out AudioSource source))
                source.Play();
            particle.Play();
        }
        
        public static void PlayAfterItPlayMusic(this ParticleSystem particle, AudioSource source)
        {
            particle.Play();
            CompositeDisposable disposable = new();
            Observable.EveryUpdate().Subscribe(x =>
            {
                if (particle.isPlaying) 
                    return;
                source.Play();
                disposable.Clear();
            }).AddTo(disposable);
        }
    }
}

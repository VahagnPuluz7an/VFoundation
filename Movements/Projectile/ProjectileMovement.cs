using System;
using UniRx;
using UnityEngine;

namespace VFoundation.Movements
{
    public static class ProjectileMovement
    {
        private static readonly CompositeDisposable Disposable = new();

        public static void ProjectileMoveToTarget(this Transform transform, Vector3 targetPos, float firingAngle, float gravity, Action callback)
        {
            Vector3 position = transform.position;
            float targetDistance = Vector3.Distance(position, targetPos);
            float projectileVelocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
            float cos = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
            float sin = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
            float flightDuration = targetDistance / cos;
            float elapseTime = 0;
            transform.rotation = Quaternion.LookRotation(targetPos - position);

            Observable.EveryUpdate().Subscribe(x =>
            {
                transform.Translate(0, (sin - (gravity * elapseTime)) * Time.deltaTime, cos * Time.deltaTime);
                elapseTime += Time.deltaTime;

                if (elapseTime < flightDuration)
                    return;
                
                Disposable.Clear();
                callback?.Invoke();

            }).AddTo(Disposable);
        }
    }
}